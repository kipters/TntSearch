using IncrementalCollection.Core.Abstractions;
using IncrementalCollection.Uwp;
using MvvmCross;
using MvvmCross.Platforms.Uap.Core;
using MvvmCross.Platforms.Uap.Views;
using TntSearch.Core.Services;
using TntSearch.Core.Services.Abstractions;
using TntSearch.Core.ViewModels;
using TntSearch.Uwp.Services;
using TntSearch.Uwp.Views;

namespace TntSearch.Uwp
{
    public class Setup : MvxWindowsSetup<Core.App>
    {
        protected override void InitializeFirstChance()
        {
            base.InitializeFirstChance();

            var ioc = Mvx.IoCProvider;
            ioc.RegisterSingleton<IPathBuilder>(new UwpPathBuilder());
            ioc.RegisterSingleton<IFileService>(() => new UwpFileService());
            ioc.RegisterSingleton<IClipboardService>(() => new UwpClipboardService());
            ioc.RegisterSingleton<ISocialService>(() => new UwpSocialService());
        }

        protected override void InitializeLastChance()
        {
            base.InitializeLastChance();

            var ioc = Mvx.IoCProvider;
            ioc.RegisterSingleton<IIncrementalCollectionFactory>(new UwpIncrementalCollectionFactory());
            ioc.RegisterSingleton<IConfig>(new UwpConfig());
        }

        protected override IMvxStoreViewsContainer CreateStoreViewsContainer()
        {
            var container = base.CreateStoreViewsContainer();
            container.Add<SettingsViewModel, SettingsDialog>();
            return container;
        }
    }
}
