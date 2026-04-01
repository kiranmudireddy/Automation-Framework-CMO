using System.Text.Json;
using CMOS_Automation_Framework.src.Models.ApiModels;
using CMOS_Automation_Framework.src.Models.ContextModels;
using CMOS_Automation_Framework.src.Models.DbModels;
using CMOS_Automation_Framework.src.Models.FileModels;

namespace CMOS_Automation_Framework.src.Services.Evidence;

public class EvidenceCollector
{
    public void CaptureInput(CmosTestContext context, string key, object value)
    {
        context.Inputs[key] = value;
    }

    public void CaptureApiRequest(CmosTestContext context, ApiRequestContext request)
    {
        context.ApiRequests.Add(request);
    }

    public void CaptureApiResponse(CmosTestContext context, ApiResponseContext response)
    {
        context.ApiResponses.Add(response);
    }

    public void CaptureFile(CmosTestContext context, FileArtifact fileArtifact)
    {
        context.Files.Add(fileArtifact);
    }

    public void CaptureDatabaseSnapshot(CmosTestContext context, DatabaseSnapshot snapshot)
    {
        context.DatabaseSnapshots.Add(snapshot);
    }

    public string Persist(CmosTestContext context, string evidenceRoot)
    {
        var scenarioDirectory = Path.Combine(evidenceRoot, context.ScenarioName.Replace(' ', '_'), context.CorrelationId);
        Directory.CreateDirectory(scenarioDirectory);

        var payload = JsonSerializer.Serialize(context, new JsonSerializerOptions { WriteIndented = true });
        var evidencePath = Path.Combine(scenarioDirectory, "scenario-evidence.json");
        System.IO.File.WriteAllText(evidencePath, payload);

        return evidencePath;
    }
}
