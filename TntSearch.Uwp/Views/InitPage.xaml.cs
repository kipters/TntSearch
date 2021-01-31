using MvvmCross.Platforms.Uap.Views;
using TntSearch.Core.ViewModels;
using Windows.UI.Xaml.Controls;

namespace TntSearch.Uwp.Views
{
    public abstract class InitPageBase : MvxWindowsPage<InitViewModel> { }

    public sealed partial class InitPage : InitPageBase
    {
        public InitPage()
        {
            this.InitializeComponent();
        }
    }
}
