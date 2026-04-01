namespace CMOS_Automation_Framework.src.Drivers.Web;

public interface IWebDriverFactory
{
    IWebDriver Create(string browser);
}
