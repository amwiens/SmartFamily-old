using Prism.Commands;
using Prism.Mvvm;

using SmartFamily.Core.WPF.Contracts.Services;

using System.Windows.Input;

namespace SmartFamily.Main.ViewModels
{
    internal class FileSettingsViewModel : BindableBase
    {
        private readonly ISelectFolderDialogService _selectFolderDialogService;

        private string _selectedFolder;

        public string SelectedFolder
        {
            get => _selectedFolder;
            set => SetProperty(ref _selectedFolder, value);
        }

        private DelegateCommand _selectFolderCommand;

        public DelegateCommand SelectFolderCommand => _selectFolderCommand ?? new DelegateCommand(OnSelectFolder);

        public FileSettingsViewModel(ISelectFolderDialogService selectFolderDialogService)
        {
            _selectFolderDialogService = selectFolderDialogService;
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