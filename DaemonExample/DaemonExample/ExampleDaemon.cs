using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using BusinessObjectExamples;
using DaemonExample.Configuration;
using DaemonExample.ServiceRunner;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace DaemonExample
{
    public class ExampleDaemon : ServiceBase
    {

        private IHost _host;
        private readonly ILoggerProvider _loggerProvider;

        public ExampleDaemon(ILoggerProvider loggerProvider)
        {
            _loggerProvider = loggerProvider;
        }

        public override async Task StartAsync(CancellationToken cancellationToken)
        {
            _host = await Host.CreateDefaultBuilder()
                .ConfigureLogging(SetUpLogger)
                .UseServiceProviderFactory(new AutofacServiceProviderFactory(SetUpDI))
                .StartAsync(cancellationToken);
        }

        private void SetUpLogger(ILoggingBuilder loggingBuilder)
        {
            loggingBuilder.AddProvider(_loggerProvider);
        }

        private void SetUpDI(ContainerBuilder containerBuilder)
        {
            var coreAssembly = Assembly.GetAssembly(typeof(BusinessLogicItem));
            // this grabs all types that implement the interface IRequireSingleton
            var singletonTypes = coreAssembly.GetTypes()
                .Where(t => t.IsAssignableTo<IRequireSingleton>())
                .ToArray();
            // this grabs everything else
            var nonSingletonTypes = coreAssembly.GetTypes()
                .Except(singletonTypes)
                .ToArray();
            // register the IRequireSingleton's
            containerBuilder.RegisterTypes(singletonTypes)
                   .SingleInstance();
            // register the others
            containerBuilder.RegisterTypes(nonSingletonTypes);

            // registering all types in an assembly as their implemented interfaces
            var serviceAssembly = Assembly.GetExecutingAssembly();
            containerBuilder.RegisterAssemblyTypes(serviceAssembly)
                   .AsImplementedInterfaces();

            // Set up configuration to read from appropriate file
            IConfiguration configuration = new ConfigurationBuilder()
                .AddJsonFile(GetConfigurationFileForEnvironment(), optional: true, reloadOnChange: true)
                .Build();
            // create instances of strongly typed configuration for DI
            var loggingConfiguration = new LoggingConfiguration();
            var databaseConfiguration = new DatabaseConfiguration();
            // bind config file with instance
            configuration.Bind("Database", databaseConfiguration);
            configuration.Bind("Logging", loggingConfiguration);
            
            containerBuilder.RegisterInstance(loggingConfiguration);
            containerBuilder.RegisterInstance(databaseConfiguration);

            var serviceCollection = new ServiceCollection();
            serviceCollection.AddHostedService<ExampleLongRunningService>();

            containerBuilder.Populate(serviceCollection);
        }

        private string GetConfigurationFileForEnvironment()
        {
            var environmentName = "Default";
#if DEBUG
            environmentName = "Development";
#elif STAGING
    environmentName = "Staging";
#elif RELEASE
    environmentName = "Production";
#endif
            return $"appsettings.{environmentName}.json";
        }

        public override Task StopAsync(CancellationToken cancellationToken)
        {
            return _host.StopAsync(cancellationToken);
        }
    }
}
