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

        static void Main(string[] args)
        {
            Console.WindowWidth = 120;
            Console.WindowHeight = 30;
            address = ConfigurationManager.AppSettings["address"];
            ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, sslPolicyErrors) => true;

            string path = ConfigurationManager.AppSettings["filePath"];

            Console.WriteLine(address);

            
            int.TryParse(ConfigurationManager.AppSettings["refreshTime"], out refreshTime);
            Timer timer = new Timer(_ => OnCallBack(), null, 0, refreshTime);

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
                int[] errorValueArray = ConfigurationManager.AppSettings["errorTimeArray"].Split(',').Select(n => Convert.ToInt32(n)).ToArray();
                for (int i = 0; i < errorValueArray.Length; i += 1)
                {
                    refreshTime = errorValueArray[i];
                    Console.WriteLine("Error, " + "Next attempt is avalaible in " + refreshTime);
                    Console.WriteLine();

                    //int counter = 1;
                    //switch (counter)
                    //{
                    //    case 1:
                    //        Console.WriteLine(refreshTime);
                    //        counter += 1;
                    //        break;
                    //    case 2:
                    //        Console.WriteLine(refreshTime);
                    //        counter += 1;
                    //        break;
                    //    case 3:
                    //        Console.WriteLine(refreshTime);
                    //        counter += 1;
                    //        break;
                    //    case 4:
                    //        Console.WriteLine(refreshTime);
                    //        counter += 1;
                    //        break;
                    //    case 5:
                    //        Console.WriteLine(refreshTime);
                    //        counter += 1;
                    //        break;
                    //    default:
                    //        Console.WriteLine(25000);
                    //        break;
                    //}

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
