using CMOS_Automation_Framework.src.Builders.FileBuilders;
using CMOS_Automation_Framework.src.Config;
using CMOS_Automation_Framework.src.Models.ContextModels;
using CMOS_Automation_Framework.src.Models.DbModels;
using CMOS_Automation_Framework.src.Models.FileModels;
using CMOS_Automation_Framework.src.Services.Db;
using CMOS_Automation_Framework.src.Services.Evidence;
using CMOS_Automation_Framework.src.Services.File;
using CMOS_Automation_Framework.src.Services.Orchestration;
using CMOS_Automation_Framework.src.Validators.DbValidators;
using CMOS_Automation_Framework.src.Validators.EndToEndValidators;
using CMOS_Automation_Framework.src.Validators.FileValidators;
using DataTable = System.Data.DataTable;
using ReqnrollScenarioContext = Reqnroll.ScenarioContext;

namespace CMOS_Automation_Framework.tests.StepDefinitions.EndToEnd;

[Binding]
public class HybridProcessingSteps
{
    private readonly ReqnrollScenarioContext _scenarioContext;
    private readonly CmosTestContext _context;
    private string _evidencePath = string.Empty;
    private string _reportPath = string.Empty;

    public HybridProcessingSteps(ReqnrollScenarioContext scenarioContext)
    {
        _scenarioContext = scenarioContext;
        _context = _scenarioContext.Get<CmosTestContext>();
    }

    [Given(@"a reusable CMOS framework scenario context")]
    public void GivenAReusableCmosFrameworkScenarioContext()
    {
        _context.Status = "Running";
    }

    [When(@"the demo orchestration is prepared")]
    public void WhenTheDemoOrchestrationIsPrepared()
    {
        using var provider = new FrameworkServiceProviderFactory().Create("LOCAL");
        var environment = provider.GetRequiredService<EnvironmentSettings>();
        var evidenceCollector = provider.GetRequiredService<EvidenceCollector>();
        var orchestrator = provider.GetRequiredService<CmosScenarioOrchestrator>();
        var fileService = provider.GetRequiredService<DelimitedFileService>();
        var fileValidator = provider.GetRequiredService<FileProcessingValidator>();
        var dbValidator = provider.GetRequiredService<CmosDbValidator>();
        var endToEndValidator = provider.GetRequiredService<ScenarioOutcomeValidator>();
        var fileBuilder = provider.GetRequiredService<DoeiFileBuilder>();
        var databaseQueryService = provider.GetRequiredService<DatabaseQueryService>();

        var lines = fileBuilder.BuildInstructionLines(
        [
            ("4100001234567890", 150.25m, "REF001"),
            ("4100001234567891", 99.75m, "REF002")
        ]);

        var layout = new FileLayoutDefinition("DOEI", "v1", "HDR", "TRL");
        var generatedFilesRoot = Path.Combine(TestContext.CurrentContext.WorkDirectory, "tests", "Reports", "GeneratedFiles");
        var fileArtifact = fileService.GenerateFile(layout, lines, generatedFilesRoot, "demo-doei.csv", 250.00m);
        evidenceCollector.CaptureFile(_context, fileArtifact);

        var fileCountValidation = fileValidator.ValidateCounts(fileArtifact, 2);
        var headerValidation = fileService.ValidateHeaderAndTrailer(layout, fileService.ReadRecords(fileArtifact.FullPath));

        var transactionTable = new DataTable();
        transactionTable.Columns.Add("PaymentInstructionId");
        transactionTable.Rows.Add("PI-001");
        var queueTable = new DataTable();
        queueTable.Columns.Add("QueueReference");
        queueTable.Rows.Add("Q-001");

        var transactionSnapshot = new DatabaseSnapshot("PaymentInstruction", "SELECT * FROM PI WHERE PaymentInstructionId = 'PI-001'", transactionTable);
        var queueSnapshot = new DatabaseSnapshot("Queue", "SELECT * FROM PIQ WHERE QueueReference = 'Q-001'", queueTable);
        evidenceCollector.CaptureDatabaseSnapshot(_context, transactionSnapshot);
        evidenceCollector.CaptureDatabaseSnapshot(_context, queueSnapshot);

        var transactionValidation = dbValidator.ValidateTransactionCreated(transactionSnapshot);
        var queueValidation = dbValidator.ValidateQueueCreated(queueSnapshot);
        var e2eValidation = endToEndValidator.ValidateAll(fileCountValidation, headerValidation, transactionValidation, queueValidation);

        _context.ValidationResults.AddRange([fileCountValidation, headerValidation, transactionValidation, queueValidation, e2eValidation]);
        databaseQueryService.Should().NotBeNull();

        (_evidencePath, _reportPath) = orchestrator.FinalizeScenario(
            _context,
            Path.Combine(TestContext.CurrentContext.WorkDirectory, environment.Framework.EvidenceRoot),
            Path.Combine(TestContext.CurrentContext.WorkDirectory, "tests", "Reports"));
    }

    [Then(@"the framework produces manager-readable evidence outputs")]
    public void ThenTheFrameworkProducesManagerReadableEvidenceOutputs()
    {
        File.Exists(_evidencePath).Should().BeTrue();
        File.Exists(_reportPath).Should().BeTrue();
        _context.Status.Should().Be("Passed");
    }
}
