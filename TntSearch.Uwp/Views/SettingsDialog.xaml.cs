using MvvmCross.Platforms.Uap.Presenters.Attributes;
using MvvmCross.Platforms.Uap.Views;
using MvvmCross.ViewModels;
using MvvmCross.Views;
using TntSearch.Core.ViewModels;
using Windows.UI.Xaml.Controls;

namespace TntSearch.Uwp.Views
{
    [MvxDialogViewPresentation]
    public sealed partial class SettingsDialog : ContentDialog, IMvxWindowsView, IMvxWindowsContentDialog
    {
        public SettingsDialog()
        {
            this.InitializeComponent();
        }

        public IMvxViewModel ViewModel { get; set; }

        public void ClearBackStack()
        {
            throw new System.NotImplementedException();
        }

        private void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
        }

        private void ContentDialog_SecondaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
        }
    }
}
