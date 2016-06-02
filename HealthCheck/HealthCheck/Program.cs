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
        public static int refreshTime;
        public static int[] errorValueArray;
        public static int time = 5;
        public static Timer timer;

        static void Main(string[] args)
        {
            Console.WindowWidth = 120;
            Console.WindowHeight = 30;
            address = ConfigurationManager.AppSettings["address"];
            ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, sslPolicyErrors) => true;

            string path = ConfigurationManager.AppSettings["filePath"];

            Console.WriteLine(address);

            int.TryParse(ConfigurationManager.AppSettings["refreshTime"], out refreshTime);
            timer = new Timer(_ => OnCallBack(), null, 0, refreshTime);

            Console.ReadKey();


        }

        private static void OnCallBack()
        {
            var healthcheckprovider = HealthCheckProvider.Check(address);
            WriteHealthCheckData(healthcheckprovider);
            
        }

        public static void WriteHealthCheckData(HealthCheckDto healthcheckdto)
        {
            if (healthcheckdto == null)
            {
                errorValueArray = ConfigurationManager.AppSettings["errorTimeArray"].Split(',').Select(j => Convert.ToInt32(j)).ToArray();

                Console.WriteLine("Error, Internet connection is not available, " + "next attempt will be made at " + time + " seconds");

                if (time < 25)
                {
                    time += 5;
                }

                timer = new Timer(_ => OnCallBack(), null, 0, refreshTime);

                for (int i = 0; i < errorValueArray.Length; i += 1)
                {
                    timer.Change(Timeout.Infinite, Timeout.Infinite);
                    Thread.Sleep(errorValueArray[i]);
                    timer.Change(0, errorValueArray[i]);
                    timer.Change(Timeout.Infinite, Timeout.Infinite);
                }

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
