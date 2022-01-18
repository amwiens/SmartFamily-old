using SmartFamily.Contracts.Services;
using SmartFamily.Core;
using SmartFamily.Core.Contracts.Services;
using SmartFamily.Core.Models;

using System;
using System.Collections.Generic;
using System.IO;

namespace SmartFamily.Services
{
    internal class RecentFilesService : IRecentFilesService
    {
        private readonly IFileService _fileService;
        private readonly AppConfig _appConfig;
        private readonly string _localAppData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="fileService">File service</param>
        /// <param name="appConfig">Application config</param>
        public RecentFilesService(IFileService fileService, AppConfig appConfig)
        {
            _fileService = fileService;
            _appConfig = appConfig;
        }

        /// <inheritdoc/>
        public void PersistData()
        {
            if (ApplicationSettings.RecentFiles != null)
            {
                var folderPath = Path.Combine(_localAppData, _appConfig.ConfigurationsFolder);
                var fileName = _appConfig.RecentFilesFileName;
                _fileService.Save(folderPath, fileName, ApplicationSettings.RecentFiles);
            }
        }

        /// <inheritdoc/>
        public void RestoreData()
        {
            var folderPath = Path.Combine(_localAppData, _appConfig.ConfigurationsFolder);
            var fileName = _appConfig.RecentFilesFileName;
            ApplicationSettings.RecentFiles = _fileService.Read<List<RecentFile>>(folderPath, fileName);
        }
    }
}