using CMOS_Automation_Framework.src.API.CMOS.Auth;
using CMOS_Automation_Framework.src.API.CMOS.Clients;
using CMOS_Automation_Framework.src.Services.Api;
using CMOS_Automation_Framework.src.Services.Db;
using CMOS_Automation_Framework.src.Services.Evidence;
using CMOS_Automation_Framework.src.Services.File;
using CMOS_Automation_Framework.src.Services.Orchestration;
using CMOS_Automation_Framework.src.Services.Reporting;
using CMOS_Automation_Framework.src.Services.Waiters;
using CMOS_Automation_Framework.src.Validators.ApiValidators;
using CMOS_Automation_Framework.src.Validators.DbValidators;
using CMOS_Automation_Framework.src.Validators.EndToEndValidators;
using CMOS_Automation_Framework.src.Validators.FileValidators;
using CMOS_Automation_Framework.src.Builders.FileBuilders;
using CMOS_Automation_Framework.src.Builders.RequestBuilders;
using CMOS_Automation_Framework.src.Builders.TestDataBuilders;

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
        services.AddSingleton<CmosApiService>();
        services.AddSingleton<IrisConnectionFactory>();
        services.AddSingleton<DatabaseQueryService>();
        services.AddSingleton<DelimitedFileService>();
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

        return services;
    }
}
