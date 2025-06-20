#region using directives
using Microsoft.Extensions.Options;
using System.Reflection;
using MediatR;
using DevNest.Business.Domain.Mappers;
using DevNest.Common.Base.Contracts;
using DevNest.Common.Logger;
using DevNest.Business.Domain.RouterContracts;
using DevNest.Common.Base.Entity;
using DevNest.Common.Base.Helpers;
using DevNest.Infrastructure.Entity.Configurations.CredentialManager;
using DevNest.Common.Base.Constants;
using DevNest.Common.Base.Constants.Message;
using DevNest.Common.Base.MediatR;
using DevNest.Common.Base.Configuration;
using DevNest.Common.Base.MediatR.Contracts;
using DevNest.Application.Queries.Credentials;
using DevNest.Application.Commands.Credentials;
using DevNest.Application.CommandHandlers.Credentials;
using DevNest.Application.QueryHandlers.Credentials;
using System.Windows.Input;
using DevNest.Infrastructure.Routers.Credentials;
using DevNest.Common.Manager.FileSystem;
using DevNest.Common.Manager.Plugin;
using DevNest.Common.Manager.Tag;
#endregion using directives

namespace DevNest.CredentialsManager.Api
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
            builder.Services.Configure<LoggerConfigEntityModel>(
                builder.Configuration.GetSection("applicationLoggers"));

            // Setup AppConfigService for LoggerConfig
            using var provider = builder.Services.BuildServiceProvider();
            var config = provider.GetRequiredService<IOptions<LoggerConfigEntityModel>>();

            // Initialize logger and register in DI
            var loggingManager = new LoggingManager(config);
            Serilog.ILogger logger = loggingManager.Initialize(ServiceConstants.ServiceName_CredentialManager);
            builder.Services.AddSingleton<IOptions<LoggerConfigEntityModel>>(config);
            builder.Services.AddSingleton(logger);
            builder.Services.AddSingleton(typeof(IAppLogger<>), typeof(AppLogger<>));
            builder.Services.AddSingleton(typeof(IAppConfigService<>), typeof(AppConfigService<>));
        }

        /// <summary>
        /// Registers the custom configuration files to the services.
        /// </summary>
        /// <param name="configuration"></param>
        public static void RegisterConfigurations(this WebApplicationBuilder builder)
        {
            FileSystemManager manager = new();
            List<string> configDirectory = manager?.Preferences?.GetFilesWithSearchPattern()?.ToList() ?? [];
            configDirectory.ForEach(item =>
            {
                builder.Configuration.AddJsonFile(item, optional: true, reloadOnChange: true).AddEnvironmentVariables();
            });
            builder.Services.Configure<CredentialManagerConfigurations>(builder.Configuration);


        }

        /// <summary>
        /// Register the mediatr dependency injections in the DI container.
        /// </summary>
        /// <param name="builder"></param>
        public static void RegisterMediatr(this IServiceCollection services)
        {
            var currentAssembly = Assembly.GetAssembly(typeof(Program));
            var referencedAssemblies = AssemblyHelper.GetReferencedAssemblies(currentAssembly).ToList();
            if(currentAssembly != null)
                referencedAssemblies.Add(currentAssembly);

            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssemblies(referencedAssemblies.ToArray());
                cfg.RegisterServicesFromAssemblies(typeof(GetCredentialsQueryHandler).Assembly);
                cfg.RegisterServicesFromAssemblies(typeof(AddCredentialCommandHandler).Assembly);
            });

            services.AddScoped<IMediator, Mediator>();
            services.AddScoped<IAppMedidator, AppMedidator>();
            services.AddAutoMapper(typeof(MappingProfile));

            // Register the managers in DI container
            services.AddScoped<IFileSystemManager, FileSystemManager>();
            services.AddScoped<IPluginManager, PluginManager>();
            services.AddScoped<ITagManager, TagManager>();

            services.Scan(scan => scan
                .FromAssemblies(referencedAssemblies)

                .AddClasses(classes => classes.AssignableTo(typeof(IRepositoryRouter)))
                .AsImplementedInterfaces()
                .WithScopedLifetime()

                .AddClasses(classes => classes.AssignableTo(typeof(IDomainService)))
                .AsImplementedInterfaces()
                .WithScopedLifetime()

                .AddClasses(classes => classes.InNamespaceOf<CredentialRepositoryRouter>())
                .AsImplementedInterfaces()
                .WithScopedLifetime());
        }

        /// <summary>
        /// Registers the infrastructure services for the application.
        /// </summary>
        public static void RegisterInfrastructure()
        {
            FileSystemManager manager = new();
            IList<string>? errorCodesFiles = manager.Resources?.GetSubDirectory(FileSystemConstants.ErrorCodesDirectoy)?.GetFilesWithSearchPattern() ?? [];
            IList<string>? warningCodesFiles = manager.Resources?.GetSubDirectory(FileSystemConstants.WarningCodesDirectory)?.GetFilesWithSearchPattern() ?? [];
            IList<string>? successCodesFiles = manager.Resources?.GetSubDirectory(FileSystemConstants.SuccessCodesDirectory)?.GetFilesWithSearchPattern() ?? [];
            Messages.InitErrorCodes(errorCodesFiles);
            Messages.InitWarningCodes(warningCodesFiles);
            Messages.InitSuccessCodes(successCodesFiles);
        }
    }
}