namespace CMOS_Automation_Framework.src.Models.DbModels;

public record DatabaseSnapshot(
    string QueryName,
    IReadOnlyList<Dictionary<string, object?>> Rows);
