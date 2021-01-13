using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace DaemonExample.ServiceRunner
{
    public static class ServiceRunnerExtensions
    {
        public static ServiceRunner WithFactory(
            this ServiceRunner serviceRunner,
            Func<ServiceBase> factory)
        {
            serviceRunner.Factory = factory;
            return serviceRunner;
        }

        public static ServiceRunner WithLogger(
            this ServiceRunner serviceRunner,
            ILogger logger)
        {
            serviceRunner.Logger = logger;
            return serviceRunner;
        }
    }
}
