using CMOS_Automation_Framework.src.Config.Settings;
using InterSystems.Data.IRISClient;
using Microsoft.Extensions.Configuration;
using System;

namespace CMOS_Automation_Framework.src.Services.Db;

public class IrisConnectionFactory
{
    private readonly DbSettings _dbSettings;

    public IrisConnectionFactory(IConfiguration configuration)
    {
        _dbSettings = configuration.GetSection("DbSettings").Get<DbSettings>()
                      ?? throw new InvalidOperationException("DbSettings section is missing from configuration.");
    }

    public IRISConnection CreateConnection()
    {
        var connectionString =
            $"Server={_dbSettings.Server};" +
            $"Port={_dbSettings.Port};" +
            $"Namespace={_dbSettings.Namespace};" +
            $"Password={_dbSettings.Password};" +
            $"User ID={_dbSettings.Username};";

        return new IRISConnection(connectionString);
    }

    public int GetCommandTimeoutSeconds()
    {
        return _dbSettings.CommandTimeoutSeconds;
    }
}