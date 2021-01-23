using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using System.Threading.Tasks;
using TntSearch.Core.Services.Abstractions;
using TntSearch.Core.ViewModels;

namespace TntSearch.Core
{
    internal class TntAppStart : MvxAppStart
    {
        private readonly IDataPackManager _dataPackManager;

        public TntAppStart(IMvxApplication application, IMvxNavigationService navigationService
            , IDataPackManager dataPackManager)
            : base(application, navigationService)
        {
            _dataPackManager = dataPackManager;
        }

        protected override async Task NavigateToFirstViewModel(object hint = null)
        {
            var navigationTask = _dataPackManager.GetDataPackStatus() switch
            {
                DataPackStatus.Ready => NavigationService.Navigate<MainViewModel>(),
                DataPackStatus.NeedsSchemaMigration => NavigationService.Navigate<MigrationViewModel, bool>(false),
                DataPackStatus.NeedsDataMigration => NavigationService.Navigate<MigrationViewModel, bool>(true),
                DataPackStatus.NeedsInitialization => NavigationService.Navigate<InitViewModel>(),
                _ => throw new System.NotImplementedException()
            };

            await navigationTask;
        }
    }
}
