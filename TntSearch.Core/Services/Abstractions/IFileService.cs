using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace TntSearch.Core.Services.Abstractions
{
    public interface IFileService
    {
        Task<Stream> PickFileAsync(IEnumerable<string> filter, bool readOnly);
    }
}
