using SmartFamily.Core.Models;

namespace SmartFamily.Contracts.Services
{
    /// <summary>
    /// Theme selector service interface.
    /// </summary>
    public interface IThemeSelectorService
    {
        /// <summary>
        /// Initializes the theme.
        /// </summary>
        void InitializeTheme();

        /// <summary>
        /// Sets the theme.
        /// </summary>
        /// <param name="theme">Theme</param>
        void SetTheme(AppTheme theme);

        /// <summary>
        /// Gets the current theme.
        /// </summary>
        /// <returns>Theme</returns>
        AppTheme GetCurrentTheme();
    }
}