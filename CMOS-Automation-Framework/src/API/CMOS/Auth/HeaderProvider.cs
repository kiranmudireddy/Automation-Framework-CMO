using CMOS_Automation_Framework.src.Config;

namespace CMOS_Automation_Framework.src.API.CMOS.Auth;

public class HeaderProvider
{
    private readonly ApiSettings _settings;

    public HeaderProvider(ApiSettings settings)
    {
        _settings = settings;
    }

    public Dictionary<string, string> BuildHeaders(Dictionary<string, string>? overrides = null)
    {
        var headers = new Dictionary<string, string>(_settings.DefaultHeaders, StringComparer.OrdinalIgnoreCase);

        if (!string.IsNullOrWhiteSpace(_settings.AuthToken))
        {
            headers["Authorization"] = $"Bearer {_settings.AuthToken}";
        }

        if (overrides is null)
        {
            return headers;
        }

        foreach (var pair in overrides)
        {
            headers[pair.Key] = pair.Value;
        }

        return headers;
    }
}
