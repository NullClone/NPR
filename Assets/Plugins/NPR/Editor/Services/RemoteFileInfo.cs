namespace NPR.Editor.Services
{
    public class RemoteFileInfo
    {
        public string RelativePath { get; set; }

        public string DownloadUrl { get; set; }

        public long Size { get; set; }

        public string ETag { get; set; }

        public bool IsDirectory { get; set; }
    }
}