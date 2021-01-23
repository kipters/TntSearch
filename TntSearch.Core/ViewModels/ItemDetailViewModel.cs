using MvvmCross.Commands;
using MvvmCross.ViewModels;
using System;
using System.Threading.Tasks;
using TntSearch.Core.Services.Abstractions;

namespace TntSearch.Core.ViewModels
{
    public class ItemDetailViewModel : MvxViewModel<SearchItem>
    {
        private SearchItem? _parameter;
        private readonly IDataRepository _repo;

        public ItemDetailViewModel(IDataRepository repo, IClipboardService clipboard)
        {
            _repo = repo;

            CopyMagnetCommand = new MvxCommand(() => clipboard.SetText(Magnet));
        }

        public override void Prepare(SearchItem item)
        {
            _parameter = item;
        }

        public override async Task Initialize()
        {
            await base.Initialize();

            if (_parameter is null)
            {
                throw new InvalidOperationException("Uninitialized viewmodel");
            }

            var item = _repo.GetItemById(_parameter.Id);
            Title = item.Title;
            Description = item.Description;
            Magnet = $"magnet:?xt=urn:btih:{item.Hash}";
        }

        public string Title { get; private set; } = string.Empty;
        public string Description { get; private set; } = string.Empty;
        public string Magnet { get; private set; } = string.Empty;

        public IMvxCommand CopyMagnetCommand { get; }
    }
}
