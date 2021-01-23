namespace IncrementalCollection.Core.Abstractions
{
    public interface IIncrementalCollection
    {
        bool HasMoreItems { get; }
        void Reset();
    }
}
