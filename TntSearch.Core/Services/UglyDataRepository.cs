using SQLitePCL;
using System;
using System.Collections.Generic;
using TntSearch.Core.Services.Abstractions;

using static SQLitePCL.raw;
using static SQLitePCL.Ugly.ugly;

namespace TntSearch.Core.Services
{
    public class UglyDataRepository : IDataRepository
    {
        private readonly IConfig _config;
        private readonly sqlite3 _db;

        public UglyDataRepository(IPathBuilder pathBuilder, IConfig config)
        {
            _config = config;
            var path = pathBuilder.GetDatabasePath(config.DatabaseName);
            _db = open_v2(path, SQLITE_OPEN_READONLY, null);
        }

        public TntItem GetItemById(int id)
        {
            const string query =
                @"SELECT 
                    date, hash, topic, post, title, description, size, category, username
                FROM Items i
                JOIN Authors a ON a.id = i.author_id
                WHERE i.id = ?";

            using var stmt = _db.prepare(query);
            stmt.bind_int(1, id);
            stmt.step_row();

            var rawDate = stmt.column_text(0);
            var date = DateTime.Parse(rawDate);
            var hash = stmt.column_text(1);
            var topic = stmt.column_int(2);
            var post = stmt.column_int(3);
            var title = stmt.column_text(4);
            var description = stmt.column_text(5);
            var size = stmt.column_int64(6);
            var category = stmt.column_int(7);
            var author = stmt.column_text(8);

            var item = new TntItem(date, hash, topic, post, author, title, description, size, category);

            return item;
        }

        public List<SearchItem> GetItems(string? term, int lastId, int count)
        {
            const string unfilteredQuery =
                "SELECT id, title, description FROM Items WHERE id > ?1 ORDER BY id LIMIT ?2";
            const string filteredQuery =
                "SELECT data_id, highlight(Search, 0, '**', '**'), highlight(Search, 1, '**', '**') FROM Search WHERE Search match ?3 AND data_id > ?1 ORDER BY data_id LIMIT ?2";
            
            var t = term?.Trim();

            var query = t switch
            {
                null => unfilteredQuery,
                _ when t.Length < 3 => unfilteredQuery,
                _ => filteredQuery
            };

            using var stmt = _db.prepare(query);

            stmt.bind_int(1, lastId);
            stmt.bind_int(2, count);

            if (query == filteredQuery)
            {
                stmt.bind_text(3, t);
            }

            var items = new List<SearchItem>();

            while (stmt.step() == SQLITE_ROW)
            {
                var id = stmt.column_int(0);
                var title = stmt.column_text(1);
                var description = stmt.column_text(2);

                var row = new SearchItem(id, title, description);
                items.Add(row);
            }

            if (items.Count == 0 && _config.InjectListTerminator)
                items.Add(new SearchTerminator());

            return items;
        }
    }
}
