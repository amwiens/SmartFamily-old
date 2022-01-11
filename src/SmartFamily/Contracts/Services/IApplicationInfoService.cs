using System;

namespace SmartFamily.Contracts.Services
{
    /// <summary>
    /// Application information service interface.
    /// </summary>
    public interface IApplicationInfoService
    {
        /// <summary>
        /// Get the version of the application.
        /// </summary>
        /// <returns>Version.</returns>
        Version GetVersion();
    }
}