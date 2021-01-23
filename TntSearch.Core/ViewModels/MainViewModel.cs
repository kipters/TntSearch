using MvvmCross.Navigation;
using MvvmCross.ViewModels;

namespace TntSearch.Core.ViewModels
{
    public class MainViewModel : MvxViewModel
    {
        private readonly IMvxNavigationService _navigationService;

        public MainViewModel(IMvxNavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        public override void ViewAppeared()
        {
            base.ViewAppeared();

            _navigationService.Navigate<ItemListViewModel>();
        }
    }
}
