using CMOS_Automation_Framework.src.Utils.Logging;

namespace CMOS_Automation_Framework.src.Hooks;

[Binding]
public class TestRunHooks
{
    [BeforeTestRun]
    public static void BeforeTestRun()
    {
        LoggerConfig.Configure();
    }

    [AfterTestRun]
    public static void AfterTestRun()
    {
        LoggerConfig.CloseAndFlush();
    }
}
