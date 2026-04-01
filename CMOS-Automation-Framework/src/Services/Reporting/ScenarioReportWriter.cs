using System.Text;
using CMOS_Automation_Framework.src.Models.ContextModels;

namespace CMOS_Automation_Framework.src.Services.Reporting;

public class ScenarioReportWriter
{
    public string WriteManagerReport(CmosTestContext context, string reportDirectory)
    {
        Directory.CreateDirectory(reportDirectory);

        var fullPath = Path.Combine(reportDirectory, $"{context.ScenarioName.Replace(' ', '_')}_{context.CorrelationId}.md");
        var builder = new StringBuilder();
        builder.AppendLine($"# {context.ScenarioName}");
        builder.AppendLine();
        builder.AppendLine($"- Final status: {context.Status}");
        builder.AppendLine($"- Correlation ID: {context.CorrelationId}");
        builder.AppendLine($"- Inputs used: {context.Inputs.Count}");
        builder.AppendLine($"- API calls captured: {context.ApiResponses.Count}");
        builder.AppendLine($"- Files generated: {context.Files.Count}");
        builder.AppendLine($"- DB snapshots: {context.DatabaseSnapshots.Count}");
        builder.AppendLine();
        builder.AppendLine("## Validation results");

        foreach (var validation in context.ValidationResults)
        {
            builder.AppendLine($"- {(validation.Passed ? "PASS" : "FAIL")}: {validation.Assertion} ({validation.Details})");
        }

        System.IO.File.WriteAllText(fullPath, builder.ToString());
        return fullPath;
    }
}
