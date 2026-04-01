namespace CMOS_Automation_Framework.src.Models.DbModels;

public record DatabaseQueryDefinition(
    string QueryName,
    string Sql,
    IReadOnlyList<DatabaseQueryParameter> Parameters);
