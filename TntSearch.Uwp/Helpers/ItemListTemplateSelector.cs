using TntSearch.Core.Services.Abstractions;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace TntSearch.Uwp.Helpers
{
    public class ItemListTemplateSelector : DataTemplateSelector
    {
        public DataTemplate Normal { get; set; }
        public DataTemplate Terminator { get; set; }

        protected override DataTemplate SelectTemplateCore(object item)
        {
            return item switch
            {
                SearchTerminator => Terminator,
                _ => Normal
            };
        }
    }
}
