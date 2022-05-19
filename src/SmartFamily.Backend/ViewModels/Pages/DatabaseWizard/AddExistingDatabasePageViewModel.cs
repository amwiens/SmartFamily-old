using CommunityToolkit.Mvvm.DependencyInjection;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;

using SmartFamily.Backend.Messages;
using SmartFamily.Backend.Services;
using SmartFamily.Backend.Utils;
using SmartFamily.Backend.ViewModels.Dialogs;

namespace SmartFamily.Backend.ViewModels.Pages.DatabaseWizard;

public sealed class AddExistingDatabasePageViewModel : BaseDatabaseWizardPageViewModel
{
    private IFileExplorerService FileExplorerService { get; } = Ioc.Default.GetRequiredService<IFileExplorerService>();

    private string? _pathSourceText;
    public string? PathSourceText
    {
        get => _pathSourceText;
        set
        {
            if (SetProperty(ref _pathSourceText, value))
            {
                DialogViewModel.PrimaryButtonEnabled = CheckAvailability(value);
            }
        }
    }

    public IAsyncRelayCommand BrowseForFileCommand { get; }

    public AddExistingDatabasePageViewModel(IMessenger messenger, DatabaseWizardDialogViewModel dialogViewModel)
        : base(messenger, dialogViewModel)
    {
        DialogViewModel.PrimaryButtonClickCommand = new RelayCommand<IHandledFlag?>(PrimaryButtonClick);
        BrowseForFileCommand = new AsyncRelayCommand(BrowseForFile);
    }

    private void PrimaryButtonClick(IHandledFlag? e)
    {
        e?.Handle();
        DialogViewModel.DatabaseViewModel = new(new(), Path.GetFullPath(PathSourceText!)!);

        Messenger.Send(new DatabaseWizardNavigationRequestedMessage(new DatabaseWizardFinishPageViewModel(Messenger, DialogViewModel)));
    }

    private async Task BrowseForFile()
    {
        var path = await FileExplorerService.PickSingleFileAsync(new List<string>() { Path.GetExtension(".rmtree") });
        if (!string.IsNullOrEmpty(path))
        {
            PathSourceText = path;
        }
    }

    private bool CheckAvailability(string? path)
    {
        if (string.IsNullOrEmpty(path))
        {
            return false;
        }

        //path = Path.Combine(Path.GetDirectoryName(path)!, )
        if (File.Exists(path))
        {
            return true;
            //using var fileStream = File.Open(path, FileMode.Open, FileAccess.Read, FileShare.Read);

            //var rawDatabaseConfiguration = rawDatabaseConfiguration.Load(fileStream);
            //return DatabaseVersion.IsVersionSupported((DatabaseVersion)rawDatabaseConfiguration);
        }
        else
        {
            return false;
        }
    }
}