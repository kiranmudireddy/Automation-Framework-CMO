namespace CMOS_Automation_Framework.src.Queries;

public static class CollectionQueries
{
    public static string ByCollectionReference(string collectionReference) =>
        $"SELECT * FROM CC WHERE CollectionReference = '{collectionReference}'";
}
