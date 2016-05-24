using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCheck.Model
{
    class Worker
    {
        public string Name { get; private set; }
        public string Status { get; private set; }

        public Worker(string name, string status)
        {
            Name = name;
            Status = status;
        }

        public override string ToString()
        {
            return Name + " - " + Status;
        }
    }
}
