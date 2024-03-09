using System.Text.Json;
using System.Text.Json.Serialization;

namespace Lambifast.Extensions;

public static class JsonSerializerExtension
{
    public static JsonSerializerOptions GetOptions(bool indent = false)
    {
        var options = new JsonSerializerOptions()
        {
            PropertyNameCaseInsensitive = true,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
            WriteIndented = indent,				
        };
        options.Converters.Add(new JsonStringEnumConverter());
        return options;
    }

    public static string SerializeJson<T>(this T? data, bool indent = false)
    {
        if (data == null)
            return string.Empty;
        return JsonSerializer.Serialize<T>(data, GetOptions(indent));
    }

    public static T? DeserializeJson<T>(this string json)
    {
        return (T?)json.DeserializeJson(typeof(T));
    }

    public static object? DeserializeJson(this string json, Type outType)
    {
        if (string.IsNullOrEmpty(json))
            return null;
        return JsonSerializer.Deserialize(json, outType, GetOptions());
    }
}