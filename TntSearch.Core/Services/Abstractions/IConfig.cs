namespace TntSearch.Core.Services
{
    public interface IConfig
    {
        string DatabaseName { get; }
        bool InjectListTerminator { get; }
    }
}
