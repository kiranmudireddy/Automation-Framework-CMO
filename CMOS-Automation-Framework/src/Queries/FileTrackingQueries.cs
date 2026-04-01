using CMOS_Automation_Framework.src.Models.DbModels;

namespace CMOS_Automation_Framework.src.Queries;

public static class FileTrackingQueries
{
    public static DatabaseQueryDefinition ByFileName(string fileName) =>
        new(
            "FileTrackingByName",
            "SELECT * FROM PIT WHERE FileName = ?",
            [new DatabaseQueryParameter("FileName", fileName)]);
}
