using CommunityToolkit.Mvvm.ComponentModel;

using Newtonsoft.Json;

using SmartFamily.Backend.Models;

namespace SmartFamily.Backend.ViewModels;

[Serializable]
public sealed class DatabaseViewModel : ObservableObject
{
    [JsonIgnore]
    public DatabaseModel DatabaseModel { get; set; }

    public DatabaseIdModel DatabaseIdModel { get; }

    public string DatabaseRootPath { get; }

    public string DatabaseName { get; }

    public DatabaseViewModel(DatabaseIdModel databaseIdModel, string databaseRootPath)
    {
        DatabaseIdModel = databaseIdModel;
        DatabaseRootPath = databaseRootPath;
        DatabaseName = Path.GetFileName(databaseRootPath);
        DatabaseModel = new(databaseIdModel);
    }
}