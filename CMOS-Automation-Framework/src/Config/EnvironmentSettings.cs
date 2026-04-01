namespace CMOS_Automation_Framework.src.Config;

public class EnvironmentSettings
{
    public string Name { get; init; } = "LOCAL";
    public ApiSettings Api { get; init; } = new();
    public DbSettings Db { get; init; } = new();
    public SftpSettings Sftp { get; init; } = new();
    public FrameworkSettings Framework { get; init; } = new();
}
