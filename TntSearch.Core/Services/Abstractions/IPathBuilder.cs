namespace TntSearch.Core.Services.Abstractions
{
    public interface IPathBuilder
    {
        string GetAssetPath(string relativePath);
        string GetDataPath(string relativePath);
        string GetDatabasePath(string relativePath);
    }
}
