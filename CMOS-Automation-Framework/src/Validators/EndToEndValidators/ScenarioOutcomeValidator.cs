using CMOS_Automation_Framework.src.Models.ValidationModels;

namespace CMOS_Automation_Framework.src.Validators.EndToEndValidators;

public class ScenarioOutcomeValidator
{
    public ValidationResult ValidateAll(params ValidationResult[] validations)
    {
        var failed = validations.Where(validation => !validation.Passed).ToList();
        var passed = failed.Count == 0;

        return new ValidationResult(
            "End-to-end business flow validated",
            passed,
            passed ? "All underlying API, file, and DB validations passed." : $"Failed assertions: {string.Join(", ", failed.Select(validation => validation.Assertion))}");
    }
}
