using System;
using UnityEngine;

namespace NPR.Editor.Settings
{
    [Serializable]
    public abstract class TAssetRepository : ScriptableObject
    {
        public abstract string Name { get; }

        public abstract string RepositoryID { get; }

        public abstract string AssetPath { get; }

        public abstract int Priority { get; }
    }
}