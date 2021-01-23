using System;
using System.Collections.Generic;

namespace TntSearch.Core.Services.Abstractions
{
    public interface IDataRepository
    {
        List<SearchItem> GetItems(string? term, int lastId, int count);
        TntItem GetItemById(int id);

    }

    public record TntItem(DateTime Date, string Hash, int Topic, int Post, 
        string Author, string Title, string Description, long Size, int Category);

    public record SearchItem(int Id, string Title, string Description);
    public record SearchTerminator : SearchItem 
    {
        public SearchTerminator() : base(0, "TERMINATOR", "TERMINATOR")
        {

        }
    }
}
