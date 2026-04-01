using CMOS_Automation_Framework.src.Constants;
using CMOS_Automation_Framework.src.Models.FileModels;
using CMOS_Automation_Framework.src.Models.ValidationModels;

namespace CMOS_Automation_Framework.src.Validators.FileValidators;

public class FileProcessingValidator
{
    public ValidationResult ValidateCounts(FileArtifact fileArtifact, int expectedCount)
    {
        var passed = fileArtifact.RecordCount == expectedCount;
        return new ValidationResult("File counts balanced", passed, passed ? "Record count matches the expected total." : $"Expected {expectedCount} records but found {fileArtifact.RecordCount}.");
    }

    public ValidationResult ValidateCompletedStatus(string status)
    {
        var passed = string.Equals(status, "Completed", StringComparison.OrdinalIgnoreCase);
        return new ValidationResult(CustomMessages.FileCompleted, passed, passed ? "File status progressed to Completed." : $"Current file status is {status}.");
    }
}
