using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using TntSearch.Core.Services.Abstractions;
using Windows.Storage.Pickers;

namespace TntSearch.Uwp.Services
{
    public class UwpFileService : IFileService
    {
        public async Task<Stream> PickFileAsync(IEnumerable<string> filter, bool readOnly)
        {
            var picker = new FileOpenPicker
            {
                ViewMode = PickerViewMode.List                
            };

            foreach (var f in filter)
            {
                picker.FileTypeFilter.Add("." + f);
            }

            var result = await picker.PickSingleFileAsync();
            
            return result switch
            {
                null => null,
                _ when readOnly => await result.OpenStreamForReadAsync(),
                _ => await result.OpenStreamForWriteAsync()
            };
        }
    }
}
