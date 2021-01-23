namespace TntSearch.Core.Services.Abstractions
{
    public class ConfigBase : IConfig
    {
        public virtual string DatabaseName { get; } = "tnt.db";

        public virtual bool InjectListTerminator { get; } = false;
    }
}
