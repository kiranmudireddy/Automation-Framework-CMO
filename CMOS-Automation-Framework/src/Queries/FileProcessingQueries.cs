using CMOS_Automation_Framework.src.Models.DbModels;

namespace CMOS_Automation_Framework.src.Queries;

public static class FileProcessingQueries
{
    public static DatabaseQueryDefinition ByFileReference(string fileReference) =>
        new(
            "FileProcessingByReference",
            "SELECT * FROM FPC WHERE FileReference = ?",
            [new DatabaseQueryParameter("FileReference", fileReference)]);
}
