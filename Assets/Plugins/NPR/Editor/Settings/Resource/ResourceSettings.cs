using UnityEngine;

namespace NPR.Editor.Settings
{
    [CreateAssetMenu(menuName = "NPR/Settings/Resource", order = 10)]
    public class ResourceSettings : AssetRepository<ResourceRepository>
    {
        public override string Name => "Resources";
    }
}