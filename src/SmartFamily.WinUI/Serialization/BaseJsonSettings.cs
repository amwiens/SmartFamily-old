using SmartFamily.Backend.EventArguments;

using System.Runtime.CompilerServices;

namespace SmartFamily.WinUI.Serialization;

internal abstract class BaseJsonSettings : ISettingsSharingContext
{
    private ISettingsSharingContext? _settingsSharingContext;

    public bool IsAvailable { get; protected set; }

    private ISettingsSerializer? _settingsSerializer;
    protected ISettingsSerializer? SettingsSerializer
    {
        get => _settingsSharingContext?.Instance?.SettingsSerializer ?? _settingsSerializer;
        set => _settingsSerializer = value;
    }

    private IJsonSettingsSerializer? _jsonSettingsSerializer;
    protected IJsonSettingsSerializer? JsonSettingsSerializer
    {
        get => _settingsSharingContext?.Instance?.JsonSettingsSerializer ?? _jsonSettingsSerializer;
        set => _jsonSettingsSerializer = value;
    }

    private IJsonSettingsDatabase? _jsonSettingsDatabase;
    protected IJsonSettingsDatabase? JsonSettingsDatabase
    {
        get => _settingsSharingContext?.Instance?.JsonSettingsDatabase ?? _jsonSettingsDatabase;
        set => _jsonSettingsDatabase = value;
    }

    BaseJsonSettings ISettingsSharingContext.Instance => this;

    public event EventHandler<SettingChangedEventArgs>? OnSettingChangedEvent;

    public virtual bool FlushSettings()
    {
        return JsonSettingsDatabase?.FlushSettings() ?? false;
    }

    public virtual object ExportSettings()
    {
        return JsonSettingsDatabase?.ExportSettings() ?? false;
    }

    public virtual bool ImportSettings(object import)
    {
        return JsonSettingsDatabase?.ImportSettings(import) ?? false;
    }

    public bool RegisterSettingsContext(ISettingsSharingContext settingsSharingContext)
    {
        if (_settingsSharingContext == null)
        {
            // Can register only once
            _settingsSharingContext = settingsSharingContext;
            IsAvailable = settingsSharingContext.Instance.IsAvailable;
            return true;
        }

        return false;
    }

    public ISettingsSharingContext GetSharingContext()
    {
        return _settingsSharingContext ?? this;
    }

    protected virtual void Initialize(string filePath)
    {
        IsAvailable = SettingsSerializer?.CreateFile(filePath) ?? false;
    }

    protected virtual TValue? Get<TValue>(Func<TValue?>? defaultValue, [CallerMemberName] string propertyName = "")
    {
        if (string.IsNullOrEmpty(propertyName))
        {
            return defaultValue != null ? defaultValue() : default;
        }

        return JsonSettingsDatabase == null ? (defaultValue != null ? defaultValue() : default) : (JsonSettingsDatabase.GetValue(propertyName, defaultValue) ?? (defaultValue != null ? defaultValue() : default));
    }

    protected virtual bool Set<TValue>(TValue? value, [CallerMemberName] string propertyName = "")
    {
        if (string.IsNullOrEmpty(propertyName))
        {
            return false;
        }

        if (JsonSettingsDatabase?.SetValue(propertyName, value) ?? false)
        {
            // TODO: Notify of setting change
            return true;
        }

        return false;
    }
}