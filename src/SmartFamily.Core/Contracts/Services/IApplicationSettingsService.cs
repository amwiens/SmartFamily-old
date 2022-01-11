namespace SmartFamily.Core.Contracts.Services
{
    /// <summary>
    /// Application settings service interface.
    /// </summary>
    public interface IApplicationSettingsService
    {
        /// <summary>
        /// Set setting.
        /// </summary>
        /// <param name="key">Key</param>
        /// <param name="value">Value</param>
        void SetSetting(string key, object value);

        /// <summary>
        /// Get setting.
        /// </summary>
        /// <typeparam name="T">Type of object</typeparam>
        /// <param name="key">Key.</param>
        /// <returns>Value.</returns>
        T GetSetting<T>(string key);
    }
}