using CMOS_Automation_Framework.src.Models.ApiModels;
using CMOS_Automation_Framework.src.Models.ContextModels;

namespace CMOS_Automation_Framework.src.Helpers.Api;

public interface IApiRequestHelper
{
    Task<ApiResponseContext> ExecuteAsync(
        CmosTestContext context,
        string endpoint,
        Method method,
        object? payload = null,
        Dictionary<string, string>? headers = null,
        bool retryTransientFailures = false,
        CancellationToken cancellationToken = default);
}
