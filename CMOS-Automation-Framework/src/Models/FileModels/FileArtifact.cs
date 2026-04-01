namespace CMOS_Automation_Framework.src.Models.FileModels;

public record FileArtifact(
    string FileType,
    string Version,
    string FileName,
    string FullPath,
    int RecordCount,
    decimal TotalAmount);
