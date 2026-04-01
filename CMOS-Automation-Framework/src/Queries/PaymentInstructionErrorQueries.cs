using CMOS_Automation_Framework.src.Models.DbModels;

namespace CMOS_Automation_Framework.src.Queries;

public static class PaymentInstructionErrorQueries
{
    public static DatabaseQueryDefinition ByInstructionId(string instructionId) =>
        new(
            "PaymentInstructionErrorById",
            "SELECT * FROM PIE WHERE PaymentInstructionId = ?",
            [new DatabaseQueryParameter("PaymentInstructionId", instructionId)]);
}
