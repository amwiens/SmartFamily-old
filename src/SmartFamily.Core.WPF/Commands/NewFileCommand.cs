using Prism.Regions;
using Prism.Services.Dialogs;

using SmartFamily.Core.Constants;
using SmartFamily.Core.Contracts.Services;
using SmartFamily.Core.WPF.Dialogs;

using System.IO;

namespace SmartFamily.Core.WPF.Commands
{
    public class NewFileCommand : CommandBase
    {
        public IDialogService _dialogService;
        public IDatabaseService _databaseService;
        public IRegionNavigationService _navigationService;

        public NewFileCommand(IDialogService dialogService,
            IDatabaseService databaseService,
            IRegionNavigationService navigationService)
        {
            _dialogService = dialogService;
            _databaseService = databaseService;
            _navigationService = navigationService;
        }

        public override void Execute(object? parameter)
        {
            _dialogService.ShowNewFile(r =>
            {
                if (r.Result == ButtonResult.OK)
                {
                    var fileName = r.Parameters.GetValue<string>("FileName");
                    var fileLocation = r.Parameters.GetValue<string>("FileLocation");
                    var dbPath = Path.Combine(fileLocation, $"{fileName}.sfdb");
                    _databaseService.CreateDatabase(dbPath);
                    ApplicationSettings.OpenDatabase = dbPath;

                    RequestNavigateAndCleanJournal(PageKeys.Main);
                }
            });
        }

        /// <summary>
        /// Request navigate.
        /// </summary>
        /// <param name="target">Target page.</param>
        /// <returns><c>true</c> if page can be navigated to, otherwise <c>false</c>.</returns>
        private bool RequestNavigate(string target)
        {
            if (_navigationService.CanNavigate(target))
            {
                _navigationService.RequestNavigate(target);
                return true;
            }

            return false;
        }

        /// <summary>
        /// Navigate to the target page.
        /// </summary>
        /// <param name="target">Target page.</param>
        private void RequestNavigateAndCleanJournal(string target)
        {
            var navigated = RequestNavigate(target);
            if (navigated)
            {
                _navigationService.Journal.Clear();
            }
        }
    }
}