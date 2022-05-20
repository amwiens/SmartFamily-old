using Microsoft.Windows.ApplicationModel.Resources;

using SmartFamily.Backend.Models;
using SmartFamily.Backend.Services;

using Windows.Globalization;

namespace SmartFamily.WinUI.ServiceImplementation;

internal sealed class LocalizationService : ILocalizationService
{
    private static readonly ResourceLoader IndependentLoader = new();

    private AppLanguageModel? _currentAppLanguage;
    public AppLanguageModel CurrentAppLanguage
    {
        get => _currentAppLanguage ??= new(ApplicationLanguages.PrimaryLanguageOverride);
    }

    public string LocalizeFromResourceKey(string resourceKey)
    {
        return IndependentLoader.GetString(resourceKey);
    }

    public AppLanguageModel? GetActiveLanguage()
    {
        var languages = GetLanguages();

        return languages.FirstOrDefault(IThreadPoolWorkItem => IThreadPoolWorkItem.Id == ApplicationLanguages.PrimaryLanguageOverride) ?? languages.FirstOrDefault();
    }

    public void SetActiveLanguage(AppLanguageModel language)
    {
        ApplicationLanguages.PrimaryLanguageOverride = language.Id;
    }

    public IEnumerable<AppLanguageModel> GetLanguages()
    {
        return ApplicationLanguages.ManifestLanguages.Select(item => new AppLanguageModel(item));
    }
}