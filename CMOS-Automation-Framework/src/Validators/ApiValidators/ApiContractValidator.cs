using CMOS_Automation_Framework.src.Models.ApiModels;
using CMOS_Automation_Framework.src.Models.ValidationModels;

namespace CMOS_Automation_Framework.src.Validators.ApiValidators;

public class ApiContractValidator
{
    public ValidationResult ValidateSuccessfulResponse(ApiResponseContext response)
    {
        var passed = response.IsSuccessful && (int)response.StatusCode < 400;
        return new ValidationResult("API response accepted", passed, passed ? "HTTP response is within the expected success range." : $"Unexpected status code: {(int)response.StatusCode}");
    }
}
