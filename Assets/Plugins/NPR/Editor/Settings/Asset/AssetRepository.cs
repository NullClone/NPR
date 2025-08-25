using System;
using UnityEngine;

namespace NPR.Editor.Settings
{
    [Serializable]
    public abstract class AssetRepository<T> : TAssetRepository where T : class, IRepository, new()
    {
        // Fields

        [SerializeReference, HideInInspector] private IRepository _repository = new T();


        // Properties

        public override string AssetPath => _repository.AssetDirectory;

        public override string RepositoryID => _repository.RepositoryID;

        public override int Priority => 10;


        // Methods

        public T Get() => _repository as T;
    }
}