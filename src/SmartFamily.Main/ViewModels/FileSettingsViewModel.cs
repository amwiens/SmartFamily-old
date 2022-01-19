using Microsoft.Extensions.Logging;

using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;

using SmartFamily.Core.Constants;
using SmartFamily.Core.WPF.Contracts.Services;

namespace SmartFamily.Main.ViewModels
{
    /// <summary>
    /// File settings view model.
    /// </summary>
    public class FileSettingsViewModel : BindableBase, INavigationAware
    {
        private readonly IRegionManager _regionManager;
        private readonly ISelectFolderDialogService _selectFolderDialogService;
        private readonly ILogger<FileSettingsViewModel> _logger;

        private IRegionNavigationService _navigationService;

        private string _selectedFolder;

        private DelegateCommand _selectFolderCommand;

        /// <summary>
        /// Selected folder.
        /// </summary>
        public string SelectedFolder
        {
            get => _selectedFolder;
            set => SetProperty(ref _selectedFolder, value);
        }

        /// <summary>
        /// Select folder command.
        /// </summary>
        public DelegateCommand SelectFolderCommand => _selectFolderCommand ?? (_selectFolderCommand = new DelegateCommand(OnSelectFolder));

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="regionManager">Region manager.</param>
        /// <param name="selectFolderDialogService">Select folder dialog service.</param>
        /// <param name="logger">Logger.</param>
        public FileSettingsViewModel(IRegionManager regionManager,
            ISelectFolderDialogService selectFolderDialogService,
            ILogger<FileSettingsViewModel> logger)
        {
            _regionManager = regionManager;
            _selectFolderDialogService = selectFolderDialogService;
            _logger = logger;
        }

        /// <summary>
        /// On navigated.
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="e">Event args.</param>
        private void OnNavigated(object? sender, RegionNavigationEventArgs e)
        {
        }

        /// <summary>
        /// Is navigation target.
        /// </summary>
        /// <param name="navigationContext">Navigation context.</param>
        /// <returns><c>true</c> if it can be navigated to, otherwise <c>false</c>.</returns>
        public bool IsNavigationTarget(NavigationContext navigationContext)
            => true;

        /// <summary>
        /// On navigated to.
        /// </summary>
        /// <param name="navigationContext">Navigation context.</param>
        public async void OnNavigatedTo(NavigationContext navigationContext)
        {
            _logger.LogInformation("FileSettingsViewModel: Navigated to.");

            _navigationService = _regionManager.Regions[Regions.Hamburger].NavigationService;
            _navigationService.Navigated += OnNavigated;
        }

        /// <summary>
        /// On navigated from.
        /// </summary>
        /// <param name="navigationContext">Navigation context.</param>
        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
            _logger.LogInformation("FileSettingsViewModel: Navigated from.");
        }

        /// <summary>
        /// On select folder.
        /// </summary>
        private void OnSelectFolder()
        {
            if (_selectFolderDialogService.ShowDialog(out string folderName) == true && !string.IsNullOrEmpty(folderName))
            {
                SelectedFolder = folderName;
            }
        }
    }
}