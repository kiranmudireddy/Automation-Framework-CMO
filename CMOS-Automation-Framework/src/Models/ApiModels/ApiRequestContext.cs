namespace CMOS_Automation_Framework.src.Models.ApiModels;

public record ApiRequestContext(
    string EndpointName,
    Method HttpMethod,
    object? Payload,
    Dictionary<string, string>? Headers = null);
