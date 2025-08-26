using NPR.Editor.Services;
using NPR.Editor.Utilities;
using System;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using UnityEditor;
using UnityEngine;

namespace NPR.Editor.Settings
{
    public static class ResourceManager
    {
        public static void DownloadGameResourcesAsync(string gameKey)
        {
            GetFileListAsync(gameKey);
        }

        private static async void GetFileListAsync(string gameKey)
        {
            var url = ResourceConfig.Games[gameKey];

            var token = NextCloudClient.ExtractTokenFromShareUrl(url);

            if (string.IsNullOrEmpty(token))
            {
                throw new Exception($"Could not extract token from share URL: {url}");
            }

            using var httpClient = new HttpClient();

            httpClient.Timeout = TimeSpan.FromMinutes(10);

            var files = await NextCloudClient.TryWebDavPropFind(httpClient, url, token);

            if (files.Count == 0)
            {
                throw new Exception($"All file discovery methods failed for NextCloud share: {url}");
            }

            files = files.Where(f => !f.IsDirectory && !f.RelativePath.EndsWith("/") && !f.RelativePath.EndsWith("\\")).ToList();

            if (!Directory.Exists(PathUtils.RESOURCE_DIRECTORY))
            {
                Directory.CreateDirectory(PathUtils.RESOURCE_DIRECTORY);
            }

            var path = PathUtils.RESOURCE_DIRECTORY + gameKey;

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            foreach (var file in files)
            {
                var cleanRelativePath = file.RelativePath.Replace('/', Path.DirectorySeparatorChar);

                var localPath = Path.Combine(path, cleanRelativePath);
                var localDir = Path.GetDirectoryName(localPath);

                if (!Directory.Exists(localDir))
                {
                    Directory.CreateDirectory(localDir);
                }

                if (file.IsDirectory || localPath.EndsWith("\\") || localPath.EndsWith("/") || file.DownloadUrl.EndsWith("/")) continue;

                var request = new HttpRequestMessage(HttpMethod.Get, file.DownloadUrl);

                if (!string.IsNullOrEmpty(token))
                {
                    var authToken = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{token}:"));

                    request.Headers.Add("Authorization", $"Basic {authToken}");
                }

                using var response = await httpClient.SendAsync(request, HttpCompletionOption.ResponseHeadersRead);

                response.EnsureSuccessStatusCode();

                using var stream = await response.Content.ReadAsStreamAsync();

                using var fs = new FileStream(localPath, FileMode.Create, FileAccess.Write);

                await stream.CopyToAsync(fs);
            }

            AssetDatabase.Refresh(ImportAssetOptions.ForceSynchronousImport);

            Debug.Log($"Successfully downloaded {gameKey} resources ({files.Count} files).");
        }
    }
}