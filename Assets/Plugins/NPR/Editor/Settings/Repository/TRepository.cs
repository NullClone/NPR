using System;
using System.IO;
using UnityEngine;

namespace NPR.Editor.Settings
{
    [Serializable]
    public abstract class TRepository<T> : IRepository where T : class, IRepository, new()
    {
        // Fields

        protected static T Instance;


        // Properties

        public string AssetDirectory => PathUtils.DATA_DIRECTORY;

        public abstract string RepositoryID { get; }

        public static T Get
        {
            get
            {
                if (Instance != null) return Instance;

                var repository = new T();

                var path = Path.Combine(repository.RepositoryID, repository.AssetDirectory);

                var assetRepository = Resources.Load<AssetRepository<T>>(path);

                if (assetRepository != null) repository = assetRepository.Get();

                Instance = repository;

                return Instance;
            }
        }
    }
}