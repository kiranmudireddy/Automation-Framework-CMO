namespace CMOS_Automation_Framework.src.Models.ValidationModels;

public record ValidationResult(
    string Assertion,
    bool Passed,
    string Details);
