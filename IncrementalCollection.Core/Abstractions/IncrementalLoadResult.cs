using System.Collections.Generic;

namespace IncrementalCollection.Core.Abstractions
{
    public readonly struct IncrementalLoadResult<T>
    {
        public IncrementalLoadResult(bool hasMoreItems, IEnumerable<T> data)
        {
            HasMoreItems = hasMoreItems;
            Data = data;
        }

        public bool HasMoreItems { get; }
        public IEnumerable<T> Data { get; }
    }
}
