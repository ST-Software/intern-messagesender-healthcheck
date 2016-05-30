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
            ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, sslPolicyErrors) => true;
            string adress = ConfigurationManager.AppSettings["adress"];
            string path = ConfigurationManager.AppSettings["filePath"];
            Console.WriteLine(adress);
            Console.ReadKey();
        }
    }
}
