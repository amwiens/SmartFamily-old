using CommunityToolkit.Mvvm.ComponentModel;

using Newtonsoft.Json;

namespace SmartFamily.Backend.Models;

[Serializable]
public sealed class DatabaseModel : ObservableObject
{
    [JsonIgnore]
    public DatabaseIdModel DatabaseIdModel { get; }

    private DateTime _lastOpened;
    public DateTime LastOpened
    {
        get => _lastOpened;
        set => SetProperty(ref _lastOpened, value);
    }

    private DateTime _lastScanned;
    public DateTime LastScanned
    {
        get => _lastScanned;
        set => SetProperty(ref _lastScanned, value);
    }

    public DatabaseModel(DatabaseIdModel databaseIdModel)
    {
        DatabaseIdModel = databaseIdModel;
    }
 }