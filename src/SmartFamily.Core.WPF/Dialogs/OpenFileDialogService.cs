﻿using Microsoft.Win32;

using SmartFamily.Core.WPF.Contracts.Services;

using System;

namespace SmartFamily.Core.WPF.Dialogs
{
    public class OpenFileDialogService : IOpenFileDialogService
    {
        private readonly OpenFileDialog _openFileDialog = new();

        /// <inheritdoc/>
        public bool? ShowOpenDatabaseDialog(out string fileName)
        {
            fileName = String.Empty;
            _openFileDialog.Filter = "SmartFamily database (*.sfdb)|*.sfdb|All files (*.*)|*.*";
            _openFileDialog.Multiselect = false;
            var openFile = _openFileDialog.ShowDialog();
            if (_openFileDialog.FileName != null)
            {
                fileName = _openFileDialog.FileName;
            }
            return openFile;
        }
    }
}