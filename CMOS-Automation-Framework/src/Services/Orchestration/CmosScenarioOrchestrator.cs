using CMOS_Automation_Framework.src.Constants;
using CMOS_Automation_Framework.src.Models.ContextModels;
using CMOS_Automation_Framework.src.Models.ValidationModels;
using CMOS_Automation_Framework.src.Services.Evidence;
using CMOS_Automation_Framework.src.Services.Reporting;

namespace CMOS_Automation_Framework.src.Services.Orchestration;

public class CmosScenarioOrchestrator
{
    private readonly EvidenceCollector _evidenceCollector;
    private readonly ScenarioReportWriter _reportWriter;

    public CmosScenarioOrchestrator(EvidenceCollector evidenceCollector, ScenarioReportWriter reportWriter)
    {
        _evidenceCollector = evidenceCollector;
        _reportWriter = reportWriter;
    }

    public (string EvidencePath, string ReportPath) FinalizeScenario(CmosTestContext context, string evidenceRoot, string reportRoot)
    {
        if (context.ValidationResults.All(result => result.Passed))
        {
            context.Status = "Passed";
            context.ValidationResults.Add(new ValidationResult(CustomMessages.NoBlockingErrors, true, "All business validations passed."));
        }
        else
        {
            context.Status = "Failed";
        }

        var evidencePath = _evidenceCollector.Persist(context, evidenceRoot);
        var reportPath = _reportWriter.WriteManagerReport(context, reportRoot);
        return (evidencePath, reportPath);
    }
}
