namespace CMOS_Automation_Framework.src.Queries;

public static class FileTrackingQueries
{
    public static string ByFileName(string fileName) =>
        $"SELECT * FROM PIT WHERE FileName = '{fileName}'";
}
