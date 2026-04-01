namespace CMOS_Automation_Framework.src.Models.ContextModels;

public record WaitPolicy(
    TimeSpan Timeout,
    TimeSpan PollingInterval,
    string ExpectedStatus);
