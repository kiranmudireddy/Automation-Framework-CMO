using System.Net;

namespace CMOS_Automation_Framework.src.Models.ApiModels;

public record ApiResponseContext(
    bool IsSuccessful,
    HttpStatusCode StatusCode,
    string Content,
    IReadOnlyDictionary<string, string> Headers);
