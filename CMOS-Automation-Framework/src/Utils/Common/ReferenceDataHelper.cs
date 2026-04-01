namespace CMOS_Automation_Framework.src.Utils.Common;

public class ReferenceDataHelper
{
    public string CreateCorrelationId(string prefix = "CMOS")
    {
        return $"{prefix}-{DateTime.UtcNow:yyyyMMddHHmmss}-{Guid.NewGuid().ToString("N")[..8]}";
    }

    public string CreateFileName(string prefix, string extension)
    {
        var normalizedExtension = extension.StartsWith('.') ? extension : $".{extension}";
        return $"{prefix}_{DateTime.UtcNow:yyyyMMddHHmmss}{normalizedExtension}";
    }

    public string CreateSequenceReference(string prefix)
    {
        return $"{prefix}{DateTime.UtcNow:yyyyMMddHHmmssfff}";
    }
}
