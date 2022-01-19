using MahApps.Metro.Controls;

using Microsoft.Extensions.Logging;

using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using Prism.Services.Dialogs;

using SmartFamily.Core;
using SmartFamily.Core.Constants;
using SmartFamily.Core.Contracts.Services;
using SmartFamily.Core.WPF.Dialogs;

using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows.Input;

namespace SmartFamily.Main.ViewModels
{
    public class MainViewModel : BindableBase, INavigationAware
    {
        private readonly IRegionManager _regionManager;
        private readonly IApplicationSettingsService _applicationSettingsService;
        private readonly IDialogService _dialogService;
        private readonly ILogger<MainViewModel> _logger;

        private IRegionNavigationService _navigationService;
        private string _title;
        private HamburgerMenuItem _selectedMenuItem;
        private HamburgerMenuItem _selectedOptionsMenuItem;

        private ICommand _loadedCommand;
        private ICommand _unloadedCommand;
        private ICommand _menuItemInvokedCommand;
        private ICommand _optionsMenuItemInvokedCommand;

        /// <summary>
        /// Title
        /// </summary>
        public string Title
        {
            get { return _title; }
            private set { SetProperty(ref _title, value); }
        }

        /// <summary>
        /// Selected menu item.
        /// </summary>
        public HamburgerMenuItem SelectedMenuItem
        {
            get { return _selectedMenuItem; }
            set { SetProperty(ref _selectedMenuItem, value); }
        }

        /// <summary>
        /// Selected option menu item.
        /// </summary>
        public HamburgerMenuItem SelectedOptionsMenuItem
        {
            get { return _selectedOptionsMenuItem; }
            set { SetProperty(ref _selectedOptionsMenuItem, value); }
        }

        /// <summary>
        /// Menu items.
        /// </summary>
        // TODO: Change the icons and title for all HamburgerMenuItems here.
        public ObservableCollection<HamburgerMenuItem> MenuItems { get; } = new ObservableCollection<HamburgerMenuItem>()
        {
            new HamburgerMenuGlyphItem() { Label = "Dashboard", Glyph = "\uE8A5", Tag = PageKeys.Dashboard },
            new HamburgerMenuGlyphItem() { Label = "People", Glyph = "\uE8A5", Tag = PageKeys.People },
        };

        /// <summary>
        /// Option menu items.
        /// </summary>
        public ObservableCollection<HamburgerMenuItem> OptionMenuItems { get; } = new ObservableCollection<HamburgerMenuItem>()
        {
            new HamburgerMenuGlyphItem() { Label = "File Settings", Glyph = "\uE713", Tag = PageKeys.FileSettings },
        };

        public ICommand LoadedCommand => _loadedCommand ?? (_loadedCommand = new DelegateCommand(OnLoaded));

        public ICommand UnloadedCommand => _unloadedCommand ?? (_unloadedCommand = new DelegateCommand(OnUnloaded));

        public ICommand MenuItemInvokedCommand => _menuItemInvokedCommand ?? (_menuItemInvokedCommand = new DelegateCommand(OnMenuItemInvoked));

        public ICommand OptionsMenuItemInvokedCommand => _optionsMenuItemInvokedCommand ?? (_optionsMenuItemInvokedCommand = new DelegateCommand(OnOptionsMenuItemInvoked));

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="regionManager">Region manager.</param>
        /// <param name="applicationSettingsService">Application settings service.</param>
        /// <param name="dialogService">Dialg service.</param>
        public MainViewModel(IRegionManager regionManager,
            IApplicationSettingsService applicationSettingsService,
            IDialogService dialogService,
            ILogger<MainViewModel> logger)
        {
            _regionManager = regionManager;
            _applicationSettingsService = applicationSettingsService;
            _dialogService = dialogService;
            _logger = logger;
        }

        /// <summary>
        /// Runs when the view model is loaded.
        /// </summary>
        private void OnLoaded()
        {
            _navigationService = _regionManager.Regions[Regions.Hamburger].NavigationService;
            _navigationService.Navigated += OnNavigated;
            SelectedMenuItem = MenuItems.First();

            var fileProperties = new FileInfo(ApplicationSettings.OpenDatabase);
            Title = fileProperties.Name.Substring(0, fileProperties.Name.LastIndexOf('.'));
        }

        /// <summary>
        /// Runs when the view is unloaded.
        /// </summary>
        private void OnUnloaded()
        {
            _navigationService.Navigated -= OnNavigated;
        }

        /// <summary>
        /// On menu item invoked.
        /// </summary>
        private void OnMenuItemInvoked()
            => RequestNavigate(SelectedMenuItem.Tag?.ToString());

        /// <summary>
        /// On options menu item invoked.
        /// </summary>
        private void OnOptionsMenuItemInvoked()
            => RequestNavigate(SelectedOptionsMenuItem.Tag?.ToString());

        /// <summary>
        /// Request navigate.
        /// </summary>
        /// <param name="target">Target.</param>
        private void RequestNavigate(string target)
        {
            if (_navigationService.CanNavigate(target))
            {
                _navigationService.RequestNavigate(target);
            }
        }

        /// <summary>
        /// On navigated.
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="e">Event args.</param>
        private void OnNavigated(object? sender, RegionNavigationEventArgs e)
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
        }

        /// <summary>
        /// Is navigation target.
        /// </summary>
        /// <param name="navigationContext">Navigation context.</param>
        /// <returns><c>true</c> if the view model is a navigation target, otherwise <c>false</c>.</returns>
        public bool IsNavigationTarget(NavigationContext navigationContext)
            => true;

        /// <summary>
        /// On navigated to.
        /// </summary>
        /// <param name="navigationContext">Navigation context.</param>
        public void OnNavigatedTo(NavigationContext navigationContext)
        {
        }

        /// <summary>
        /// On navigated from.
        /// </summary>
        /// <param name="navigationContext">Navigation context.</param>
        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
        }
    }
}