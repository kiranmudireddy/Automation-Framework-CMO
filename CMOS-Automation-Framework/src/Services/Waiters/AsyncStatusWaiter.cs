using CMOS_Automation_Framework.src.Models.ContextModels;

namespace CMOS_Automation_Framework.src.Services.Waiters;

public class AsyncStatusWaiter
{
    public async Task<bool> WaitUntilAsync(Func<Task<string>> statusProvider, WaitPolicy policy, CancellationToken cancellationToken = default)
    {
        var timeoutAt = DateTime.UtcNow.Add(policy.Timeout);

        while (DateTime.UtcNow <= timeoutAt)
        {
            cancellationToken.ThrowIfCancellationRequested();

            if (string.Equals(await statusProvider(), policy.ExpectedStatus, StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }

            await Task.Delay(policy.PollingInterval, cancellationToken);
        }

        return false;
    }
}
