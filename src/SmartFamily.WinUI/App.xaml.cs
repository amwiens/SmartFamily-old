﻿using CommunityToolkit.Mvvm.DependencyInjection;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml;

using SmartFamily.Backend.Services;
using SmartFamily.WinUI.Helpers;
using SmartFamily.WinUI.ServiceImplementation;
using SmartFamily.WinUI.WindowViews;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace SmartFamily.WinUI;

/// <summary>
/// Provides application-specific behavior to supplement the default Application class.
/// </summary>
public partial class App : Application
{
    private Window? _window;

    private IServiceProvider? ServiceProvider { get; set; }

    /// <summary>
    /// Initializes the singleton application object.  This is the first line of authored code
    /// executed, and as such is the logical equivalent of main() or WinMain().
    /// </summary>
    public App()
    {
        this.InitializeComponent();

        EnsureEarlyApp();
    }

    /// <summary>
    /// Invoked when the application is launched normally by the end user.  Other entry points
    /// will be used such as when the application is launched to open a specific file.
    /// </summary>
    /// <param name="args">Details about the launch request and process.</param>
    protected override void OnLaunched(LaunchActivatedEventArgs args)
    {
        _window = new MainWindow();
        _window.Activate();
    }

    private void EnsureEarlyApp()
    {
        // Configure exception handlers
        UnhandledException += App_UnhandledException;
        TaskScheduler.UnobservedTaskException += TaskScheduler_UnobservedTaskException;

        // Configure IoC
        ServiceProvider = ConfigureServices();
        Ioc.Default.ConfigureServices(ServiceProvider);

        // Start AppCenter
        // TODO: Start AppCenter
    }

    private IServiceProvider ConfigureServices()
    {
        var serviceCollection = new ServiceCollection();

        serviceCollection

            .AddSingleton<IDialogService, DialogService>()
            .AddSingleton<IApplicationService, ApplicationService>()
            .AddSingleton<IThreadingService, ThreadingService>()

            .AddSingleton<IFileExplorerService, FileExplorerService>()
            .AddSingleton<IClipboardService, ClipboardService>();

        return serviceCollection.BuildServiceProvider();
    }

    private void TaskScheduler_UnobservedTaskException(object? sender, UnobservedTaskExceptionEventArgs e)
    {
        LogException(e.Exception);
    }

    private void App_UnhandledException(object sender, Microsoft.UI.Xaml.UnhandledExceptionEventArgs e)
    {
        LogException(e.Exception);
    }

    private static void LogException(Exception ex)
    {
        LogExceptionToFile(ex);
    }

    private static void LogExceptionToFile(Exception ex)
    {
        LoggingHelpers.SafeLogExceptionToFile(ex);
    }
}