namespace TntSearch.Core.Model.Csv
{
    public class ItemRow
    {
        public string Date { get; set; } = null!;
        public string Hash { get; set; } = null!;
        public int Topic { get; set; }
        public int Post { get; set; }
        public string Author { get; set; } = null!;
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public long Size { get; set; }
        public int Category { get; set; }
    }
}
