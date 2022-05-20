using Newtonsoft.Json;

namespace SmartFamily.WinUI.Serialization.Implementation;

internal sealed class DefaultJsonSettingsSerializer : IJsonSettingsSerializer
{
    public string? SerializeToJson(object? obj)
    {
        return JsonConvert.SerializeObject(obj, Formatting.Indented);
    }

    public T? DeserializeFromJson<T>(string json)
    {
        return JsonConvert.DeserializeObject<T?>(json);
    }
}