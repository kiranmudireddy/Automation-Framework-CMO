namespace CMOS_Automation_Framework.src.Config;

public class SftpSettings
{
    public string Host { get; set; } = string.Empty;
    public int Port { get; set; } = 22;
    public string Username { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string RemoteInboundFolder { get; set; } = string.Empty;
    public string RemoteOutboundFolder { get; set; } = string.Empty;
}
