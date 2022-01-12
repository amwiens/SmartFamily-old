using MahApps.Metro.Controls;

using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using Prism.Services.Dialogs;

using SmartFamily.Core;
using SmartFamily.Core.Constants;
using SmartFamily.Core.Contracts.Services;
using SmartFamily.Core.WPF.Dialogs;

using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace SmartFamily.Main.ViewModels
{
    public class MainViewModel : BindableBase, INavigationAware
    {
        private readonly IRegionManager _regionManager;
        private readonly IApplicationSettingsService _applicationSettingsService;
        private readonly IDialogService _dialogService;
        private IRegionNavigationService _navigationService;
        private string _title;
        private HamburgerMenuItem _selectedMenuItem;
        private HamburgerMenuItem _selectedOptionsMenuItem;
        private DelegateCommand _goBackCommand;
        private ICommand _loadedCommand;
        private ICommand _unloadedCommand;
        private ICommand _menuItemInvokedCommand;
        private ICommand _optionsMenuItemInvokedCommand;

        public string Title
        {
            get { return _title; }
            private set { SetProperty(ref _title, value); }
        }

        public HamburgerMenuItem SelectedMenuItem
        {
            get { return _selectedMenuItem; }
            set { SetProperty(ref _selectedMenuItem, value); }
        }

        public HamburgerMenuItem SelectedOptionsMenuItem
        {
            get { return _selectedOptionsMenuItem; }
            set { SetProperty(ref _selectedOptionsMenuItem, value); }
        }

        // TODO: Change the icons and title for all HamburgerMenuItems here.
        public ObservableCollection<HamburgerMenuItem> MenuItems { get; } = new ObservableCollection<HamburgerMenuItem>()
        {
            new HamburgerMenuGlyphItem() { Label = "Dashboard", Glyph = "\uE8A5", Tag = PageKeys.Dashboard },
        };

        public ObservableCollection<HamburgerMenuItem> OptionMenuItems { get; } = new ObservableCollection<HamburgerMenuItem>()
        {
            new HamburgerMenuGlyphItem() { Label = "File Settings", Glyph = "\uE713", Tag = PageKeys.FileSettings },
        };

        public DelegateCommand GoBackCommand => _goBackCommand ?? (_goBackCommand = new DelegateCommand(OnGoBack, CanGoBack));

        public ICommand LoadedCommand => _loadedCommand ?? (_loadedCommand = new DelegateCommand(OnLoaded));

        public ICommand UnloadedCommand => _unloadedCommand ?? (_unloadedCommand = new DelegateCommand(OnUnloaded));

        public ICommand MenuItemInvokedCommand => _menuItemInvokedCommand ?? (_menuItemInvokedCommand = new DelegateCommand(OnMenuItemInvoked));

        public ICommand OptionsMenuItemInvokedCommand => _optionsMenuItemInvokedCommand ?? (_optionsMenuItemInvokedCommand = new DelegateCommand(OnOptionsMenuItemInvoked));

        public MainViewModel(IRegionManager regionManager, IApplicationSettingsService applicationSettingsService, IDialogService dialogService)
        {
            _regionManager = regionManager;
            _applicationSettingsService = applicationSettingsService;
            _dialogService = dialogService;
        }

        private void OnLoaded()
        {
            _navigationService = _regionManager.Regions[Regions.Hamburger].NavigationService;
            _navigationService.Navigated += OnNavigated;
            SelectedMenuItem = MenuItems.First();

            Title = ApplicationSettings.OpenDatabase;
        }

        private void OnUnloaded()
        {
            _navigationService.Navigated -= OnNavigated;
        }

        private bool CanGoBack()
            => _navigationService != null && _navigationService.Journal.CanGoBack;

        private void OnGoBack()
            => _navigationService.Journal.GoBack();

        private void OnMenuItemInvoked()
            => RequestNavigate(SelectedMenuItem.Tag?.ToString());

        private void OnOptionsMenuItemInvoked()
            => RequestNavigate(SelectedOptionsMenuItem.Tag?.ToString());

        private void RequestNavigate(string target)
        {
            if (_navigationService.CanNavigate(target))
            {
                _navigationService.RequestNavigate(target);
            }
        }

        private void OnNavigated(object sender, RegionNavigationEventArgs e)
        {
            var item = MenuItems
                .OfType<HamburgerMenuItem>()
                .FirstOrDefault(i => e.Uri.ToString() == i.Tag?.ToString());
            if (item != null)
            {
                SelectedMenuItem = item;
            }
            else
            {
                SelectedOptionsMenuItem = OptionMenuItems
                    .OfType<HamburgerMenuItem>()
                    .FirstOrDefault(i => e.Uri.ToString() == i.Tag?.ToString());
            }

            GoBackCommand.RaiseCanExecuteChanged();
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
            if (_applicationSettingsService.GetSetting<bool>("AskForBackup"))
            {
                var message = "Would you like to backup this database?";

                _dialogService.ShowNotification(message, r =>
                {
                    if (r.Result == ButtonResult.OK)
                    {
                    }
                });
            }
        }
    }
}