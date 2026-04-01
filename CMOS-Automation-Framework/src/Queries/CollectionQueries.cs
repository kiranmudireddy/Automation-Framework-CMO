using CMOS_Automation_Framework.src.Models.DbModels;

namespace CMOS_Automation_Framework.src.Queries;

public static class CollectionQueries
{
    public static DatabaseQueryDefinition ByCollectionReference(string collectionReference) =>
        new(
            "CollectionByReference",
            "SELECT * FROM CC WHERE CollectionReference = ?",
            [new DatabaseQueryParameter("CollectionReference", collectionReference)]);
}
