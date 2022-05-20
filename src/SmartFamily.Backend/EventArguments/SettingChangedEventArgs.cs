namespace SmartFamily.Backend.EventArguments;

public sealed class SettingChangedEventArgs : EventArgs
{
    public string SettingName { get; }

    public object? NewValue { get; }

    public SettingChangedEventArgs(string settingName, object? newValue)
    {
        SettingName = settingName;
        NewValue = newValue;
    }
}