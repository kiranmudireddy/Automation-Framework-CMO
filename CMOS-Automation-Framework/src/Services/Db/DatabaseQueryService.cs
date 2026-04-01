using CMOS_Automation_Framework.src.Models.DbModels;
using InterSystems.Data.IRISClient;
using DataColumn = System.Data.DataColumn;
using DataRow = System.Data.DataRow;
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

        using var command = CreateCommand(connection, sql);

        using var adapter = new IRISDataAdapter(command);
        var dataTable = new DataTable();
        adapter.Fill(dataTable);

        return new DatabaseSnapshot(queryName, ToRows(dataTable));
    }

    public DatabaseSnapshot CaptureSnapshot(DatabaseQueryDefinition queryDefinition)
    {
        ArgumentNullException.ThrowIfNull(queryDefinition);

        using var connection = _connectionFactory.CreateConnection();
        connection.Open();

        using var command = CreateCommand(connection, queryDefinition);
        using var adapter = new IRISDataAdapter(command);
        var dataTable = new DataTable();
        adapter.Fill(dataTable);

        return new DatabaseSnapshot(queryDefinition.QueryName, ToRows(dataTable));
    }

    public object? ExecuteScalar(string sql)
    {
        using var connection = _connectionFactory.CreateConnection();
        connection.Open();

        using var command = CreateCommand(connection, sql);

        return command.ExecuteScalar();
    }

    public object? ExecuteScalar(DatabaseQueryDefinition queryDefinition)
    {
        ArgumentNullException.ThrowIfNull(queryDefinition);

        using var connection = _connectionFactory.CreateConnection();
        connection.Open();

        using var command = CreateCommand(connection, queryDefinition);
        return command.ExecuteScalar();
    }

    private IRISCommand CreateCommand(IRISConnection connection, string sql)
    {
        return new IRISCommand(sql, connection)
        {
            CommandTimeout = _connectionFactory.GetCommandTimeoutSeconds()
        };
    }

    private IRISCommand CreateCommand(IRISConnection connection, DatabaseQueryDefinition queryDefinition)
    {
        var command = CreateCommand(connection, queryDefinition.Sql);

        foreach (var parameter in queryDefinition.Parameters)
        {
            command.Parameters.Add(new IRISParameter(parameter.Name, parameter.Value ?? DBNull.Value));
        }

        return command;
    }

    private static IReadOnlyList<Dictionary<string, object?>> ToRows(DataTable table)
    {
        return table.Rows
            .Cast<DataRow>()
            .Select(row => table.Columns
                .Cast<DataColumn>()
                .ToDictionary(column => column.ColumnName, column => row[column] is DBNull ? null : row[column]))
            .ToList();
    }
}
