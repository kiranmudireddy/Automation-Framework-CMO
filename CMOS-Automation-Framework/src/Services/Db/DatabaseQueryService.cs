using CMOS_Automation_Framework.src.Models.DbModels;
using InterSystems.Data.IRISClient;
using DataTable = System.Data.DataTable;

namespace CMOS_Automation_Framework.src.Services.Db;

public class DatabaseQueryService
{
    private readonly IrisConnectionFactory _connectionFactory;

    public DatabaseQueryService(IrisConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public DatabaseSnapshot CaptureSnapshot(string queryName, string sql)
    {
        using var connection = _connectionFactory.CreateConnection();
        connection.Open();

        using var command = new IRISCommand(sql, connection)
        {
            CommandTimeout = _connectionFactory.GetCommandTimeoutSeconds()
        };

        using var adapter = new IRISDataAdapter(command);
        var dataTable = new DataTable();
        adapter.Fill(dataTable);

        return new DatabaseSnapshot(queryName, sql, dataTable);
    }

    public object? ExecuteScalar(string sql)
    {
        using var connection = _connectionFactory.CreateConnection();
        connection.Open();

        using var command = new IRISCommand(sql, connection)
        {
            CommandTimeout = _connectionFactory.GetCommandTimeoutSeconds()
        };

        return command.ExecuteScalar();
    }
}
