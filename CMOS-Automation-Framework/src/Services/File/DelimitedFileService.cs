using System.Globalization;
using CMOS_Automation_Framework.src.Models.FileModels;
using CMOS_Automation_Framework.src.Models.ValidationModels;

namespace CMOS_Automation_Framework.src.Services.File;

public class DelimitedFileService
{
    public FileArtifact GenerateFile(FileLayoutDefinition layout, IEnumerable<string> bodyLines, string outputDirectory, string fileName, decimal totalAmount)
    {
        Directory.CreateDirectory(outputDirectory);

        var lines = bodyLines.ToList();
        var fullPath = Path.Combine(outputDirectory, fileName);
        var trailer = $"{layout.TrailerPrefix}{layout.Delimiter}{lines.Count.ToString(CultureInfo.InvariantCulture)}{layout.Delimiter}{totalAmount.ToString("0.00", CultureInfo.InvariantCulture)}";

        System.IO.File.WriteAllLines(fullPath,
        [
            $"{layout.HeaderPrefix}{layout.Delimiter}{layout.FileType}{layout.Delimiter}{layout.Version}",
            .. lines,
            trailer
        ]);

        return new FileArtifact(layout.FileType, layout.Version, fileName, fullPath, lines.Count, totalAmount);
    }

    public IReadOnlyList<string> ReadRecords(string fullPath)
    {
        return System.IO.File.ReadAllLines(fullPath);
    }

    public ValidationResult ValidateHeaderAndTrailer(FileLayoutDefinition layout, IReadOnlyList<string> records)
    {
        var valid = records.Count >= 2 &&
                    records[0].StartsWith(layout.HeaderPrefix, StringComparison.OrdinalIgnoreCase) &&
                    records[^1].StartsWith(layout.TrailerPrefix, StringComparison.OrdinalIgnoreCase);

        return new ValidationResult(
            "Header and trailer validated",
            valid,
            valid ? "Expected control records are present." : "File control records are missing or malformed.");
    }
}
