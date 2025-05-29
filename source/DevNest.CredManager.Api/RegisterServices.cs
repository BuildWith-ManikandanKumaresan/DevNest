#region using directives
using Microsoft.Extensions.Options;
using System.Reflection;
using MediatR;
using DevNest.Business.Domain.Mappers;
using DevNest.Common.Base.Contracts;
using DevNest.Common.Logger;
using DevNest.Common.Logger.Model;
using DevNest.Business.Domain.RouterContracts;
using DevNest.Infrastructure.Routers;
#endregion using directives

namespace DevNest.CredManager.Api
{
    /// <summary>
    /// Represents the service regiser in DI containers.
    /// </summary>
    public static class RegisterServices
    {
        private const string _ConfigurationsDirectory = "configurations";
        private const string _LoggerConfigurations = "logger.configuration.json";
        private const string _ApiServiceName = "Credential-Manager";
        private const string _AssemblySearchPattern = "DevNest.";

        /// <summary>
        /// Register the logging depdency injections for the services.
        /// </summary>
        /// <param name="builder"></param>
        public static void RegisterLogger(this IServiceCollection services)
        {
            // Setup AppConfigService for LoggerConfig
            var provider = services.BuildServiceProvider();
            var loggerConfigOptions = provider.GetRequiredService<IOptionsMonitor<LoggerConfig>>();
            var appConfigService = new AppConfigService<LoggerConfig>(loggerConfigOptions);

            // Initialize logger and register in DI
            var loggingManager = new LoggingManager(appConfigService);
            Serilog.ILogger logger = loggingManager.Initialize(_ApiServiceName);

            services.AddSingleton<IAppConfigService<LoggerConfig>>(appConfigService);
            services.AddSingleton(logger);
            services.AddScoped(typeof(IAppLogger<>), typeof(AppLogger<>));
        }

        /// <summary>
        /// Registers the custom configuration files to the services.
        /// </summary>
        /// <param name="builder"></param>
        public static void RegisterConfigurations(this WebApplicationBuilder builder)
        {
            string jsonFilePath = Path.Combine(
                Directory.GetCurrentDirectory(),
                "..",
                _ConfigurationsDirectory,
                _LoggerConfigurations);

            builder.Configuration.AddJsonFile(jsonFilePath, optional: false, reloadOnChange: true);
            builder.Services.Configure<LoggerConfig>(builder.Configuration);
        }

        /// <summary>
        /// Register the mediatr dependency injections in the DI container.
        /// </summary>
        /// <param name="builder"></param>
        public static void RegisterMediatr(this IServiceCollection services)
        {
            var currentAssembly = GetCurrentAssembly();
            var referencedAssemblies = GetReferencedAssemblies(currentAssembly).ToList();
            referencedAssemblies.Add(currentAssembly);

            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssemblies(referencedAssemblies.ToArray());
            });
            services.AddScoped<IMediator, Mediator>();
            services.AddAutoMapper(typeof(MappingProfile));

            services.Scan(scan => scan
                .FromAssemblies(referencedAssemblies)

                .AddClasses(classes => classes.AssignableTo(typeof(IDomainService)))
                .AsImplementedInterfaces()
                .WithScopedLifetime()

                .AddClasses(classes => classes.AssignableTo(typeof(IReposRouter)))
                .AsImplementedInterfaces()
                .WithScopedLifetime()

                .AddClasses(classes => classes.InNamespaceOf<CredManagerReposRouter>())
                .AsImplementedInterfaces()
                .WithScopedLifetime());
        }

        /// <summary>
        /// Handler method to get the current executing assmbly.
        /// </summary>
        /// <returns></returns>
        public static Assembly? GetCurrentAssembly()
        {
            return Assembly.GetAssembly(typeof(RegisterServices));
        }

        /// <summary>
        /// Gets the referenced assemblies that match the specified search pattern.
        /// </summary>
        /// <param name="assembly">The assembly to get referenced assemblies from.</param>
        /// <returns>An enumerable collection of referenced assemblies.</returns>
        public static IEnumerable<Assembly> GetReferencedAssemblies(Assembly assembly)
        {
            var assembliesName = assembly.GetReferencedAssemblies().Where(referenced => IsAssemblyAccepted(referenced.Name));
            return assembliesName.Select(Assembly.Load);
        }

        /// <summary>
        /// Determines whether an assembly name is accepted based on a search pattern.
        /// </summary>
        /// <param name="name">The name of the assembly to check.</param>
        /// <returns><c>true</c> if the assembly name matches the search pattern; otherwise, <c>false</c>.</returns>
        private static bool IsAssemblyAccepted(string? name)
        {
            return name?.StartsWith(_AssemblySearchPattern) ?? false;
        }
    }
}