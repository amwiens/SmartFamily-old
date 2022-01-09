using Microsoft.Extensions.Configuration;

using Prism.Ioc;
using Prism.Modularity;

using SmartFamily.Contracts.Services;
using SmartFamily.Core.Constants;
using SmartFamily.Core.Contracts.Services;
using SmartFamily.Core.Models;
using SmartFamily.Core.Services;
using SmartFamily.Core.WPF.Dialogs.ViewModels;
using SmartFamily.Core.WPF.Dialogs.Views;
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

        public App()
        {
        }

        protected override Window CreateShell() =>
            Container.Resolve<ShellWindow>();

        protected override async void OnInitialized()
        {
            var persistAndRestoreService = Container.Resolve<IPersistAndRestoreService>();
            persistAndRestoreService.RestoreData();

            var themeSelectorService = Container.Resolve<IThemeSelectorService>();
            themeSelectorService.InitializeTheme();

            base.OnInitialized();
            await Task.CompletedTask;
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            _startUpArgs = e.Args;
            base.OnStartup(e);
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            // Core Services
            containerRegistry.Register<IFileService, FileService>();

            // App Services
            //containerRegistry.RegisterSingleton<IToastNotificationsService, ToastNotificationsService>();
            containerRegistry.Register<IApplicationInfoService, ApplicationInfoService>();
            containerRegistry.Register<ISystemService, SystemService>();
            containerRegistry.Register<IPersistAndRestoreService, PersistAndRestoreService>();
            containerRegistry.Register<IThemeSelectorService, ThemeSelectorService>();
            containerRegistry.RegisterSingleton<IRightPaneService, RightPaneService>();

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

        protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog)
        {
            moduleCatalog.AddModule<MainModule>();
        }

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

        private void OnExit(object sender, ExitEventArgs e)
        {
            var persistAndRestoreService = Container.Resolve<IPersistAndRestoreService>();
            persistAndRestoreService.PersistData();
        }

        private void OnDispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            // TODO: Please log and handle the excption as appropriate to your scenario
            // For more info see https://docs.microsoft.com/dotnet/api/system.windows.application.dispatcherunhandledexception?view=netcore-3.0
        }
    }
}