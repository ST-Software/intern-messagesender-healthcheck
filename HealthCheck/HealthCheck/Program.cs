using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HealthCheck.Model;
using System.Net;


namespace HealthCheck
{
    class Program
    {
       public static void Main()
        {
            ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, sslPolicyErrors) => true;
            Body b = new Body();
        } 
    }
}
