using System.Threading.Tasks;

namespace TntSearch.Core.Services.Abstractions
{
    public interface ISocialService
    {
        Task ShareUrl(string caption, string url);
    }
}
