using CMOS_Automation_Framework.src.Config.Settings;
using CMOS_Automation_Framework.src.Services.Db;
using NUnit.Framework;

namespace CMOS_Automation_Framework.tests;

public class DbSmokeTest
{
    [Test]
    public void Should_Open_Iris_Connection_And_Run_Scalar_Query()
    {
        var configuration = EnvironmentSettings.LoadConfiguration();
        var connectionFactory = new IrisConnectionFactory(configuration);
        var queryExecutor = new QueryExecutor(connectionFactory);

        var result = queryExecutor.ExecuteScalar("SELECT 1");

        Assert.That(result, Is.Not.Null);
    }
}