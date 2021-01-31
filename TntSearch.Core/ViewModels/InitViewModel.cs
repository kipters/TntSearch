using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using System;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Threading.Tasks;
using TntSearch.Core.Services.Abstractions;

namespace TntSearch.Core.ViewModels
{
    public class InitViewModel : MvxViewModel
    {
        private readonly IFileService _fileService;
        private readonly IDataPackManager _dataPackManager;
        private bool _canExecuteInit = true;

        public InitViewModel(IFileService fileService, IDataPackManager dataPackManager, IMvxNavigationService navigationService)
        {
            _fileService = fileService;
            _dataPackManager = dataPackManager;
            InitializePackTask = MvxNotifyTask.Create(Task.FromException(new Exception()));
            PickDataPackCommand = new MvxAsyncCommand(PickFileAndInitPackAsync, () => _canExecuteInit);
            NavigateToMainViewModelCommand = new MvxAsyncCommand(async () =>
            {
                await navigationService.Close(this);
                await navigationService.Navigate<MainViewModel>();
            });
            _message = "Select a data pack file";
        }

        private async Task PickFileAndInitPackAsync()
        {
            using var file = await _fileService.PickFileAsync(new[] { "zip" }, true);

            await PerformPackInitAsync(file);
        }

        public async Task InitPackFromStreamAsync(Stream file)
        {
            try
            {
                _canExecuteInit = false;
                PickDataPackCommand.RaiseCanExecuteChanged();

                await PerformPackInitAsync(file);
            }
            finally
            {
                _canExecuteInit = true;
                PickDataPackCommand.RaiseCanExecuteChanged();
            }
        }

        private async Task PerformPackInitAsync(Stream file)
        {
            InitializePackTask = MvxNotifyTask.Create(InitPackAsync(file), ex => OnInitError(ex));
            await InitializePackTask.Task;
        }

        private async Task InitPackAsync(Stream file)
        {
            Message = "Loading pack";
            using var zip = new ZipArchive(file, ZipArchiveMode.Read, true);
            using var csvFile = zip.Entries.Single(e => e.FullName.EndsWith("csv")).Open();
            using var readmeFile = zip.Entries.Single(e => e.FullName.EndsWith("txt")).Open();

            await _dataPackManager.BuildDataPackAsync(readmeFile, csvFile);
            Message = "Load completed";
        }

        private void OnInitError(Exception ex)
        {

        }

        public MvxAsyncCommand PickDataPackCommand { get; }
        public MvxAsyncCommand NavigateToMainViewModelCommand { get; }

        private MvxNotifyTask _initializePackTask = null!;
        public MvxNotifyTask InitializePackTask { get => _initializePackTask; private set => SetProperty(ref _initializePackTask, value); }

        private string _message;
        public string Message { get => _message; private set => SetProperty(ref _message, value); }
    }
}
