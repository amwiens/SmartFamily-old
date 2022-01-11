namespace SmartFamily.Contracts.Services
{
    /// <summary>
    /// System service interface.
    /// </summary>
    public interface ISystemService
    {
        /// <summary>
        /// Opens the url in the default web browser.
        /// </summary>
        /// <param name="url">Url</param>
        void OpenInWebBrowser(string url);
    }
}