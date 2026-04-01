namespace CMOS_Automation_Framework.src.Config;

public class ApiSettings
{
    public string BaseUrl { get; set; } = string.Empty;
    public string AuthToken { get; set; } = string.Empty;
    public Dictionary<string, string> DefaultHeaders { get; set; } = new(StringComparer.OrdinalIgnoreCase);
    public int TimeoutSeconds { get; set; } = 60;
}
