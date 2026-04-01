using CMOS_Automation_Framework.src.Config;
using InterSystems.Data.IRISClient;

namespace CMOS_Automation_Framework.src.Services.Db;

public class IrisConnectionFactory
{
    private readonly DbSettings _dbSettings;

    public IrisConnectionFactory(DbSettings dbSettings)
    {
        _dbSettings = dbSettings;
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
