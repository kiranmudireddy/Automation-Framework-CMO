using CMOS_Automation_Framework.src.Models.DbModels;

namespace CMOS_Automation_Framework.src.Queries;

public static class MandateQueries
{
    public static DatabaseQueryDefinition ByMandateId(string mandateId) =>
        new(
            "MandateById",
            "SELECT * FROM PICC WHERE MandateId = ?",
            [new DatabaseQueryParameter("MandateId", mandateId)]);
}
