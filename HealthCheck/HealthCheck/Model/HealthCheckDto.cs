using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCheck.Model
{
    class HealthCheckDto
    {
        public bool IsDbConnected { get; set; } = false;
        public string Version { get; set; }
        public int HttpResponseStatus { get; set; }
        public string HttpResponseStatusText { get; set; }
        public List<WorkerStatusDto> Workers { get; set; }
    }
}
