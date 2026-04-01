namespace CMOS_Automation_Framework.src.Config;

public class FrameworkServiceProviderFactory
{
    public ServiceProvider Create(string? environmentName = null)
    {
        var configurationManager = environmentName is null
            ? new FrameworkConfigurationManager()
            : new FrameworkConfigurationManager(environmentName);

        var services = new ServiceCollection();
        services.AddCmosFramework(configurationManager, configurationManager.LoadEnvironment());
        return services.BuildServiceProvider(validateScopes: true);
    }
}
