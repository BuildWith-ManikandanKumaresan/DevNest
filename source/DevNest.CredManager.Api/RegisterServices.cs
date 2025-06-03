#region using directives
using Microsoft.Extensions.Options;
using System.Reflection;
using MediatR;
using DevNest.Business.Domain.Mappers;
using DevNest.Common.Base.Contracts;
using DevNest.Common.Logger;
using DevNest.Business.Domain.RouterContracts;
using DevNest.Infrastructure.Routers;
using DevNest.Common.Base.Entity;
using DevNest.Common.Base.Helpers;
using DevNest.Manager.Plugin;
using DevNest.Manager.FileSystem;
using DevNest.Infrastructure.Entity.Configurations.CredentialManager;
using DevNest.Common.Base.Constants;
#endregion using directives

namespace DevNest.CredManager.Api
{
    /// <summary>
    /// Represents the service regiser in DI containers.
    /// </summary>
    public static class RegisterServices
    {
        /// <summary>
        /// Register the logging depdency injections for the services.
        /// </summary>
        /// <param name="builder"></param>
        public static void RegisterLogger(this WebApplicationBuilder builder)
        {
            builder.Services.Configure<LoggerConfigEntity>(
                builder.Configuration.GetSection("applicationLoggers"));
            
            // Setup AppConfigService for LoggerConfig
            using var provider = builder.Services.BuildServiceProvider();
            var config = provider.GetRequiredService<IOptions<LoggerConfigEntity>>();
            
            // Initialize logger and register in DI
            var loggingManager = new LoggingManager(config);
            Serilog.ILogger logger = loggingManager.Initialize(ServiceConstants.ServiceName_CredentialManager);
            builder.Services.AddSingleton<IOptions<LoggerConfigEntity>>(config);
            builder.Services.AddSingleton(logger);
            builder.Services.AddSingleton(typeof(IApplicationLogger<>), typeof(ApplicationLogger<>));
            builder.Services.AddSingleton(typeof(IApplicationConfigService<>), typeof(ApplicationConfigService<>));
        }

        /// <summary>
        /// Registers the custom configuration files to the services.
        /// </summary>
        /// <param name="configuration"></param>
        public static void RegisterConfigurations(this WebApplicationBuilder builder)
        {
            FileSystemManager manager = new FileSystemManager();
            string configDirectory = manager.ConfigurationDirectory;

            builder.Configuration.AddJsonFile(Path.Combine(configDirectory, ConfigurationFileConstants.ConfigurationFileName_CredentialManager), optional: true, reloadOnChange: true)
                         .AddEnvironmentVariables();
            builder.Services.Configure<CredentialManagerConfigurations>(
                builder.Configuration);


        }

        /// <summary>
        /// Register the mediatr dependency injections in the DI container.
        /// </summary>
        /// <param name="builder"></param>
        public static void RegisterMediatr(this IServiceCollection services)
        {
            var currentAssembly = Assembly.GetAssembly(typeof(RegisterServices));
            var referencedAssemblies = AssemblyHelper.GetReferencedAssemblies(currentAssembly).ToList();
            referencedAssemblies.Add(currentAssembly);

            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssemblies(referencedAssemblies.ToArray());
            });

            services.AddScoped<IMediator, Mediator>();
            services.AddAutoMapper(typeof(MappingProfile));
            // Register the FileSystemManager in DI container
            services.AddScoped<IFileSystemManager, FileSystemManager>();
            services.AddScoped<IPluginManager,PluginManager>();

            services.Scan(scan => scan
                .FromAssemblies(referencedAssemblies)

                .AddClasses(classes => classes.AssignableTo(typeof(IReposRouter)))
                .AsImplementedInterfaces()
                .WithScopedLifetime()

                .AddClasses(classes => classes.AssignableTo(typeof(IDomainService)))
                .AsImplementedInterfaces()
                .WithScopedLifetime()

                .AddClasses(classes => classes.InNamespaceOf<CredentialManagerReposRouter>())
                .AsImplementedInterfaces()
                .WithScopedLifetime());
        }

    }
}