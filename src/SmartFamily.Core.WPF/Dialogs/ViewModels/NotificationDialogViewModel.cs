using Prism.Commands;
using Prism.Mvvm;
using Prism.Services.Dialogs;

using System;

namespace SmartFamily.Core.WPF.Dialogs.ViewModels
{
    /// <summary>
    /// Notification dialog view model.
    /// </summary>
    public class NotificationDialogViewModel : BindableBase, IDialogAware
    {
        private DelegateCommand<string> _closeDialogCommand;

        /// <summary>
        /// Close dialog command.
        /// </summary>
        public DelegateCommand<string> CloseDialogCommand =>
            _closeDialogCommand ?? (_closeDialogCommand = new DelegateCommand<string>(CloseDialog));

        private string _message;

        /// <summary>
        /// Message to show in the dialog window.
        /// </summary>
        public string Message
        {
            get { return _message; }
            set { SetProperty(ref _message, value); }
        }

        private string _title = "Notification";

        /// <summary>
        /// Title to show on the dialog window.
        /// </summary>
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        /// <summary>
        /// Action to be taken on close.
        /// </summary>
        public event Action<IDialogResult> RequestClose;

        /// <summary>
        /// Close dialog.
        /// </summary>
        /// <param name="parameter">Parameter to be passed in.</param>
        protected virtual void CloseDialog(string parameter)
        {
            ButtonResult result = ButtonResult.None;

            if (parameter?.ToLower() == "true")
                result = ButtonResult.OK;
            else if (parameter?.ToLower() == "false")
                result = ButtonResult.Cancel;

            RaiseRequestClose(new DialogResult(result));
        }

        /// <summary>
        /// Raise the close request.
        /// </summary>
        /// <param name="dialogResult"></param>
        public virtual void RaiseRequestClose(IDialogResult dialogResult)
        {
            RequestClose?.Invoke(dialogResult);
        }

        /// <summary>
        /// Can the dialog be closed.
        /// </summary>
        /// <returns><c>true</c> if the dialog can be closed, otherwise <c>false</c>.</returns>
        public virtual bool CanCloseDialog()
        {
            return true;
        }

        /// <summary>
        /// Action to be taken when the dialog is closed.
        /// </summary>
        public virtual void OnDialogClosed()
        {
        }

        /// <summary>
        /// What happens when the dialog window is opened.
        /// </summary>
        /// <param name="parameters">Parameters.</param>
        public virtual void OnDialogOpened(IDialogParameters parameters)
        {
            Message = parameters.GetValue<string>("message");
        }
    }
}