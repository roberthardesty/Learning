using DaemonExample.Configuration;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DaemonExample
{
    public class ExampleLongRunningService : IHostedService
    {
        private readonly DatabaseConfiguration _databaseConfiguration;
        private readonly LoggingConfiguration _loggingConfiguration;

        public ExampleLongRunningService(DatabaseConfiguration databaseConfiguration, LoggingConfiguration loggingConfiguration)
        {
            _databaseConfiguration = databaseConfiguration;
            _loggingConfiguration = loggingConfiguration;
        }
        public Task StartAsync(CancellationToken cancellationToken)
        {
            Console.WriteLine($"DB Server: {_databaseConfiguration.ServerAddress}");
            Console.WriteLine($"Logging Server: {_databaseConfiguration.ServerAddress}");
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
