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
        return new EnvironmentSettings
        {
            Name = _configurationRoot["EnvironmentName"] ?? System.Environment.GetEnvironmentVariable("TEST_ENV") ?? Constants.CustomEnvironments.Local,
            Api = _configurationRoot.GetSection("ApiSettings").Get<ApiSettings>() ?? new ApiSettings(),
            Db = _configurationRoot.GetSection("DbSettings").Get<DbSettings>() ?? new DbSettings(),
            Sftp = _configurationRoot.GetSection("SftpSettings").Get<SftpSettings>() ?? new SftpSettings(),
            Framework = _configurationRoot.GetSection("FrameworkSettings").Get<FrameworkSettings>() ?? new FrameworkSettings()
        };
    }
}
