using SmartFamily.Backend.Enums;

namespace SmartFamily.Backend.Services;

public interface IUpdateService
{
    Task<bool> AreAppUpdatesSupportedAsync();

    Task<bool> IsNewUpdateAvailableAsync();

    Task<AppUpdateResult> UpdateAppAsync(IProgress<double>? progress);
}