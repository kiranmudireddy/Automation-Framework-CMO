using CMOS_Automation_Framework.src.Config;
using CMOS_Automation_Framework.src.Models.ContextModels;
using Reqnroll.BoDi;
using ReqnrollScenarioContext = Reqnroll.ScenarioContext;

namespace CMOS_Automation_Framework.src.Hooks;

[Binding]
public class ScenarioHooks
{
    private readonly IObjectContainer _objectContainer;
    private readonly ReqnrollScenarioContext _scenarioContext;

    public ScenarioHooks(ReqnrollScenarioContext scenarioContext, IObjectContainer objectContainer)
    {
        _scenarioContext = scenarioContext;
        _objectContainer = objectContainer;
    }

    [BeforeScenario]
    public void BeforeScenario()
    {
        var context = new CmosTestContext
        {
            ScenarioName = _scenarioContext.ScenarioInfo.Title,
            Status = "Running"
        };

        var provider = new FrameworkServiceProviderFactory().Create();
        _scenarioContext.Set(context);
        _scenarioContext.Set(provider);
        provider.RegisterScenarioServices(_objectContainer, context);
    }

    [AfterScenario]
    public void AfterScenario()
    {
        try
        {
            _scenarioContext.Get<ServiceProvider>()?.Dispose();
        }
        catch (KeyNotFoundException)
        {
        }
    }
}
