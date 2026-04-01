using DataTable = System.Data.DataTable;

namespace CMOS_Automation_Framework.src.Models.DbModels;

public record DatabaseSnapshot(
    string QueryName,
    string Sql,
    DataTable Result);
