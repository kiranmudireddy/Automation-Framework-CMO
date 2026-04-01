using CMOS_Automation_Framework.src.Models.ApiModels;

namespace CMOS_Automation_Framework.src.API.CMOS.Clients;

public interface ICmosApiClient
{
    Task<ApiResponseContext> ExecuteAsync(ApiRequestContext requestContext, CancellationToken cancellationToken = default);
}
