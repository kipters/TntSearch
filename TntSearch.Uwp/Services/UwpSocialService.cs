using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Windows.ApplicationModel.DataTransfer;
using TntSearch.Core.Services.Abstractions;

namespace TntSearch.Uwp.Services
{
    public class UwpSocialService : ISocialService
    {
        private readonly Queue<IShareRequest> _queue;

        public UwpSocialService()
        {
            _queue = new Queue<IShareRequest>();
            DataTransferManager.GetForCurrentView().DataRequested += OnDataRequested;
        }

        private void OnDataRequested(DataTransferManager sender, DataRequestedEventArgs args)
        {
            if (!_queue.TryDequeue(out var request))
            {
                return;
            }

            switch (request)
            {
                case UrlShareRequest url:
                    args.Request.Data.SetWebLink(new Uri(url.Url));
                    args.Request.Data.Properties.Title = url.Caption;
                    break;

                default:
                    throw new NotSupportedException();
            }
        }

        public Task ShareUrl(string caption, string url)
        {
            _queue.Enqueue(new UrlShareRequest(caption, url));
            DataTransferManager.ShowShareUI();
            return Task.CompletedTask;
        }
    }

    internal interface IShareRequest
    {
    }

    internal record UrlShareRequest(string Caption, string Url) : IShareRequest;
}
