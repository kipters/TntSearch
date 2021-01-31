using MvvmCross.Platforms.Uap.Presenters.Attributes;
using MvvmCross.Platforms.Uap.Views;
using System.Windows.Input;
using TntSearch.Core.ViewModels;
using Windows.Foundation.Metadata;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;

namespace TntSearch.Uwp.Views
{
    public abstract class ItemDetailPageBase : MvxWindowsPage<ItemDetailViewModel> { }
    [MvxSplitViewPresentation(Position = SplitPanePosition.Content)]
    public sealed partial class ItemDetailPage : ItemDetailPageBase
    {
        public ItemDetailPage()
        {
            this.InitializeComponent();
        }

        private void OnLoaded(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            var uiCommandPresent = ApiInformation.IsTypePresent("Windows.UI.Xaml.Input.StandardUICommand");

            var copyFlyoutItem = BuildFlyoutItem(ViewModel.CopyMagnetCommand, StandardUICommandKind.Copy, "Copy");
            var shareFlyoutItem = BuildFlyoutItem(ViewModel.ShareMagnetCommand, StandardUICommandKind.Share, "Share");

            MagnetLink.ContextFlyout = new MenuFlyout
            {
                Items = { copyFlyoutItem, shareFlyoutItem }
            };

            MenuFlyoutItem BuildFlyoutItem(ICommand command, StandardUICommandKind commandKind, string text)
            {
                return uiCommandPresent switch
                {
                    true => new MenuFlyoutItem
                    {
                        Command = new StandardUICommand(commandKind)
                        {
                            Command = command
                        }
                    },
                    false => new MenuFlyoutItem
                    {
                        Command = command,
                        Text = text
                    }
                };
            }
        }
    }
}
