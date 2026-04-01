using Microsoft.Extensions.Configuration;
using System;

namespace CMOS_Automation_Framework.src.Config.Settings;

public static class EnvironmentSettings
{
    public static IConfiguration LoadConfiguration()
    {
        var environment = Environment.GetEnvironmentVariable("TEST_ENV") ?? "LOCAL";

        return new ConfigurationBuilder()
            .SetBasePath(AppContext.BaseDirectory)
            .AddJsonFile($"appsettings.{environment}.json", optional: false, reloadOnChange: true)
            .Build();
    }
}