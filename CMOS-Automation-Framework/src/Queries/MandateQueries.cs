namespace CMOS_Automation_Framework.src.Queries;

public static class MandateQueries
{
    public static string ByMandateId(string mandateId) =>
        $"SELECT * FROM PICC WHERE MandateId = '{mandateId}'";
}
