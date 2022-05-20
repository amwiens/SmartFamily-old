using SmartFamily.Backend.Models;

namespace SmartFamily.Backend.Services;

public interface ILocalizationService
{
    AppLanguageModel CurrentAppLanguage { get; }

    string? LocalizeFromResourceKey(string resourceKey);

    AppLanguageModel? GetActiveLanguage();

    void SetActiveLanguage(AppLanguageModel language);

    IEnumerable<AppLanguageModel> GetLanguages();
}