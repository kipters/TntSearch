using CsvHelper;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TntSearch.Core.Model.Csv;
using TntSearch.Core.Services.Abstractions;

using static SQLitePCL.raw;
using static SQLitePCL.Ugly.ugly;

namespace TntSearch.Core.Services
{
    internal class DataPackManager : IDataPackManager
    {
        private readonly IPathBuilder _pathBuilder;
        private readonly IConfig _config;
        private readonly Regex _categoryRegex = new Regex(@"(\d+)\ *=\s*(.*)", RegexOptions.Compiled);
        public DataPackManager(IPathBuilder pathBuilder, IConfig config)
        {
            _pathBuilder = pathBuilder;
            _config = config;
        }

        public async Task BuildDataPackAsync(Stream readmeFile, Stream csvFile)
        {
            var dbPath = _pathBuilder.GetDatabasePath(_config.DatabaseName);
            File.Delete(dbPath);

            using var db = open_v2(dbPath, SQLITE_OPEN_READWRITE | SQLITE_OPEN_CREATE, null);

            BuildTables(db);
            await InsertCategories(db, readmeFile);
            var authors = await InsertItemsAsync(db, csvFile);
            InsertAuthors(db, authors);
            db.exec("INSERT INTO Metadata (key, value) VALUES ('version', ?)", "1");
        }

        private void InsertAuthors(SQLitePCL.sqlite3 db, Dictionary<string, int> authors)
        {
            using var stmt = db.prepare("INSERT INTO Authors (id, username) VALUES (?, ?)");
            db.exec("BEGIN TRANSACTION");

            foreach(var kvp in authors)
            {
                var user = kvp.Key;
                var id = kvp.Value;

                stmt.reset();

                stmt.bind_int(1, id);
                stmt.bind_text(2, user);

                stmt.step();
            }

            db.exec("COMMIT TRANSACTION");
        }

        private async Task<Dictionary<string, int>> InsertItemsAsync(SQLitePCL.sqlite3 db, Stream csvFile)
        {
            var authors = new Dictionary<string, int>();

            using var fileReader = new StreamReader(csvFile);
            using var csv = new CsvReader(fileReader, CultureInfo.InvariantCulture, true);
            using var stmt = db.prepare("INSERT INTO Items (date, hash, topic, post, author_id, size, category, title, description) VALUES (?, ?, ?, ?, ?, ?, ?, ?, ?)");
            using var fts = db.prepare("INSERT INTO Search (title, description, data_id) VALUES (?, ?, last_insert_rowid())");

            csv.Configuration.RegisterClassMap<ItemRowClassMap>();
            var records = csv.GetRecordsAsync<ItemRow>();

            db.exec("BEGIN TRANSACTION");
            await foreach (var item in records)
            {
                stmt.reset();
                fts.reset();

                var authorName = item.Author;
                var authorId = GetAuthorId(authorName);

                stmt.bind_text(1, item.Date);
                stmt.bind_text(2, item.Hash);
                stmt.bind_int(3, item.Topic);
                stmt.bind_int(4, item.Post);
                stmt.bind_int(5, authorId);
                stmt.bind_int64(6, item.Size);
                stmt.bind_int(7, item.Category);
                stmt.bind_text(8, item.Title);
                stmt.bind_text(9, item.Description);

                stmt.step();

                fts.bind_text(1, item.Title);
                fts.bind_text(2, item.Description);

                fts.step();
            }
            db.exec("COMMIT TRANSACTION");

            return authors;

            int GetAuthorId(string name)
            {
                if (authors.TryGetValue(name, out var id))
                {
                    return id;
                }

                var newId = authors.Count + 1;
                authors.Add(name, newId);
                return newId;
            }
        }

        private async Task InsertCategories(SQLitePCL.sqlite3 db, Stream readmeFile)
        {
            using var reader = new StreamReader(readmeFile);
            var readmeContent = await reader.ReadToEndAsync();
            
            using var stmt = db.prepare("INSERT INTO Categories (id, name) VALUES (?, ?)");
            db.exec("BEGIN TRANSACTION");

            var matches = _categoryRegex.Matches(readmeContent);

            foreach (Match match in matches)
            {
                stmt.reset();
                var id = match.Groups[1].Value;
                var name = match.Groups[2].Value;

                stmt.bind_int(1, int.Parse(id));
                stmt.bind_text(2, name);
                stmt.step();
            }

            db.exec("COMMIT TRANSACTION");
        }

        private static void BuildTables(SQLitePCL.sqlite3 db)
        {
            db.exec(
                @"CREATE TABLE Categories (
                    id INTEGER PRIMARY KEY,
                    name TEXT NOT NULL
                );"
            );

            db.exec(
                @"CREATE TABLE Items (
                    id INTEGER PRIMARY KEY,
                    date TEXT NOT NULL,
                    hash TEXT NOT NULL,
                    topic INTEGER NOT NULL,
                    post INTEGER NOT NULL,
                    author_id INTEGER NOT NULL,
                    size INTEGER NOT NULL,
                    category INTEGER NOT NULL,
                    title TEXT NOT NULL,
                    description TEXT NOT NULL
                );"
            );

            db.exec(
                @"CREATE VIRTUAL TABLE Search USING fts5(title, description, data_id UNINDEXED);"
            );

            db.exec(
                @"CREATE TABLE Authors (
                    id INTEGER PRIMARY KEY,
                    username TEXT NOT NULL
                );"
            );

            db.exec(
                @"CREATE TABLE Metadata (
                    key TEXT PRIMARY KEY,
                    value TEXT
                ) WITHOUT ROWID"
            );
        }

        public DataPackStatus GetDataPackStatus()
        {
            var dbPath = _pathBuilder.GetDatabasePath(_config.DatabaseName);

            return File.Exists(dbPath) switch
            {
                true => DataPackStatus.Ready,
                false => DataPackStatus.NeedsInitialization
            };
        }
    }
}
