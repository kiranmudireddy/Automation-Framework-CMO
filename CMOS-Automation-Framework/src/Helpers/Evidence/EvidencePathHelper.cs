using CMOS_Automation_Framework.src.Models.ContextModels;
using CMOS_Automation_Framework.src.Utils.Common;

namespace CMOS_Automation_Framework.src.Helpers.Evidence;

public class EvidencePathHelper : IEvidencePathHelper
{
    private readonly PathHelper _pathHelper;

    public EvidencePathHelper(PathHelper pathHelper)
    {
        _pathHelper = pathHelper;
    }

    public string GetScenarioRoot(string evidenceRoot, CmosTestContext context)
    {
        ArgumentNullException.ThrowIfNull(context);
        return _pathHelper.EnsureDirectory(
            _pathHelper.Combine(
                evidenceRoot,
                _pathHelper.SanitizeFileName(context.ScenarioName),
                context.CorrelationId));
    }

    public string CreateArtifactPath(string evidenceRoot, CmosTestContext context, string artifactType, string fileName, string? stepName = null)
    {
        var scenarioRoot = GetScenarioRoot(evidenceRoot, context);
        var artifactDirectory = string.IsNullOrWhiteSpace(stepName)
            ? _pathHelper.Combine(scenarioRoot, _pathHelper.SanitizeFileName(artifactType))
            : _pathHelper.Combine(scenarioRoot, _pathHelper.SanitizeFileName(stepName), _pathHelper.SanitizeFileName(artifactType));

        _pathHelper.EnsureDirectory(artifactDirectory);
        return _pathHelper.Combine(artifactDirectory, _pathHelper.SanitizeFileName(fileName));
    }
}
