using CMOS_Automation_Framework.src.Config;
using CMOS_Automation_Framework.src.Models.ContextModels;
using CMOS_Automation_Framework.src.Models.DbModels;
using CMOS_Automation_Framework.src.Models.ValidationModels;
using CMOS_Automation_Framework.src.Queries;
using CMOS_Automation_Framework.src.Services.Db;
using CMOS_Automation_Framework.src.Services.Orchestration;
using CMOS_Automation_Framework.src.Validators.DbValidators;
using DataTable = System.Data.DataTable;

namespace CMOS_Automation_Framework.tests;

[TestFixture]
public class FrameworkHealthTests
{
    [Test]
    public void Framework_health_should_wire_config_di_db_and_reporting()
    {
        Environment.SetEnvironmentVariable("TEST_ENV", "LOCAL");

        using var provider = new FrameworkServiceProviderFactory().Create("LOCAL");
        var environment = provider.GetRequiredService<EnvironmentSettings>();
        var configurationManager = provider.GetRequiredService<FrameworkConfigurationManager>();
        var connectionFactory = provider.GetRequiredService<IrisConnectionFactory>();
        var databaseQueryService = provider.GetRequiredService<DatabaseQueryService>();
        var dbValidator = provider.GetRequiredService<CmosDbValidator>();
        var orchestrator = provider.GetRequiredService<CmosScenarioOrchestrator>();

        environment.Name.Should().Be("LOCAL");
        configurationManager.LoadEnvironment().Api.BaseUrl.Should().Be(environment.Api.BaseUrl);
        databaseQueryService.Should().NotBeNull();
        PaymentInstructionQueries.ByInstructionId("PI-001").Sql.Should().Contain("?");

        using var connection = connectionFactory.CreateConnection();
        connection.ConnectionString.Should().Contain(environment.Db.Server);
        connection.ConnectionString.Should().Contain(environment.Db.Namespace);

        var table = new DataTable();
        table.Columns.Add("PaymentInstructionId");
        table.Rows.Add("PI-001");

        var queryDefinition = PaymentInstructionQueries.ByInstructionId("PI-001");
        var snapshot = new DatabaseSnapshot("PaymentInstructionHealth", ToRows(table));
        var validation = dbValidator.ValidateTransactionCreated(snapshot);

        var scenario = new CmosTestContext
        {
            ScenarioName = "Framework Health",
            Status = "Running"
        };

        scenario.DatabaseSnapshots.Add(snapshot);
        scenario.ValidationResults.Add(validation);
        scenario.ValidationResults.Add(new ValidationResult("DI container resolved", true, "All required framework services were resolved."));

        var root = Path.Combine(TestContext.CurrentContext.WorkDirectory, "tests", "Reports", "Health");
        var (evidencePath, reportPath) = orchestrator.FinalizeScenario(
            scenario,
            Path.Combine(root, "Evidence"),
            Path.Combine(root, "Reports"));

        File.Exists(evidencePath).Should().BeTrue();
        File.Exists(reportPath).Should().BeTrue();
        File.ReadAllText(reportPath).Should().Contain("Final status: Passed");
        File.ReadAllText(reportPath).Should().Contain("Payment instruction created");
    }

    private static IReadOnlyList<Dictionary<string, object?>> ToRows(DataTable table)
    {
        return table.Rows
            .Cast<System.Data.DataRow>()
            .Select(row => table.Columns
                .Cast<System.Data.DataColumn>()
                .ToDictionary(column => column.ColumnName, column => row[column] is DBNull ? null : row[column]))
            .ToList();
    }
}
