using Microsoft.Extensions.Logging;

using Prism.Commands;
using Prism.Mvvm;

using SmartFamily.Core.WPF.Contracts.Services;

namespace SmartFamily.Main.ViewModels
{
    internal class FileSettingsViewModel : BindableBase
    {
        private readonly ISelectFolderDialogService _selectFolderDialogService;
        private readonly ILogger<FileSettingsViewModel> _logger;

        private string _selectedFolder;

        public string SelectedFolder
        {
            get => _selectedFolder;
            set => SetProperty(ref _selectedFolder, value);
        }

        private DelegateCommand _selectFolderCommand;

        public DelegateCommand SelectFolderCommand => _selectFolderCommand ?? new DelegateCommand(OnSelectFolder);

        public FileSettingsViewModel(ISelectFolderDialogService selectFolderDialogService,
            ILogger<FileSettingsViewModel> logger)
        {
            _selectFolderDialogService = selectFolderDialogService;
            _logger = logger;
        }

        private void OnSelectFolder()
        {
            if (_selectFolderDialogService.ShowDialog(out string folderName) == true && !string.IsNullOrEmpty(folderName))
            {
                _selectedFolder = folderName;
            }
        }
    }
}