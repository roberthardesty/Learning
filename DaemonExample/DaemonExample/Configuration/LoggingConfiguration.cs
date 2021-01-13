using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace DaemonExample.Configuration
{
    public class LoggingConfiguration
    {
        public string ServerAddress { get; set; }
        public LogLevel LogLevel { get; set; }
    }
}
