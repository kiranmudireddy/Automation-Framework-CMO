using CMOS_Automation_Framework.src.Models.ContextModels;
using ReqnrollScenarioContext = Reqnroll.ScenarioContext;

namespace CMOS_Automation_Framework.src.Hooks;

[Binding]
public class ScenarioHooks
{
    private readonly ReqnrollScenarioContext _scenarioContext;

    public ScenarioHooks(ReqnrollScenarioContext scenarioContext)
    {
        _scenarioContext = scenarioContext;
    }

    [BeforeScenario]
    public void BeforeScenario()
    {
        _scenarioContext.Set(new CmosTestContext
        {
            ScenarioName = _scenarioContext.ScenarioInfo.Title,
            Status = "Running"
        });
    }
}
