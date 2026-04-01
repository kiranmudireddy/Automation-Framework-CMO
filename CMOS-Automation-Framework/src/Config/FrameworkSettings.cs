namespace CMOS_Automation_Framework.src.Config;

public class FrameworkSettings
{
    public string EvidenceRoot { get; set; } = "tests\\Reports\\Evidence";
    public int DefaultPollingIntervalSeconds { get; set; } = 5;
    public int DefaultTimeoutSeconds { get; set; } = 120;
}
