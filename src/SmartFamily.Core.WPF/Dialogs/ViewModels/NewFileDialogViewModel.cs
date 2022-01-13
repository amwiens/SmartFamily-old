using Prism.Commands;
using Prism.Mvvm;
using Prism.Services.Dialogs;

using SmartFamily.Core.WPF.Contracts.Services;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartFamily.Core.WPF.Dialogs.ViewModels
{
    /// <summary>
    /// New file dialog view model.
    /// </summary>
    public class NewFileDialogViewModel : BindableBase, IDialogAware
    {
        private readonly ISelectFolderDialogService _selectFolderDialogService;
        private DelegateCommand<string> _closeDialogCommand;
        private DelegateCommand _selectFolderCommand;

        private string _fileName;

        /// <summary>
        /// File name.
        /// </summary>
        public string FileName
        {
            get => _fileName;
            set => SetProperty(ref _fileName, value);
        }

        private string _fileLocation;

        /// <summary>
        /// File location.
        /// </summary>
        public string FileLocation
        {
            get => _fileLocation;
            set => SetProperty(ref _fileLocation, value);
        }

        /// <summary>
        /// Select folder command.
        /// </summary>
        public DelegateCommand SelectFolderCommand => _selectFolderCommand ?? new DelegateCommand(OnSelectFolder);

        /// <summary>
        /// Close dialog command.
        /// </summary>
        public DelegateCommand<string> CloseDialogCommand =>
            _closeDialogCommand ?? (_closeDialogCommand = new DelegateCommand<string>(CloseDialog));

        /// <summary>
        /// Title to show on the dialog window.
        /// </summary>
        public string Title => "New database";

        /// <summary>
        /// Action to be taken on close.
        /// </summary>
        public event Action<IDialogResult> RequestClose;

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="selectFolderDialogService">Select folder dialog service.</param>
        public NewFileDialogViewModel(ISelectFolderDialogService selectFolderDialogService)
        {
            _selectFolderDialogService = selectFolderDialogService;
        }

        /// <summary>
        /// Close dialog.
        /// </summary>
        /// <param name="parameter">Parameter to be passed in.</param>
        protected virtual void CloseDialog(string parameter)
        {
            ButtonResult result = ButtonResult.None;
            var parameters = new DialogParameters();

            if (parameter?.ToLower() == "true")
            {
                result = ButtonResult.OK;
                parameters.Add("FileName", FileName);
                parameters.Add("FileLocation", FileLocation);
            }
            else if (parameter?.ToLower() == "false")
            {
                result = ButtonResult.Cancel;
            }

            RaiseRequestClose(new DialogResult(result, parameters));
        }

        /// <summary>
        /// Raise the close request.
        /// </summary>
        /// <param name="dialogResult">Dialog result.</param>
        public virtual void RaiseRequestClose(IDialogResult dialogResult)
        {
            RequestClose?.Invoke(dialogResult);
        }

        /// <summary>
        /// Can the dialog be closed.
        /// </summary>
        /// <returns><c>true</c> if the dialog can be closed, otherwise <c>false</c>.</returns>
        public bool CanCloseDialog()
        {
            return true;
        }

        /// <summary>
        /// Action to be taken when the dialog is closed.
        /// </summary>
        public void OnDialogClosed()
        {
        }

        /// <summary>
        /// What happens when the dialog window is opened.
        /// </summary>
        /// <param name="parameters">Parameters.</param>
        public void OnDialogOpened(IDialogParameters parameters)
        {
        }

        /// <summary>
        /// Selects the folder.
        /// </summary>
        private void OnSelectFolder()
        {
            if (_selectFolderDialogService.ShowDialog(out string folderName) == true && !string.IsNullOrEmpty(folderName))
            {
                FileLocation = folderName;
            }
        }
    }
}