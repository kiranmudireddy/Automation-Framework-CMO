using Microsoft.Extensions.Configuration;

namespace CMOS_Automation_Framework.src.Config;

public class FrameworkConfigurationManager
{
    private readonly IConfigurationRoot _configurationRoot;

    public FrameworkConfigurationManager()
        : this(System.Environment.GetEnvironmentVariable("TEST_ENV") ?? Constants.CustomEnvironments.Local)
    {
    }

    public FrameworkConfigurationManager(string environmentName)
    {
        _configurationRoot = new ConfigurationBuilder()
            .SetBasePath(AppContext.BaseDirectory)
            .AddJsonFile($"appsettings.{environmentName}.json", optional: false, reloadOnChange: true)
            .Build();
    }

    public IConfigurationRoot Root => _configurationRoot;

    public EnvironmentSettings LoadEnvironment()
    {
        var environmentSettings = new EnvironmentSettings
        {
            Name = _configurationRoot["EnvironmentName"] ?? System.Environment.GetEnvironmentVariable("TEST_ENV") ?? Constants.CustomEnvironments.Local,
            Api = _configurationRoot.GetSection("ApiSettings").Get<ApiSettings>() ?? new ApiSettings(),
            Db = _configurationRoot.GetSection("DbSettings").Get<DbSettings>() ?? new DbSettings(),
            Sftp = _configurationRoot.GetSection("SftpSettings").Get<SftpSettings>() ?? new SftpSettings(),
            Framework = _configurationRoot.GetSection("FrameworkSettings").Get<FrameworkSettings>() ?? new FrameworkSettings()
        };

        return ApplyEnvironmentOverrides(environmentSettings);
    }

    private static EnvironmentSettings ApplyEnvironmentOverrides(EnvironmentSettings settings)
    {
        return new EnvironmentSettings
        {
            Name = settings.Name,
            Api = new ApiSettings
            {
                BaseUrl = Resolve("CMOS_API_BASE_URL", settings.Api.BaseUrl),
                AuthToken = Resolve("CMOS_API_AUTH_TOKEN", settings.Api.AuthToken),
                TimeoutSeconds = settings.Api.TimeoutSeconds,
                DefaultHeaders = new Dictionary<string, string>(settings.Api.DefaultHeaders, StringComparer.OrdinalIgnoreCase)
            },
            Db = new DbSettings
            {
                Server = Resolve("CMOS_DB_SERVER", settings.Db.Server),
                Port = ResolveInt("CMOS_DB_PORT", settings.Db.Port),
                Namespace = Resolve("CMOS_DB_NAMESPACE", settings.Db.Namespace),
                Username = Resolve("CMOS_DB_USERNAME", settings.Db.Username),
                Password = Resolve("CMOS_DB_PASSWORD", settings.Db.Password),
                CommandTimeoutSeconds = ResolveInt("CMOS_DB_COMMAND_TIMEOUT_SECONDS", settings.Db.CommandTimeoutSeconds)
            },
            Sftp = new SftpSettings
            {
                Host = Resolve("CMOS_SFTP_HOST", settings.Sftp.Host),
                Port = ResolveInt("CMOS_SFTP_PORT", settings.Sftp.Port),
                Username = Resolve("CMOS_SFTP_USERNAME", settings.Sftp.Username),
                Password = Resolve("CMOS_SFTP_PASSWORD", settings.Sftp.Password),
                RemoteInboundFolder = Resolve("CMOS_SFTP_REMOTE_INBOUND_FOLDER", settings.Sftp.RemoteInboundFolder),
                RemoteOutboundFolder = Resolve("CMOS_SFTP_REMOTE_OUTBOUND_FOLDER", settings.Sftp.RemoteOutboundFolder)
            },
            Framework = new FrameworkSettings
            {
                EvidenceRoot = Resolve("CMOS_FRAMEWORK_EVIDENCE_ROOT", settings.Framework.EvidenceRoot),
                DefaultPollingIntervalSeconds = ResolveInt("CMOS_FRAMEWORK_DEFAULT_POLLING_INTERVAL_SECONDS", settings.Framework.DefaultPollingIntervalSeconds),
                DefaultTimeoutSeconds = ResolveInt("CMOS_FRAMEWORK_DEFAULT_TIMEOUT_SECONDS", settings.Framework.DefaultTimeoutSeconds)
            }
        };
    }

    private static string Resolve(string environmentVariableName, string currentValue)
    {
        var overrideValue = System.Environment.GetEnvironmentVariable(environmentVariableName);
        return string.IsNullOrWhiteSpace(overrideValue) ? currentValue : overrideValue;
    }

    private static int ResolveInt(string environmentVariableName, int currentValue)
    {
        var overrideValue = System.Environment.GetEnvironmentVariable(environmentVariableName);
        return int.TryParse(overrideValue, out var parsedValue) ? parsedValue : currentValue;
    }
}
