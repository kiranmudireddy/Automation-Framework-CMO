namespace CMOS_Automation_Framework.src.Utils.Common;

public class PathHelper
{
    public string Combine(params string[] segments)
    {
        return Path.Combine(segments);
    }

    public string EnsureDirectory(string path)
    {
        Directory.CreateDirectory(path);
        return path;
    }

    public string CreateScenarioArtifactPath(string rootDirectory, string scenarioName, string correlationId, string fileName)
    {
        var scenarioDirectory = Path.Combine(
            rootDirectory,
            SanitizeFileName(scenarioName),
            correlationId);

        Directory.CreateDirectory(scenarioDirectory);
        return Path.Combine(scenarioDirectory, SanitizeFileName(fileName));
    }

    public string SanitizeFileName(string value)
    {
        var invalidCharacters = Path.GetInvalidFileNameChars();
        var sanitized = new string(value.Select(character => invalidCharacters.Contains(character) ? '_' : character).ToArray());
        return string.IsNullOrWhiteSpace(sanitized) ? "artifact" : sanitized;
    }
}
