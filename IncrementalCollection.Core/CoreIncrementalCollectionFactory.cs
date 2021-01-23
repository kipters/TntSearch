using IncrementalCollection.Core.Abstractions;
using IncrementalCollection.Core.Internal;
using MvvmCross.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IncrementalCollection.Core
{
    public class CoreIncrementalCollectionFactory : IIncrementalCollectionFactory
    {
        public MvxObservableCollection<T> GetCollection<T>(Func<uint, Task<IncrementalLoadResult<T>>> loadAction) => new CoreIncrementalCollection<T>(loadAction);

        public MvxObservableCollection<T> GetCollection<T>(Func<uint, Task<IncrementalLoadResult<T>>> loadAction, IEnumerable<T> items) =>
            new CoreIncrementalCollection<T>(loadAction, items);
    }
}
