namespace CMOS_Automation_Framework.src.Queries;

public static class PaymentInstructionErrorQueries
{
    public static string ByInstructionId(string instructionId) =>
        $"SELECT * FROM PIE WHERE PaymentInstructionId = '{instructionId}'";
}
