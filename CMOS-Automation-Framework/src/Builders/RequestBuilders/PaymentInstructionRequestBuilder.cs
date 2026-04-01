namespace CMOS_Automation_Framework.src.Builders.RequestBuilders;

public class PaymentInstructionRequestBuilder
{
    public object Build(string accountNumber, decimal amount, string mandateId)
    {
        return new
        {
            accountNumber,
            amount,
            mandateId
        };
    }
}
