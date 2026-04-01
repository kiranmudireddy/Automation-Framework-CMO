namespace CMOS_Automation_Framework.src.Drivers.Web;

public class WebDriverFactory : IWebDriverFactory
{
    public IWebDriver Create(string browser)
    {
        return browser.Trim().ToLowerInvariant() switch
        {
            "chrome" => CreateChromeDriver(),
            "firefox" => CreateFirefoxDriver(),
            "edge" => CreateEdgeDriver(),
            _ => throw new ArgumentOutOfRangeException(nameof(browser), browser, "Supported browsers are chrome, firefox, and edge.")
        };
    }

    private static IWebDriver CreateChromeDriver()
    {
        var options = new ChromeOptions();
        options.AddArgument("--start-maximized");
        return new ChromeDriver(options);
    }

    private static IWebDriver CreateFirefoxDriver()
    {
        var options = new FirefoxOptions();
        options.AddArgument("--width=1920");
        options.AddArgument("--height=1080");
        return new FirefoxDriver(options);
    }

    private static IWebDriver CreateEdgeDriver()
    {
        var options = new EdgeOptions();
        options.AddArgument("--start-maximized");
        return new EdgeDriver(options);
    }
}
