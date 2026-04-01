using CMOS_Automation_Framework.src.Models.ContextModels;
using CMOS_Automation_Framework.src.Models.DbModels;

namespace CMOS_Automation_Framework.src.Helpers.Db;

public interface IDbSnapshotHelper
{
    DatabaseSnapshot CaptureSnapshot(string queryName, string sql);
    DatabaseSnapshot CaptureSnapshot(DatabaseQueryDefinition queryDefinition);
    DatabaseSnapshot CaptureAndAttach(CmosTestContext context, string queryName, string sql);
    DatabaseSnapshot CaptureAndAttach(CmosTestContext context, DatabaseQueryDefinition queryDefinition);
    string SerializeSnapshot(DatabaseSnapshot snapshot);
    DatabaseSnapshotComparisonResult Compare(DatabaseSnapshot beforeSnapshot, DatabaseSnapshot afterSnapshot);
}
