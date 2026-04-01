using System.Net;
using CMOS_Automation_Framework.src.Models.ApiModels;
using CMOS_Automation_Framework.src.Models.ContextModels;
using CMOS_Automation_Framework.src.Services.Api;
using CMOS_Automation_Framework.src.Services.Evidence;
using CMOS_Automation_Framework.src.Utils.Common;

namespace CMOS_Automation_Framework.src.Helpers.Api;

public class ApiRequestHelper : IApiRequestHelper
{
    private readonly CmosApiService _apiService;
    private readonly EvidenceCollector _evidenceCollector;
    private readonly RetryHelper _retryHelper;

    public ApiRequestHelper(CmosApiService apiService, EvidenceCollector evidenceCollector, RetryHelper retryHelper)
    {
        _apiService = apiService;
        _evidenceCollector = evidenceCollector;
        _retryHelper = retryHelper;
    }

    public async Task<ApiResponseContext> ExecuteAsync(
        CmosTestContext context,
        string endpoint,
        Method method,
        object? payload = null,
        Dictionary<string, string>? headers = null,
        bool retryTransientFailures = false,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(context);

        var requestContext = new ApiRequestContext(endpoint, method, payload, headers);
        _evidenceCollector.CaptureApiRequest(context, requestContext);

        async Task<ApiResponseContext> ExecuteCoreAsync()
        {
            var response = await _apiService.SendAsync(endpoint, method, payload, headers, cancellationToken);

            if (retryTransientFailures && IsTransient(response.StatusCode))
            {
                throw new TransientApiException(response);
            }

            return response;
        }

        ApiResponseContext finalResponse;

        try
        {
            finalResponse = retryTransientFailures
                ? await _retryHelper.ExecuteAsync(ExecuteCoreAsync, 3, TimeSpan.FromSeconds(2), cancellationToken)
                : await ExecuteCoreAsync();
        }
        catch (TransientApiException exception)
        {
            finalResponse = exception.Response;
        }

        _evidenceCollector.CaptureApiResponse(context, finalResponse);
        return finalResponse;
    }

    private static bool IsTransient(HttpStatusCode statusCode)
    {
        var numericStatusCode = (int)statusCode;
        return numericStatusCode is >= 500 and < 600;
    }

    private sealed class TransientApiException : Exception
    {
        public TransientApiException(ApiResponseContext response)
            : base($"Transient API response received: {(int)response.StatusCode}")
        {
            Response = response;
        }

        public ApiResponseContext Response { get; }
    }
}
