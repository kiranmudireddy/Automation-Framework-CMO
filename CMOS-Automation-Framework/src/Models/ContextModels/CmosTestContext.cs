using CMOS_Automation_Framework.src.Models.ApiModels;
using CMOS_Automation_Framework.src.Models.DbModels;
using CMOS_Automation_Framework.src.Models.FileModels;
using CMOS_Automation_Framework.src.Models.ValidationModels;

namespace CMOS_Automation_Framework.src.Models.ContextModels;

public class CmosTestContext
{
    public string ScenarioName { get; set; } = string.Empty;
    public string CorrelationId { get; set; } = Guid.NewGuid().ToString("N");
    public Dictionary<string, object> Inputs { get; } = new(StringComparer.OrdinalIgnoreCase);
    public List<ApiRequestContext> ApiRequests { get; } = [];
    public List<ApiResponseContext> ApiResponses { get; } = [];
    public List<FileArtifact> Files { get; } = [];
    public List<DatabaseSnapshot> DatabaseSnapshots { get; } = [];
    public List<ValidationResult> ValidationResults { get; } = [];
    public string Status { get; set; } = "NotStarted";
}
