using TntSearch.Core.Services.Abstractions;
using Windows.ApplicationModel.DataTransfer;

namespace TntSearch.Uwp.Services
{
    public class UwpClipboardService : IClipboardService
    {
        public void SetText(string text)
        {
            var request = new DataPackage();
            request.SetText(text);
            Clipboard.SetContent(request);
        }
    }
}
