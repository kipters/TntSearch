using IncrementalCollection.Core.Abstractions;
using IncrementalCollection.Uwp.Internal;
using MvvmCross.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IncrementalCollection.Uwp
{
    public class UwpIncrementalCollectionFactory : IIncrementalCollectionFactory
    {
        public MvxObservableCollection<T> GetCollection<T>(Func<uint, Task<IncrementalLoadResult<T>>> loadAction) =>
            new UwpIncrementalCollection<T>(loadAction);

        public MvxObservableCollection<T> GetCollection<T>(Func<uint, Task<IncrementalLoadResult<T>>> loadAction, IEnumerable<T> items) =>
            new UwpIncrementalCollection<T>(loadAction, items);
    }
}
