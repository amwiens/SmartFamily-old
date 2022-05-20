using SmartFamily.Backend.Extensions;
using SmartFamily.Shared.Extensions;

using System.Globalization;

namespace SmartFamily.Backend.Models;

[Serializable]
public sealed class AppLanguageModel
{
    public string? Id { get; }

    public string? Name { get; }

    public AppLanguageModel(string? id)
    {
        if (!string.IsNullOrWhiteSpace(id))
        {
            var cultureInfo = new CultureInfo(id);
            Id = cultureInfo.Name;
            Name = cultureInfo.NativeName.FirstToUpper();
        }
        else
        {
            Id = string.Empty;
            var systemDefaultLanguageOptionStr = "SettingsGeneralPageDefaultLanguage".ToLocalized();
            Name = string.IsNullOrEmpty(systemDefaultLanguageOptionStr) ? "Default" : systemDefaultLanguageOptionStr;
        }
    }

    public override string ToString()
    {
        return Name ?? string.Empty;
    }
}