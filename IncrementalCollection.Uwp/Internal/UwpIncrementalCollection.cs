using IncrementalCollection.Core.Abstractions;
using MvvmCross.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.UI.Xaml.Data;

namespace IncrementalCollection.Uwp.Internal
{
    internal class UwpIncrementalCollection<T> : MvxObservableCollection<T>, ISupportIncrementalLoading, IIncrementalCollection
    {
        private readonly Func<uint, Task<IncrementalLoadResult<T>>> _loadAction;

        public UwpIncrementalCollection(Func<uint, Task<IncrementalLoadResult<T>>> loadAction)
        {
            _loadAction = loadAction;
        }

        public UwpIncrementalCollection(Func<uint, Task<IncrementalLoadResult<T>>> loadAction, IEnumerable<T> items) : base(items)
        {
            _loadAction = loadAction;
        }

        public IAsyncOperation<LoadMoreItemsResult> LoadMoreItemsAsync(uint count) => LoadMoreItemsAsyncTask(count).AsAsyncOperation();

        private async Task<LoadMoreItemsResult> LoadMoreItemsAsyncTask(uint count)
        {
            var result = await _loadAction(count);
            var items = result.Data.ToList();

            HasMoreItems = result.HasMoreItems;
            AddRange(items);
            return new LoadMoreItemsResult { Count = (uint)items.Count };
        }

        public void Reset() => OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));

        public bool HasMoreItems { get; private set; } = true;

        protected override void ClearItems()
        {
            HasMoreItems = true;
            base.ClearItems();
        }
    }
}
