using Windows.UI.Xaml;
using MvvmCross.Platforms.Uap.Presenters.Attributes;
using MvvmCross.Platforms.Uap.Views;
using TntSearch.Core.ViewModels;

namespace TntSearch.Uwp.Views
{
    public abstract class MainPageBase : MvxWindowsPage<MainViewModel> { }
    [MvxPagePresentation]
    public sealed partial class MainPage : MainPageBase
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            ClearBackStack();
        }
    }
}
