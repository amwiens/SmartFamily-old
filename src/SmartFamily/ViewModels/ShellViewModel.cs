using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using Prism.Services.Dialogs;

using SmartFamily.Contracts.Services;
using SmartFamily.Core;
using SmartFamily.Core.Constants;
using SmartFamily.Core.Contracts.Services;
using SmartFamily.Core.WPF.Dialogs;

using System.Windows;
using System.Windows.Input;

namespace SmartFamily.ViewModels
{
    public class ShellViewModel : BindableBase
    {
        private readonly IRegionManager _regionManager;
        private readonly IRightPaneService _rightPaneService;
        private readonly IDialogService _dialogService;
        private readonly IApplicationSettingsService _applicationSettingsService;
        private IRegionNavigationService _navigationService;
        private DelegateCommand _goBackCommand;
        private ICommand _menuFileOpenCommand;
        private ICommand _menuFileNewCommand;
        private ICommand _menuFileSettingsCommand;
        private ICommand _menuViewsDashboardCommand;
        private ICommand _loadedCommand;
        private ICommand _unloadedCommand;
        private ICommand _menuFileExitCommand;

        public DelegateCommand GoBackCommand => _goBackCommand ?? (_goBackCommand = new DelegateCommand(OnGoBack, CanGoBack));

        public ICommand MenuFileOpenCommand => _menuFileOpenCommand ?? (_menuFileOpenCommand = new DelegateCommand(OnMenuFileOpen));

        public ICommand MenuFileNewCommand => _menuFileNewCommand ?? (_menuFileNewCommand = new DelegateCommand(OnMenuFileNew));

        public ICommand MenuFileSettingsCommand => _menuFileSettingsCommand ?? (_menuFileSettingsCommand = new DelegateCommand(OnMenuFileSettings));

        public ICommand MenuViewsDashboardCommand => _menuViewsDashboardCommand ?? (_menuViewsDashboardCommand = new DelegateCommand(OnMenuViewsDashboard));

        public ICommand LoadedCommand => _loadedCommand ?? (_loadedCommand = new DelegateCommand(OnLoaded));

        public ICommand UnloadedCommand => _unloadedCommand ?? (_unloadedCommand = new DelegateCommand(OnUnloaded));

        public ICommand MenuFileExitCommand => _menuFileExitCommand ?? (_menuFileExitCommand = new DelegateCommand(OnMenuFileExit));

        public ShellViewModel(IRegionManager regionManager, IRightPaneService rightPaneService, IDialogService dialogService, IApplicationSettingsService applicationSettingsService)
        {
            _regionManager = regionManager;
            _rightPaneService = rightPaneService;
            _dialogService = dialogService;
            _applicationSettingsService = applicationSettingsService;
        }

        private void OnLoaded()
        {
            _navigationService = _regionManager.Regions[Regions.Main].NavigationService;
            _navigationService.Navigated += OnNavigated;

            if (_applicationSettingsService.GetSetting<bool>("OpenLastClosedFile"))
            {
                OnMenuFileOpen();
            }
        }

        private void OnUnloaded()
        {
            _navigationService.Navigated -= OnNavigated;
            _regionManager.Regions.Remove(Regions.Main);
            _rightPaneService.CleanUp();
        }

        private bool CanGoBack()
            => _navigationService != null && _navigationService.Journal.CanGoBack;

        private void OnGoBack()
            => _navigationService.Journal.GoBack();

        private bool RequestNavigate(string target)
        {
            if (_navigationService.CanNavigate(target))
            {
                _navigationService.RequestNavigate(target);
                return true;
            }

            return false;
        }

        private void RequestNavigateOnRightPane(string target)
            => _rightPaneService.OpenInRightPane(target);

        private void RequestNavigateAndCleanJournal(string target)
        {
            var navigated = RequestNavigate(target);
            if (navigated)
            {
                _navigationService.Journal.Clear();
            }
        }

        private void OnNavigated(object sender, RegionNavigationEventArgs e)
            => GoBackCommand.RaiseCanExecuteChanged();

        private void OnMenuFileExit()
            => Application.Current.Shutdown();

        private void OnMenuViewsDashboard()
            => RequestNavigateAndCleanJournal(PageKeys.Dashboard);

        private void OnMenuFileSettings()
            => RequestNavigateOnRightPane(PageKeys.Settings);

        private void OnMenuFileOpen()
        {
            RequestNavigateAndCleanJournal(PageKeys.Main);
            ApplicationSettings.OpenDatabase = "OpenFile";
        }

        private void OnMenuFileNew()
        {
            var message = "this is the message";

            _dialogService.ShowNotification(message, r =>
            {
                if (r.Result == ButtonResult.OK)
                {
                    ApplicationSettings.OpenDatabase = "TestDatabase";
                    RequestNavigateAndCleanJournal(PageKeys.Main);
                }
            });
        }
    }
}