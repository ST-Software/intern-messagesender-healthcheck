using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using System.Timers;
using System.Configuration;
using HealthCheck.Model;


namespace HealthCheck
{
    class Program
    {
      
        static void Main(string[] args)
        {
            Console.WindowWidth = 120;
            Console.WindowHeight = 30;
            ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, sslPolicyErrors) => true;
            string adress = ConfigurationManager.AppSettings["adress"];
            string path = ConfigurationManager.AppSettings["filePath"];

            Console.WriteLine(adress);
            
            var healthcheckbody = HealthCheckBody.Check(adress);
            WriteHealthCheckData(healthcheckbody);
            Console.ReadKey();

        }

        static void WriteHealthCheckData(HealthCheckDto healthcheckdto)
        {
            
            if(healthcheckdto == null)
            {
                Console.WriteLine("Error");
            }
            else
            {
                Console.WriteLine("IsDbConnected" + " – " + healthcheckdto.IsDbConnected);
                Console.WriteLine("Version" + " – " + healthcheckdto.Version);
                Console.WriteLine("Service status" + " – " + healthcheckdto.ServiceStatus);

                foreach (var item in healthcheckdto.Workers)
                {
                    Console.WriteLine("– " + item.Name + " Status: " + item.StatusText);
                }
            }

        }
    }
}
