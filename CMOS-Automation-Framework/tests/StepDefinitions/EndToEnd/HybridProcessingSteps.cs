using CMOS_Automation_Framework.src.Builders.FileBuilders;
using CMOS_Automation_Framework.src.Config;
using CMOS_Automation_Framework.src.Helpers.Db;
using CMOS_Automation_Framework.src.Helpers.Evidence;
using CMOS_Automation_Framework.src.Models.ContextModels;
using CMOS_Automation_Framework.src.Models.DbModels;
using CMOS_Automation_Framework.src.Models.FileModels;
using CMOS_Automation_Framework.src.Queries;
using CMOS_Automation_Framework.src.Services.Db;
using CMOS_Automation_Framework.src.Services.Evidence;
using CMOS_Automation_Framework.src.Services.File;
using CMOS_Automation_Framework.src.Services.Orchestration;
using CMOS_Automation_Framework.src.Validators.DbValidators;
using CMOS_Automation_Framework.src.Validators.EndToEndValidators;
using CMOS_Automation_Framework.src.Validators.FileValidators;
using DataTable = System.Data.DataTable;

namespace CMOS_Automation_Framework.tests.StepDefinitions.EndToEnd;

[Binding]
public class HybridProcessingSteps
{
    private readonly CmosTestContext _context;
    private readonly EnvironmentSettings _environment;
    private readonly EvidenceCollector _evidenceCollector;
    private readonly IEvidencePathHelper _evidencePathHelper;
    private readonly CmosScenarioOrchestrator _orchestrator;
    private readonly DelimitedFileService _fileService;
    private readonly FileProcessingValidator _fileValidator;
    private readonly CmosDbValidator _dbValidator;
    private readonly ScenarioOutcomeValidator _endToEndValidator;
    private readonly DoeiFileBuilder _fileBuilder;
    private readonly DatabaseQueryService _databaseQueryService;
    private readonly IDbSnapshotHelper _dbSnapshotHelper;
    private string _evidencePath = string.Empty;
    private string _reportPath = string.Empty;

    public HybridProcessingSteps(
        CmosTestContext context,
        EnvironmentSettings environment,
        EvidenceCollector evidenceCollector,
        IEvidencePathHelper evidencePathHelper,
        CmosScenarioOrchestrator orchestrator,
        DelimitedFileService fileService,
        FileProcessingValidator fileValidator,
        CmosDbValidator dbValidator,
        ScenarioOutcomeValidator endToEndValidator,
        DoeiFileBuilder fileBuilder,
        DatabaseQueryService databaseQueryService,
        IDbSnapshotHelper dbSnapshotHelper)
    {
        _context = context;
        _environment = environment;
        _evidenceCollector = evidenceCollector;
        _evidencePathHelper = evidencePathHelper;
        _orchestrator = orchestrator;
        _fileService = fileService;
        _fileValidator = fileValidator;
        _dbValidator = dbValidator;
        _endToEndValidator = endToEndValidator;
        _fileBuilder = fileBuilder;
        _databaseQueryService = databaseQueryService;
        _dbSnapshotHelper = dbSnapshotHelper;
    }

    [Given(@"a reusable CMOS framework scenario context")]
    public void GivenAReusableCmosFrameworkScenarioContext()
    {
        _context.Status = "Running";
    }

    [When(@"the demo orchestration is prepared")]
    public void WhenTheDemoOrchestrationIsPrepared()
    {
        var lines = _fileBuilder.BuildInstructionLines(
        [
            ("4100001234567890", 150.25m, "REF001"),
            ("4100001234567891", 99.75m, "REF002")
        ]);

        var layout = new FileLayoutDefinition("DOEI", "v1", "HDR", "TRL");
        var evidenceRoot = Path.Combine(TestContext.CurrentContext.WorkDirectory, _environment.Framework.EvidenceRoot);
        var generatedFilePath = _evidencePathHelper.CreateArtifactPath(evidenceRoot, _context, "generated-files", "demo-doei.csv", "demo-orchestration");
        var generatedFilesRoot = Path.GetDirectoryName(generatedFilePath) ?? throw new InvalidOperationException("Generated file directory could not be resolved.");
        var generatedFileName = Path.GetFileName(generatedFilePath);
        var fileArtifact = _fileService.GenerateFile(layout, lines, generatedFilesRoot, generatedFileName, 250.00m);
        _evidenceCollector.CaptureFile(_context, fileArtifact);

        var fileCountValidation = _fileValidator.ValidateCounts(fileArtifact, 2);
        var headerValidation = _fileService.ValidateHeaderAndTrailer(layout, _fileService.ReadRecords(fileArtifact.FullPath));

        var transactionTable = new DataTable();
        transactionTable.Columns.Add("PaymentInstructionId");
        transactionTable.Rows.Add("PI-001");
        var queueTable = new DataTable();
        queueTable.Columns.Add("QueueReference");
        queueTable.Rows.Add("Q-001");

        var paymentInstructionQuery = PaymentInstructionQueries.ByInstructionId("PI-001");
        var queueQuery = QueueQueries.ByReference("Q-001");

        var transactionSnapshot = new DatabaseSnapshot(paymentInstructionQuery.QueryName, ToRows(transactionTable));
        var queueSnapshot = new DatabaseSnapshot(queueQuery.QueryName, ToRows(queueTable));
        _evidenceCollector.CaptureDatabaseSnapshot(_context, transactionSnapshot);
        _evidenceCollector.CaptureDatabaseSnapshot(_context, queueSnapshot);

        var transactionValidation = _dbValidator.ValidateTransactionCreated(transactionSnapshot);
        var queueValidation = _dbValidator.ValidateQueueCreated(queueSnapshot);
        var e2eValidation = _endToEndValidator.ValidateAll(fileCountValidation, headerValidation, transactionValidation, queueValidation);

        _context.ValidationResults.AddRange([fileCountValidation, headerValidation, transactionValidation, queueValidation, e2eValidation]);
        _databaseQueryService.Should().NotBeNull();
        _dbSnapshotHelper.SerializeSnapshot(transactionSnapshot).Should().NotBeNullOrWhiteSpace();

        (_evidencePath, _reportPath) = _orchestrator.FinalizeScenario(
            _context,
            evidenceRoot,
            Path.Combine(TestContext.CurrentContext.WorkDirectory, "tests", "Reports"));
    }

    [Then(@"the framework produces manager-readable evidence outputs")]
    public void ThenTheFrameworkProducesManagerReadableEvidenceOutputs()
    {
        File.Exists(_evidencePath).Should().BeTrue();
        File.Exists(_reportPath).Should().BeTrue();
        _context.Status.Should().Be("Passed");
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
