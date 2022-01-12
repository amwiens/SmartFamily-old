using Microsoft.Extensions.Configuration;

using Prism.Ioc;
using Prism.Modularity;
using Prism.Services.Dialogs;

using SmartFamily.Contracts.Services;
using SmartFamily.Core;
using SmartFamily.Core.Constants;
using SmartFamily.Core.Contracts.Services;
using SmartFamily.Core.Models;
using SmartFamily.Core.Services;
using SmartFamily.Core.WPF.Contracts.Services;
using SmartFamily.Core.WPF.Dialogs;
using SmartFamily.Core.WPF.Dialogs.ViewModels;
using SmartFamily.Core.WPF.Dialogs.Views;
using SmartFamily.EntityFramework.Services;
using SmartFamily.Main;
using SmartFamily.Services;
using SmartFamily.ViewModels;
using SmartFamily.Views;

using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace SmartFamily
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        private string[] _startUpArgs;

        /// <summary>
        /// Ctor
        /// </summary>
        public App()
        {
        }

        /// <summary>
        /// Creates the shell.
        /// </summary>
        /// <returns>Shell window.</returns>
        protected override Window CreateShell() =>
            Container.Resolve<ShellWindow>();

        /// <summary>
        /// Runs on initialization of the app.
        /// </summary>
        protected override async void OnInitialized()
        {
            var persistAndRestoreService = Container.Resolve<IPersistAndRestoreService>();
            persistAndRestoreService.RestoreData();

            var themeSelectorService = Container.Resolve<IThemeSelectorService>();
            themeSelectorService.InitializeTheme();

            base.OnInitialized();
            await Task.CompletedTask;
        }

        /// <summary>
        /// Runs on startup of the app.
        /// </summary>
        /// <param name="e">Event args.</param>
        protected override void OnStartup(StartupEventArgs e)
        {
            _startUpArgs = e.Args;
            base.OnStartup(e);
        }

        /// <summary>
        /// Register types for prism.
        /// </summary>
        /// <param name="containerRegistry">Container registry.</param>
        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            // Core Services
            containerRegistry.Register<IFileService, FileService>();
            containerRegistry.Register<IDatabaseService, DatabaseService>();

            // App Services
            //containerRegistry.RegisterSingleton<IToastNotificationsService, ToastNotificationsService>();
            containerRegistry.Register<IApplicationInfoService, ApplicationInfoService>();
            containerRegistry.Register<IApplicationSettingsService, ApplicationSettingsService>();
            containerRegistry.Register<ISystemService, SystemService>();
            containerRegistry.Register<IPersistAndRestoreService, PersistAndRestoreService>();
            containerRegistry.Register<IThemeSelectorService, ThemeSelectorService>();
            containerRegistry.RegisterSingleton<IRightPaneService, RightPaneService>();
            containerRegistry.Register<IOpenFileDialogService, OpenFileDialogService>();

            // Views
            containerRegistry.RegisterForNavigation<ShellWindow, ShellViewModel>();
            containerRegistry.RegisterForNavigation<SettingsPage, SettingsViewModel>(PageKeys.Settings);

            // Dialogs
            containerRegistry.RegisterDialog<NotificationDialog, NotificationDialogViewModel>();

            // Configuration
            var configuration = BuildConfiguration();
            var appConfig = configuration
                .GetSection(nameof(AppConfig))
                .Get<AppConfig>();

            // Register configurations to IoC
            containerRegistry.RegisterInstance<IConfiguration>(configuration);
            containerRegistry.RegisterInstance<AppConfig>(appConfig);
        }

        /// <summary>
        /// Configure modules.
        /// </summary>
        /// <param name="moduleCatalog">Module catalog.</param>
        protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog)
        {
            moduleCatalog.AddModule<MainModule>();
        }

        /// <summary>
        /// Build configuration.
        /// </summary>
        /// <returns>configuration.</returns>
        private IConfiguration BuildConfiguration()
        {
            var appLocation = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
            return new ConfigurationBuilder()
                .SetBasePath(appLocation)
                .AddJsonFile("appsettings.json")
                .AddCommandLine(_startUpArgs)
                //.AddInMemoryCollection(activationArgs)
                .Build();
        }

        /// <summary>
        /// Runs on session ending.
        /// </summary>
        /// <param name="e">Event args.</param>
        protected override void OnSessionEnding(SessionEndingCancelEventArgs e)
        {
            base.OnSessionEnding(e);

            var applicationSettingsService = Container.Resolve<IApplicationSettingsService>();
            if (applicationSettingsService.GetSetting<bool>("AskForBackup") && !string.IsNullOrWhiteSpace(ApplicationSettings.OpenDatabase))
            {
                var dialogService = Container.Resolve<IDialogService>();
                var message = "Would you like to backup this database?";

                dialogService.ShowNotification(message, r =>
                {
                    if (r.Result == ButtonResult.OK)
                    {
                    }
                });
            }
        }

        /// <summary>
        /// Runs on exit of the app.
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="e">Event args.</param>
        private void OnExit(object sender, ExitEventArgs e)
        {
            var persistAndRestoreService = Container.Resolve<IPersistAndRestoreService>();
            persistAndRestoreService.PersistData();
        }

        /// <summary>
        /// Handles any unhandled exceptions.
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="e">Event args.</param>
        private void OnDispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            // TODO: Please log and handle the excption as appropriate to your scenario
            // For more info see https://docs.microsoft.com/dotnet/api/system.windows.application.dispatcherunhandledexception?view=netcore-3.0
        }
    }
}