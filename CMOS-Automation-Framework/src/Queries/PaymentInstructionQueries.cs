using CMOS_Automation_Framework.src.Models.DbModels;

namespace CMOS_Automation_Framework.src.Queries;

public static class PaymentInstructionQueries
{
    public static DatabaseQueryDefinition ByInstructionId(string instructionId) =>
        new(
            "PaymentInstructionById",
            "SELECT * FROM PI WHERE PaymentInstructionId = ?",
            [new DatabaseQueryParameter("PaymentInstructionId", instructionId)]);
}
