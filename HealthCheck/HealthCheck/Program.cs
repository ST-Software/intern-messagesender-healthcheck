using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using System.Configuration;
using HealthCheck.Model;
using System.Threading;


namespace HealthCheck
{
    class Program
    {

        public static string address;

        static void Main(string[] args)
        {
            Console.WindowWidth = 120;
            Console.WindowHeight = 30;
            address = ConfigurationManager.AppSettings["address"];
            ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, sslPolicyErrors) => true;

            string path = ConfigurationManager.AppSettings["filePath"];

            Console.WriteLine(address);

            int refreshtime;
            int.TryParse(ConfigurationManager.AppSettings["refreshTime"], out refreshtime);
            Timer timer = new Timer(_ => OnCallBack(), null, 0, refreshtime);

            Console.ReadKey();


        }

        private static void OnCallBack()
        {
            var healthcheckprovider = HealthCheckProvider.Check(address);
            WriteHealthCheckData(healthcheckprovider);
        }



        public static void WriteHealthCheckData(HealthCheckDto healthcheckdto)
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
                Console.WriteLine();
            }

        }
    }
}
