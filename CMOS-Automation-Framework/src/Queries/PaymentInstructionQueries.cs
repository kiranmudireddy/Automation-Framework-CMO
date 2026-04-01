namespace CMOS_Automation_Framework.src.Queries;

public static class PaymentInstructionQueries
{
    public static string ByInstructionId(string instructionId) =>
        $"SELECT * FROM PI WHERE PaymentInstructionId = '{instructionId}'";
}
