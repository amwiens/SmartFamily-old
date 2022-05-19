using Microsoft.UI.Xaml;

using SmartFamily.Backend.Services;

using Windows.ApplicationModel;
using Windows.Storage;
using Windows.System;

namespace SmartFamily.WinUI.ServiceImplementation;

internal sealed class ApplicationService : IApplicationService
{
    public Version GetAppVersion()
    {
        var packageVersion = Package.Current.Id.Version;

        return new Version(packageVersion.Major, packageVersion.Minor, packageVersion.Build, packageVersion.Revision);
    }

    public void CloseApplication()
    {
        Application.Current.Exit();
    }

    public async Task OpenUriAsync(Uri uri)
    {
        await Launcher.LaunchUriAsync(uri);
    }

    public async Task OpenAppFolderAsync()
    {
        await Launcher.LaunchFolderAsync(ApplicationData.Current.LocalFolder);
    }
}