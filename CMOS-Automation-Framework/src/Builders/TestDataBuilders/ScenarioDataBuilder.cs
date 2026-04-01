namespace CMOS_Automation_Framework.src.Builders.TestDataBuilders;

public class ScenarioDataBuilder
{
    public Dictionary<string, object> BuildHappyPathData()
    {
        return new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase)
        {
            ["AccountNumber"] = "4100001234567890",
            ["Amount"] = 150.25m,
            ["MandateId"] = "MANDATE-001",
            ["CollectionReference"] = $"COLL-{DateTime.UtcNow:yyyyMMddHHmmss}"
        };
    }
}
