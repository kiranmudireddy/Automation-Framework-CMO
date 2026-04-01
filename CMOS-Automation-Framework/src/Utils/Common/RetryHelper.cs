namespace CMOS_Automation_Framework.src.Utils.Common;

public class RetryHelper
{
    public async Task<T> ExecuteAsync<T>(Func<Task<T>> action, int maxAttempts, TimeSpan delay, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(action);

        Exception? lastException = null;

        for (var attempt = 1; attempt <= maxAttempts; attempt++)
        {
            cancellationToken.ThrowIfCancellationRequested();

            try
            {
                return await action();
            }
            catch (Exception exception) when (attempt < maxAttempts)
            {
                lastException = exception;
                await Task.Delay(delay, cancellationToken);
            }
        }

        throw new InvalidOperationException("Retry operation failed without capturing the final exception.", lastException);
    }

    public async Task ExecuteAsync(Func<Task> action, int maxAttempts, TimeSpan delay, CancellationToken cancellationToken = default)
    {
        await ExecuteAsync(async () =>
        {
            await action();
            return true;
        }, maxAttempts, delay, cancellationToken);
    }
}
