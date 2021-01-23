using IncrementalCollection.Core.Abstractions;
using MvvmCross.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace IncrementalCollection.Core.Internal
{
    internal class CoreIncrementalCollection<T> : MvxObservableCollection<T>, IIncrementalCollection
    {
        private readonly Func<uint, Task<IncrementalLoadResult<T>>> _loadAction;
        private readonly SemaphoreSlim _semaphore = new SemaphoreSlim(1, 1);

        public CoreIncrementalCollection(Func<uint, Task<IncrementalLoadResult<T>>> loadAction)
        {
            _loadAction = loadAction;
        }

        public CoreIncrementalCollection(Func<uint, Task<IncrementalLoadResult<T>>> loadAction, IEnumerable<T> items) : base(items)
        {
            _loadAction = loadAction;
        }

        public bool HasMoreItems { get; private set; } = true;
        public async Task<uint> LoadMoreItemsAsync(uint count)
        {
            await _semaphore.WaitAsync();

            if (HasMoreItems == false)
            {
                _semaphore.Release();
                return 0;
            }

            try
            {
                var result = await _loadAction(count);
                var items = result.Data.ToList();

                HasMoreItems = result.HasMoreItems;
                AddRange(items);
                return (uint)items.Count;
            }
            finally
            {
                _semaphore.Release();
            }
        }

        public void Reset() => OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
    }
}
