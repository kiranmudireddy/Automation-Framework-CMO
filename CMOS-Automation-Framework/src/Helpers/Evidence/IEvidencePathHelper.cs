using CMOS_Automation_Framework.src.Models.ContextModels;

namespace CMOS_Automation_Framework.src.Helpers.Evidence;

public interface IEvidencePathHelper
{
    string GetScenarioRoot(string evidenceRoot, CmosTestContext context);
    string CreateArtifactPath(string evidenceRoot, CmosTestContext context, string artifactType, string fileName, string? stepName = null);
}
