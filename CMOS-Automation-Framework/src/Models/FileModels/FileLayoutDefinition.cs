namespace CMOS_Automation_Framework.src.Models.FileModels;

public record FileLayoutDefinition(
    string FileType,
    string Version,
    string HeaderPrefix,
    string TrailerPrefix,
    char Delimiter = ',');
