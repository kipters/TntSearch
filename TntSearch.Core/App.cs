using MvvmCross;
using MvvmCross.IoC;
using MvvmCross.ViewModels;
using TntSearch.Core.Services;
using TntSearch.Core.Services.Abstractions;

namespace TntSearch.Core
{
    public class App : MvxApplication
    {
        public override void Initialize()
        {
            var ioc = Mvx.IoCProvider;
            ioc.RegisterSingleton<IConfig>(new ConfigBase());

            ioc.LazyConstructAndRegisterSingleton<IDataPackManager, DataPackManager>();
            ioc.LazyConstructAndRegisterSingleton<IDataRepository, UglyDataRepository>();

            RegisterCustomAppStart<TntAppStart>();
        }
    }
}
