using System;

namespace NPR.Editor.Settings
{
    [Serializable]
    public class ResourceRepository : TRepository<ResourceRepository>
    {
        public override string RepositoryID => "Resource";
    }
}