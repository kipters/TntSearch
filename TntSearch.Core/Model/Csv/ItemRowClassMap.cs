using CsvHelper.Configuration;

namespace TntSearch.Core.Model.Csv
{
    public class ItemRowClassMap : ClassMap<ItemRow>
    {
        public ItemRowClassMap()
        {
            Map(_ => _.Date).Name("DATA");
            Map(_ => _.Hash).Name("HASH");
            Map(_ => _.Topic).Name("TOPIC");
            Map(_ => _.Post).Name("POST");
            Map(_ => _.Author).Name("AUTORE");
            Map(_ => _.Title).Name("TITOLO");
            Map(_ => _.Description).Name("DESCRIZIONE");
            Map(_ => _.Size).Name("DIMENSIONE");
            Map(_ => _.Category).Name("CATEGORIA");
        }
    }
}
