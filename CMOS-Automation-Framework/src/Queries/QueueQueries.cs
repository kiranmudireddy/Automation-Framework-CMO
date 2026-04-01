namespace CMOS_Automation_Framework.src.Queries;

public static class QueueQueries
{
    public static string ByReference(string reference) =>
        $"SELECT * FROM PIQ WHERE QueueReference = '{reference}'";
}
