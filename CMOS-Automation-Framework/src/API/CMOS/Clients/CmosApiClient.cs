using CMOS_Automation_Framework.src.Config;
using CMOS_Automation_Framework.src.Models.ApiModels;

namespace CMOS_Automation_Framework.src.API.CMOS.Clients;

public class CmosApiClient
{
    private readonly RestClient _client;

    public CmosApiClient(ApiSettings apiSettings)
    {
        _client = new RestClient(new RestClientOptions(apiSettings.BaseUrl)
        {
            Timeout = TimeSpan.FromSeconds(30)
        });
    }

    public async Task<ApiResponseContext> ExecuteAsync(ApiRequestContext requestContext, CancellationToken cancellationToken = default)
    {
        var request = new RestRequest(requestContext.EndpointName, requestContext.HttpMethod);

        if (requestContext.Headers is not null)
        {
            foreach (var header in requestContext.Headers)
            {
                request.AddHeader(header.Key, header.Value);
            }
        }

        if (requestContext.Payload is not null)
        {
            request.AddJsonBody(requestContext.Payload);
        }

        var response = await _client.ExecuteAsync(request, cancellationToken);
        var headers = response.Headers?.ToDictionary(
            header => header.Name ?? string.Empty,
            header => header.Value?.ToString() ?? string.Empty,
            StringComparer.OrdinalIgnoreCase) ?? new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

        return new ApiResponseContext(
            response.IsSuccessful,
            response.StatusCode,
            response.Content ?? string.Empty,
            headers);
    }
}
