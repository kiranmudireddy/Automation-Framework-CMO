namespace CMOS_Automation_Framework.src.Queries;

public static class FileProcessingQueries
{
    public static string ByFileReference(string fileReference) =>
        $"SELECT * FROM FPC WHERE FileReference = '{fileReference}'";
}
