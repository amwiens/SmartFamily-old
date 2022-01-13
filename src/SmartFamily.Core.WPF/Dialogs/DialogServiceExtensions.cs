using Prism.Services.Dialogs;

using System;

namespace SmartFamily.Core.WPF.Dialogs
{
    /// <summary>
    /// Dialog service extensions.
    /// </summary>
    public static class DialogServiceExtensions
    {
        /// <summary>
        /// Shows the notification dialog.
        /// </summary>
        /// <param name="dialogService">Dialog service.</param>
        /// <param name="message">Message to be displayed.</param>
        /// <param name="callBack">Action to be taken after the message is shown.</param>
        public static void ShowNotification(this IDialogService dialogService, string message, Action<IDialogResult> callBack)
        {
            dialogService.ShowDialog("NotificationDialog", new DialogParameters($"message={message}"), callBack);
        }

        public static void ShowNewFile(this IDialogService dialogService, Action<IDialogResult> callBack)
        {
            dialogService.ShowDialog("NewFileDialog", callBack);
        }
    }
}