using SmartFamily.Contracts.Services;

using System;
using System.Diagnostics;
using System.Reflection;

namespace SmartFamily.Services
{
    /// <summary>
    /// Application information service.
    /// </summary>
    public class ApplicationInfoService : IApplicationInfoService
    {
        /// <summary>
        /// Ctor
        /// </summary>
        public ApplicationInfoService()
        {
        }

        /// <inheritdoc/>
        public Version GetVersion()
        {
            // Set the app version in SmartFamily > Properties > Package => PackageVersion
            string assemblyLocation = Assembly.GetExecutingAssembly().Location;
            var version = FileVersionInfo.GetVersionInfo(assemblyLocation).FileVersion;
            return new Version(version);
        }
    }
}