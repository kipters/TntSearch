using System.IO;
using TntSearch.Core.Services.Abstractions;
using Windows.ApplicationModel;
using Windows.Storage;

namespace TntSearch.Uwp.Services
{
    public class UwpPathBuilder : IPathBuilder
    {
        public string GetAssetPath(string relativePath)
        {
            var basePath = Package.Current.InstalledPath;
            var path = Path.Combine(basePath, relativePath);
            return path;
        }

        public string GetDatabasePath(string relativePath)
        {
            var basePath = ApplicationData.Current.LocalCacheFolder.Path;
            var path = Path.Combine(basePath, relativePath);
            return path;
        }

        public string GetDataPath(string relativePath)
        {
            var basePath = ApplicationData.Current.LocalFolder.Path;
            var path = Path.Combine(basePath, relativePath);
            return path;
        }
    }
}
