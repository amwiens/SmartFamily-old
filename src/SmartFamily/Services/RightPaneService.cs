using MahApps.Metro.Controls;

using Prism.Regions;

using SmartFamily.Contracts.Services;
using SmartFamily.Core.Constants;

using System;
using System.Windows.Controls;

namespace SmartFamily.Services
{
    public class RightPaneService : IRightPaneService
    {
        private readonly IRegionManager _regionManager;
        private IRegionNavigationService _rightPaneNavigationService;
        private SplitView _splitView;

        public event EventHandler PaneOpened;

        public event EventHandler PaneClosed;

        public RightPaneService(IRegionManager regionManager)
        {
            _regionManager = regionManager;
        }

        public void Initialize(SplitView splitView, ContentControl rightPaneContentControl)
        {
            _splitView = splitView;
            RegionManager.SetRegionName(rightPaneContentControl, Regions.RightPane);
            RegionManager.SetRegionManager(rightPaneContentControl, _regionManager);
            _rightPaneNavigationService = _regionManager.Regions[Regions.RightPane].NavigationService;
            _splitView.PaneClosed += OnPaneClosed;
        }

        public void CleanUp()
        {
            _splitView.PaneClosed -= OnPaneClosed;
            _regionManager.Regions.Remove(Regions.RightPane);
        }

        public void OpenInRightPane(string pageKey, NavigationParameters navigationParameters = null)
        {
            if (_rightPaneNavigationService.CanNavigate(pageKey))
            {
                _rightPaneNavigationService.RequestNavigate(pageKey, navigationParameters);
            }

            _splitView.IsPaneOpen = true;
            PaneOpened?.Invoke(this, EventArgs.Empty);
        }

        private void OnPaneClosed(object? sender, EventArgs e)
            => PaneClosed?.Invoke(sender, e);
    }
}