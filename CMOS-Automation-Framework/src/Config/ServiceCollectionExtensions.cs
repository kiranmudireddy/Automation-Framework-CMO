using CMOS_Automation_Framework.src.API.CMOS.Auth;
using CMOS_Automation_Framework.src.API.CMOS.Clients;
using CMOS_Automation_Framework.src.Drivers.Sftp;
using CMOS_Automation_Framework.src.Drivers.Web;
using CMOS_Automation_Framework.src.Models.ContextModels;
using CMOS_Automation_Framework.src.Services.Api;
using CMOS_Automation_Framework.src.Services.Db;
using CMOS_Automation_Framework.src.Services.Evidence;
using CMOS_Automation_Framework.src.Services.File;
using CMOS_Automation_Framework.src.Services.Orchestration;
using CMOS_Automation_Framework.src.Services.Reporting;
using CMOS_Automation_Framework.src.Services.Sftp;
using CMOS_Automation_Framework.src.Services.Waiters;
using CMOS_Automation_Framework.src.Validators.ApiValidators;
using CMOS_Automation_Framework.src.Validators.DbValidators;
using CMOS_Automation_Framework.src.Validators.EndToEndValidators;
using CMOS_Automation_Framework.src.Validators.FileValidators;
using CMOS_Automation_Framework.src.Builders.FileBuilders;
using CMOS_Automation_Framework.src.Builders.RequestBuilders;
using CMOS_Automation_Framework.src.Builders.TestDataBuilders;
using CMOS_Automation_Framework.src.Helpers.Api;
using CMOS_Automation_Framework.src.Helpers.Db;
using CMOS_Automation_Framework.src.Helpers.Evidence;
using CMOS_Automation_Framework.src.Helpers.File;
using CMOS_Automation_Framework.src.Utils.Common;

namespace CMOS_Automation_Framework.src.Config;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddCmosFramework(this IServiceCollection services, FrameworkConfigurationManager configurationManager, EnvironmentSettings environmentSettings)
    {
        services.AddSingleton(configurationManager);
        services.AddSingleton(environmentSettings);
        services.AddSingleton(environmentSettings.Api);
        services.AddSingleton(environmentSettings.Db);
        services.AddSingleton(environmentSettings.Sftp);
        services.AddSingleton(environmentSettings.Framework);

        services.AddSingleton<HeaderProvider>();
        services.AddSingleton<CmosApiClient>();
        services.AddSingleton<ICmosApiClient>(serviceProvider => serviceProvider.GetRequiredService<CmosApiClient>());
        services.AddSingleton<CmosApiService>();
        services.AddSingleton<IrisConnectionFactory>();
        services.AddSingleton<DatabaseQueryService>();
        services.AddSingleton<DelimitedFileService>();
        services.AddSingleton<SftpDriver>();
        services.AddSingleton<WebDriverFactory>();
        services.AddSingleton<ISftpService>(serviceProvider => serviceProvider.GetRequiredService<SftpDriver>());
        services.AddSingleton<IWebDriverFactory>(serviceProvider => serviceProvider.GetRequiredService<WebDriverFactory>());
        services.AddSingleton<AsyncStatusWaiter>();
        services.AddSingleton<EvidenceCollector>();
        services.AddSingleton<ScenarioReportWriter>();
        services.AddSingleton<CmosScenarioOrchestrator>();

        services.AddSingleton<ApiContractValidator>();
        services.AddSingleton<CmosDbValidator>();
        services.AddSingleton<FileProcessingValidator>();
        services.AddSingleton<ScenarioOutcomeValidator>();

        services.AddSingleton<DoeiFileBuilder>();
        services.AddSingleton<PaymentInstructionRequestBuilder>();
        services.AddSingleton<ScenarioDataBuilder>();
        services.AddSingleton<PathHelper>();
        services.AddSingleton<JsonHelper>();
        services.AddSingleton<ReferenceDataHelper>();
        services.AddSingleton<RetryHelper>();
        services.AddSingleton<IFileTransferHelper, FileTransferHelper>();
        services.AddSingleton<IEvidencePathHelper, EvidencePathHelper>();
        services.AddSingleton<IDbSnapshotHelper, DbSnapshotHelper>();
        services.AddSingleton<IApiRequestHelper, ApiRequestHelper>();

        return services;
    }

    public static void RegisterScenarioServices(this IServiceProvider serviceProvider, Reqnroll.BoDi.IObjectContainer container, CmosTestContext context)
    {
        container.RegisterInstanceAs(context);
        container.RegisterInstanceAs(serviceProvider.GetRequiredService<EnvironmentSettings>());
        container.RegisterInstanceAs(serviceProvider.GetRequiredService<EvidenceCollector>());
        container.RegisterInstanceAs(serviceProvider.GetRequiredService<IEvidencePathHelper>());
        container.RegisterInstanceAs(serviceProvider.GetRequiredService<CmosScenarioOrchestrator>());
        container.RegisterInstanceAs(serviceProvider.GetRequiredService<DelimitedFileService>());
        container.RegisterInstanceAs(serviceProvider.GetRequiredService<FileProcessingValidator>());
        container.RegisterInstanceAs(serviceProvider.GetRequiredService<CmosDbValidator>());
        container.RegisterInstanceAs(serviceProvider.GetRequiredService<ScenarioOutcomeValidator>());
        container.RegisterInstanceAs(serviceProvider.GetRequiredService<DoeiFileBuilder>());
        container.RegisterInstanceAs(serviceProvider.GetRequiredService<DatabaseQueryService>());
        container.RegisterInstanceAs(serviceProvider.GetRequiredService<IDbSnapshotHelper>());
        container.RegisterInstanceAs(serviceProvider.GetRequiredService<IApiRequestHelper>());
    }
}
