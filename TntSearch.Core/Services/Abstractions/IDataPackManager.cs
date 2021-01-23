using System;
using System.IO;
using System.Threading.Tasks;

namespace TntSearch.Core.Services.Abstractions
{
    public enum DataPackStatus
    {
        Ready,
        NeedsSchemaMigration,
        NeedsDataMigration,
        NeedsInitialization
    }

    public interface IDataPackManager
    {
        DataPackStatus GetDataPackStatus();
        Task BuildDataPackAsync(Stream readmeFile, Stream csvFile);
    }
}
