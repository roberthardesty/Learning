using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;

namespace DaemonExample.ServiceRunner
{
    public abstract class ServiceBase : IHostedService
    {
        public abstract Task StartAsync(CancellationToken cancellationToken);

        public abstract Task StopAsync(CancellationToken cancellationToken);
    }
}
