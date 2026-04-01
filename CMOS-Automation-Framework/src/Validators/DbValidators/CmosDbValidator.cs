using CMOS_Automation_Framework.src.Constants;
using CMOS_Automation_Framework.src.Models.DbModels;
using CMOS_Automation_Framework.src.Models.ValidationModels;

namespace CMOS_Automation_Framework.src.Validators.DbValidators;

public class CmosDbValidator
{
    public ValidationResult ValidateTransactionCreated(DatabaseSnapshot snapshot)
    {
        var passed = snapshot.Result.Rows.Count > 0;
        return new ValidationResult(CustomMessages.TransactionCreated, passed, passed ? "Transaction row found in IRIS." : "No transaction row was found in IRIS.");
    }

    public ValidationResult ValidateQueueCreated(DatabaseSnapshot snapshot)
    {
        var passed = snapshot.Result.Rows.Count > 0;
        return new ValidationResult(CustomMessages.QueueEntryCreated, passed, passed ? "Queue entry located." : "Expected queue entry was not found.");
    }
}
