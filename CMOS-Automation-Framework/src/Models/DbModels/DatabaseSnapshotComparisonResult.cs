namespace CMOS_Automation_Framework.src.Models.DbModels;

public record DatabaseSnapshotComparisonResult(
    string QueryName,
    int BeforeRowCount,
    int AfterRowCount,
    int RowCountDelta,
    bool IsIdentical);
