using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCheck.Model
{
    class HealthCheckDto
    {
        public bool IsDbConnected { get; set; }
        public string ServiceStatus { get; set; }
        public string Version { get; set; }
        public List<WorkerStatusDto> Workers { get; set; }
    }
}
