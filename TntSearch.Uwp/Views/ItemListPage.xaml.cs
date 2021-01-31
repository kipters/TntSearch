using MvvmCross.Platforms.Uap.Presenters.Attributes;
using MvvmCross.Platforms.Uap.Views;
using TntSearch.Core.Extensions;
using TntSearch.Core.Services.Abstractions;
using TntSearch.Core.ViewModels;
using Windows.ApplicationModel.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace TntSearch.Uwp.Views
{
    public abstract class ItemListPageBase : MvxWindowsPage<ItemListViewModel> { }
    [MvxSplitViewPresentation(Position = SplitPanePosition.Pane)]
    public sealed partial class ItemListPage : ItemListPageBase
    {        
        public ItemListPage()
        {
            this.InitializeComponent();
        }

        private async void OnItemClick(object sender, ItemClickEventArgs e)
        {
            await ViewModel.ShowDetailsCommand.TryExecuteAsync(e.ClickedItem as SearchItem);
        }
    }
}
