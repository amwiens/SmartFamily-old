using MahApps.Metro.Controls;

using Prism.Regions;

using System;
using System.Windows.Controls;

namespace SmartFamily.Contracts.Services
{
    /// <summary>
    /// Right pane service interface.
    /// </summary>
    public interface IRightPaneService
    {
        /// <summary>
        /// Handles the pane opened event.
        /// </summary>
        event EventHandler PaneOpened;

        /// <summary>
        /// Handles the pane closed event.
        /// </summary>
        event EventHandler PaneClosed;

        /// <summary>
        /// Opens the page in the right pane.
        /// </summary>
        /// <param name="pageKey">Page key.</param>
        /// <param name="navigationParameters">Navigation parameters.</param>
        void OpenInRightPane(string pageKey, NavigationParameters navigationParameters = null);

        /// <summary>
        /// Initializes the right pane.
        /// </summary>
        /// <param name="splitView">Split view.</param>
        /// <param name="rightPaneContentControl">Right pane control.</param>
        void Initialize(SplitView splitView, ContentControl rightPaneContentControl);

        /// <summary>
        /// Cleans up the right pane.
        /// </summary>
        void CleanUp();
    }
}