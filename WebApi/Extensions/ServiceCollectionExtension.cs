using Domain.Core;
using Domain.Services;
using HeadHunter.Integration.Configuration;
using HeadHunter.Integration.Helpers;
using HeadHunter.Integration.Services;
using Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using WebApi.BackgroundServices;
using WebApi.Filters;

namespace WebApi.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static void AddTransactionPerRequestFilter(this IServiceCollection services)
        {
            services.AddScoped(typeof(TransactionPerRequestActionFilterAttribute),
                typeof(TransactionPerRequestActionFilterAttribute));
            services.AddMvc(setup => setup.Filters.AddService<TransactionPerRequestActionFilterAttribute>(1));
        }

        public static void AddServices(this IServiceCollection services)
        {
            services.AddScoped<ICurrencyService, CurrencyService>();
            services.AddScoped<IProfessionalAreaService, ProfessionalAreaService>();
            services.AddScoped<IVacancyService, VacancyService>();
            services.AddScoped<IDepartmentService, DepartmentService>();
            services.AddScoped<IEmployerService, EmployerService>();

            services.AddScoped<ISynchronizableService<Currency>>(sp => sp.GetRequiredService<ICurrencyService>());
            services.AddScoped<ISynchronizableService<ProfessionalArea>>(sp => sp.GetRequiredService<IProfessionalAreaService>());

            services.AddScoped<IBaseService<ProfessionalArea>>(sp => sp.GetRequiredService<IProfessionalAreaService>());
            services.AddScoped<IBaseService<Currency>>(sp => sp.GetRequiredService<ICurrencyService>());
            services.AddScoped<IBaseService<Vacancy>>(sp => sp.GetRequiredService<IVacancyService>());
            services.AddScoped<IBaseService<Department>>(sp => sp.GetRequiredService<IDepartmentService>());
            services.AddScoped<IBaseService<Employer>>(sp => sp.GetRequiredService<IEmployerService>());

            services.AddScoped<UnitOfWork>();
        }

        public static void AddSettings(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddOptions();
            services.Configure<CurrencySynchronizerSettings>(configuration.GetSection(nameof(CurrencySynchronizerSettings)));
            services.Configure<ProfessionalAreaSynchronizerSettings>(configuration.GetSection(nameof(ProfessionalAreaSynchronizerSettings)));
            services.Configure<VacancyServiceSettings>(configuration.GetSection(nameof(VacancyServiceSettings)));
        }
        public static void AddLogging(this IServiceCollection services)
        {
            services.AddSingleton<ILoggerFactory, LoggerFactory>();
            services.AddSingleton(typeof(ILogger<>), typeof(Logger<>));
        }

        public static void AddBackgroundWorkers(this IServiceCollection services)
        {
            services.AddHostedService<DictionarySynchronizer>();
        }

        public static void AddJsonDeserializers(this IServiceCollection services)
        {
            services.AddSingleton(typeof(JsonDataPageDeserializer<,>));
            services.AddSingleton(typeof(JsonDeserializer<,>));
        }
    }
}
