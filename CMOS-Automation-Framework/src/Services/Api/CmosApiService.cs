using CMOS_Automation_Framework.src.API.CMOS.Auth;
using CMOS_Automation_Framework.src.API.CMOS.Clients;
using CMOS_Automation_Framework.src.Models.ApiModels;

namespace CMOS_Automation_Framework.src.Services.Api;

public class CmosApiService
{
    private readonly CmosApiClient _client;
    private readonly HeaderProvider _headerProvider;

    public CmosApiService(CmosApiClient client, HeaderProvider headerProvider)
    {
        _client = client;
        _headerProvider = headerProvider;
    }

    public Task<ApiResponseContext> SendAsync(string endpoint, Method method, object? payload = null, Dictionary<string, string>? headers = null, CancellationToken cancellationToken = default)
    {
        var request = new ApiRequestContext(endpoint, method, payload, _headerProvider.BuildHeaders(headers));
        return _client.ExecuteAsync(request, cancellationToken);
    }
}
