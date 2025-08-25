namespace NPR.Editor.Settings
{
    public interface IRepository
    {
        public string AssetDirectory { get; }

        public string RepositoryID { get; }
    }
}