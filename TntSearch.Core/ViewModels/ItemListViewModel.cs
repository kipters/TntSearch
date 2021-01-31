using IncrementalCollection.Core.Abstractions;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TntSearch.Core.Services.Abstractions;

namespace TntSearch.Core.ViewModels
{
    public class ItemListViewModel : MvxViewModel
    {
        private readonly IDataRepository _repo;
        private readonly IIncrementalCollectionFactory _incrementalFactory;
        
        public ItemListViewModel(IMvxNavigationService navigationService
            , IDataRepository repo
            , IIncrementalCollectionFactory incrementalFactory
            )
        {
            //_navigationService = navigationService;
            _repo = repo;
            _incrementalFactory = incrementalFactory;

            Items = _incrementalFactory.GetCollection(OnLoadMoreItems);
            ShowSettingsCommand = new MvxAsyncCommand(async () => await navigationService.Navigate<SettingsViewModel>());
            ShowDetailsCommand = new MvxAsyncCommand<SearchItem>(async si => await navigationService.Navigate<ItemDetailViewModel, SearchItem>(param: si));
        }

        private int _lastId = 0;
        private Task<IncrementalLoadResult<SearchItem>> OnLoadMoreItems(uint arg)
        {
            var items = _repo.GetItems(SearchTerm, _lastId, (int)arg);
            Debug.WriteLine($"{SearchTerm} {_lastId} {items.Count}");
            _lastId = items.LastOrDefault()?.Id ?? 0;
            var result = new IncrementalLoadResult<SearchItem>(items.Count >= arg, items);
            return Task.FromResult(result);
        }

        public MvxObservableCollection<SearchItem> Items { get; private set; }

        private string _searchTerm = string.Empty;
        public string SearchTerm
        {
            get => _searchTerm;
            set
            {
                SetProperty(ref _searchTerm, value, changed =>
                {
                    if (!changed)
                    {
                        return;
                    }
                    _lastId = 0;
                    //Items.Clear();
                    //_resetTimer.Change(300, Timeout.Infinite);
                    Items = _incrementalFactory.GetCollection(OnLoadMoreItems);
                    RaisePropertyChanged(nameof(Items));
                });
                
            }
        }

        public IMvxAsyncCommand ShowSettingsCommand { get; }
        public IMvxAsyncCommand<SearchItem> ShowDetailsCommand { get; }

    }
}
