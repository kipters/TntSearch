using MvvmCross.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IncrementalCollection.Core.Abstractions
{
    public interface IIncrementalCollectionFactory
    {
        MvxObservableCollection<T> GetCollection<T>(Func<uint, Task<IncrementalLoadResult<T>>> loadAction);
        MvxObservableCollection<T> GetCollection<T>(Func<uint, Task<IncrementalLoadResult<T>>> loadAction, IEnumerable<T> items);
    }
}
