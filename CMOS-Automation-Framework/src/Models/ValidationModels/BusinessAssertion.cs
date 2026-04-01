namespace CMOS_Automation_Framework.src.Models.ValidationModels;

public record BusinessAssertion(
    string Name,
    bool Passed,
    string EvidenceReference);
