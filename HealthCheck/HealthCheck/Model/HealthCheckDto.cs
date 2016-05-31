using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;


namespace HealthCheck.Model
{
    class HealthCheckDto
    {
        public List<WorkerStatusDto> Workers { get; set; }
        public bool IsDbConnected { get; set; } = false;
        public string Version { get; set; }
        public string ServiceStatus { get; set; }
    }
}
