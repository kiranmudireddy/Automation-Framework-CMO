using System.Data;
using InterSystems.Data.IRISClient;

namespace CMOS_Automation_Framework.src.Services.Db;

public class QueryExecutor
{
    private readonly IrisConnectionFactory _connectionFactory;

    public QueryExecutor(IrisConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public DataTable ExecuteQuery(string sql)
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

        return dataTable;
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

    public int ExecuteNonQuery(string sql)
    {
        using var connection = _connectionFactory.CreateConnection();
        connection.Open();

        using var command = new IRISCommand(sql, connection)
        {
            CommandTimeout = _connectionFactory.GetCommandTimeoutSeconds()
        };

        return command.ExecuteNonQuery();
    }
}