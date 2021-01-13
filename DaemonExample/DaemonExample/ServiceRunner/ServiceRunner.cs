using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace DaemonExample.ServiceRunner
{
    public class ServiceRunner
    {
        public Func<ServiceBase> Factory { get; set; }

        public ILogger Logger { get; set; }

        public int Run()
        {
            if (this.Factory == null)
                throw new ArgumentNullException("Factory");
            
                return this.RunLinuxDaemon();
        }

        private int RunLinuxDaemon()
        {
            if (this.Logger != null)
                AppDomain.CurrentDomain.UnhandledException += (UnhandledExceptionEventHandler)((sender, args) =>
                {
                    this.Logger.LogError("Fatal error in hosted service");
                    if (args.ExceptionObject is Exception exceptionObject)
                        this.Logger.LogError(exceptionObject.ToString());
                    else
                        this.Logger.LogError(args.ExceptionObject.ToString());
                });
            try
            {
                this.RunDaemonAsync().GetAwaiter().GetResult();
                return 0;
            }
            catch (Exception ex)
            {
                this.Logger?.LogError("Fatal error in hosted service");
                this.Logger?.LogError(ex.ToString());
            }
            return -1;
        }

        private async Task RunDaemonAsync()
        {
            CancellationTokenSource endProcessCts = new CancellationTokenSource();
            AppDomain.CurrentDomain.ProcessExit += (EventHandler)((sender, args) =>
            {
                this.Logger?.LogDebug("Received SIGTERM");
                endProcessCts.Cancel();
            });
            Console.CancelKeyPress += (ConsoleCancelEventHandler)((sender, args) =>
            {
                this.Logger?.LogDebug("Received Ctrl+C");
                endProcessCts.Cancel();
                args.Cancel = true;
            });
            ServiceBase service = this.Factory();
            try
            {
                await service.StartAsync(endProcessCts.Token);
                await Task.Delay(-1, endProcessCts.Token);
            }
            catch (TaskCanceledException)
            {
            }
            await service.StopAsync(CancellationToken.None);
        }
    }

}
