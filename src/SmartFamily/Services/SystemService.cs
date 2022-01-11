using SmartFamily.Contracts.Services;

using System.Diagnostics;

namespace SmartFamily.Services
{
    /// <summary>
    /// System service.
    /// </summary>
    public class SystemService : ISystemService
    {
        /// <summary>
        /// Ctor
        /// </summary>
        public SystemService()
        {
        }

        /// <inheritdoc/>
        public void OpenInWebBrowser(string url)
        {
            // For more info see https://github.com/dotnet/corefx/issues/10361
            var psi = new ProcessStartInfo
            {
                FileName = url,
                UseShellExecute = true
            };
            Process.Start(psi);
        }
    }
}