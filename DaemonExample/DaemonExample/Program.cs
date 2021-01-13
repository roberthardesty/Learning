using DaemonExample.ServiceRunner;
using Microsoft.Extensions.Logging;
using System;

namespace DaemonExample
{
    class Program
    {
        public static int Main(string[] args)
        {
            ILoggerProvider loggerProvider = null; // Set up serilog logger

            var serviceRunner = new ServiceRunner.ServiceRunner()
                .WithFactory(() => new ExampleDaemon(loggerProvider))
                .WithLogger(loggerProvider?.CreateLogger("Example") ?? null); 

            return serviceRunner.Run();
        }
    }
}
