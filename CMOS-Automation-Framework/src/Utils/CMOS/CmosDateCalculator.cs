namespace CMOS_Automation_Framework.src.Utils.CMOS;

public class CmosDateCalculator
{
    public DateTime GetCurrentBusinessDate()
    {
        return DateTime.Today;
    }

    public DateTime GetNextCollectionDate(DateTime startDate, int daysToAdd)
    {
        return startDate.Date.AddDays(daysToAdd);
    }
}
