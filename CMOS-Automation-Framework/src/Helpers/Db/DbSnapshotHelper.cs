using CMOS_Automation_Framework.src.Models.ContextModels;
using CMOS_Automation_Framework.src.Models.DbModels;
using CMOS_Automation_Framework.src.Services.Db;
using CMOS_Automation_Framework.src.Services.Evidence;
using CMOS_Automation_Framework.src.Utils.Common;
namespace CMOS_Automation_Framework.src.Helpers.Db;

public class DbSnapshotHelper : IDbSnapshotHelper
{
    private readonly DatabaseQueryService _databaseQueryService;
    private readonly EvidenceCollector _evidenceCollector;
    private readonly JsonHelper _jsonHelper;

    public DbSnapshotHelper(DatabaseQueryService databaseQueryService, EvidenceCollector evidenceCollector, JsonHelper jsonHelper)
    {
        _databaseQueryService = databaseQueryService;
        _evidenceCollector = evidenceCollector;
        _jsonHelper = jsonHelper;
    }

    public DatabaseSnapshot CaptureSnapshot(string queryName, string sql)
    {
        return _databaseQueryService.CaptureSnapshot(queryName, sql);
    }

    public DatabaseSnapshot CaptureSnapshot(DatabaseQueryDefinition queryDefinition)
    {
        return _databaseQueryService.CaptureSnapshot(queryDefinition);
    }

    public DatabaseSnapshot CaptureAndAttach(CmosTestContext context, string queryName, string sql)
    {
        var snapshot = CaptureSnapshot(queryName, sql);
        _evidenceCollector.CaptureDatabaseSnapshot(context, snapshot);
        return snapshot;
    }

    public DatabaseSnapshot CaptureAndAttach(CmosTestContext context, DatabaseQueryDefinition queryDefinition)
    {
        var snapshot = CaptureSnapshot(queryDefinition);
        _evidenceCollector.CaptureDatabaseSnapshot(context, snapshot);
        return snapshot;
    }

    public string SerializeSnapshot(DatabaseSnapshot snapshot)
    {
        ArgumentNullException.ThrowIfNull(snapshot);
        return _jsonHelper.Serialize(snapshot.Rows);
    }

    public DatabaseSnapshotComparisonResult Compare(DatabaseSnapshot beforeSnapshot, DatabaseSnapshot afterSnapshot)
    {
        ArgumentNullException.ThrowIfNull(beforeSnapshot);
        ArgumentNullException.ThrowIfNull(afterSnapshot);

        var beforeRows = beforeSnapshot.Rows;
        var afterRows = afterSnapshot.Rows;

        return new DatabaseSnapshotComparisonResult(
            beforeSnapshot.QueryName,
            beforeRows.Count,
            afterRows.Count,
            afterRows.Count - beforeRows.Count,
            beforeRows.SequenceEqual(afterRows));
    }
}
