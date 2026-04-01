using System.Text.Json;

namespace CMOS_Automation_Framework.src.Utils.Common;

public class JsonHelper
{
    private static readonly JsonSerializerOptions DefaultOptions = new()
    {
        WriteIndented = true,
        PropertyNameCaseInsensitive = true
    };

    public string Serialize<T>(T value)
    {
        return JsonSerializer.Serialize(value, DefaultOptions);
    }

    public T? Deserialize<T>(string json)
    {
        return JsonSerializer.Deserialize<T>(json, DefaultOptions);
    }
}
