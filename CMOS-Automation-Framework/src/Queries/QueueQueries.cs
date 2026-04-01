using CMOS_Automation_Framework.src.Models.DbModels;

namespace CMOS_Automation_Framework.src.Queries;

public static class QueueQueries
{
    public static DatabaseQueryDefinition ByReference(string reference) =>
        new(
            "QueueByReference",
            "SELECT * FROM PIQ WHERE QueueReference = ?",
            [new DatabaseQueryParameter("QueueReference", reference)]);
}
