using TntSearch.Core.Services.Abstractions;

namespace TntSearch.Uwp.Services
{
    public class UwpConfig : ConfigBase
    {
        public override bool InjectListTerminator { get; } = true;
    }
}
