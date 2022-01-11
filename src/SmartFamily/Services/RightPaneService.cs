using MahApps.Metro.Controls;

using Prism.Regions;

using SmartFamily.Contracts.Services;
using SmartFamily.Core.Constants;

using System;
using System.Windows.Controls;

namespace SmartFamily.Services
{
    /// <summary>
    /// Right pane service.
    /// </summary>
    public class RightPaneService : IRightPaneService
    {
        private readonly IRegionManager _regionManager;
        private IRegionNavigationService _rightPaneNavigationService;
        private SplitView _splitView;

        /// <inheritdoc/>
        public event EventHandler PaneOpened;

        /// <inheritdoc/>
        public event EventHandler PaneClosed;

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="regionManager">Region manager.</param>
        public RightPaneService(IRegionManager regionManager)
        {
            _regionManager = regionManager;
        }

        /// <inheritdoc/>
        public void Initialize(SplitView splitView, ContentControl rightPaneContentControl)
        {
            _splitView = splitView;
            RegionManager.SetRegionName(rightPaneContentControl, Regions.RightPane);
            RegionManager.SetRegionManager(rightPaneContentControl, _regionManager);
            _rightPaneNavigationService = _regionManager.Regions[Regions.RightPane].NavigationService;
            _splitView.PaneClosed += OnPaneClosed;
        }

        /// <inheritdoc/>
        public void CleanUp()
        {
            _splitView.PaneClosed -= OnPaneClosed;
            _regionManager.Regions.Remove(Regions.RightPane);
        }

        /// <inheritdoc/>
        public void OpenInRightPane(string pageKey, NavigationParameters navigationParameters = null)
        {
            if (_rightPaneNavigationService.CanNavigate(pageKey))
            {
                _rightPaneNavigationService.RequestNavigate(pageKey, navigationParameters);
            }

            _splitView.IsPaneOpen = true;
            PaneOpened?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>
        /// Handles the pane closed event.
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="e">Event args.</param>
        private void OnPaneClosed(object? sender, EventArgs e)
            => PaneClosed?.Invoke(sender, e);
    }
}