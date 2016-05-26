using System;
using System.Configuration;
using HealthCheck.Model;
using System.Net;
using System.IO;


namespace HealthCheck
{
    class Program
    {
        
        public static void Main()
        {
            string status;
            ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, sslPolicyErrors) => true;
            string adress = ConfigurationManager.AppSettings["adress"];
            HealthCheckDto healthcheckdto = new HealthCheckDto();
            HealthCheckContent stat = new HealthCheckContent();
            healthcheckdto = stat.GetStatusAndContent(adress, healthcheckdto, out status);
            StringWriter Output = new StringWriter();
            if (healthcheckdto == null)
            {
                Console.WriteLine("Error");
                Console.ReadKey();
                return;
            }
            Output.WriteLine("Status - " + status);
            Output.WriteLine("IsDbConnected - " + healthcheckdto.IsDbConnected);
            Output.WriteLine("Version - " + healthcheckdto.Version);
            Output.WriteLine("Workers count - " + healthcheckdto.Workers.Count);
            foreach (var item in healthcheckdto.Workers)
            {
                Output.WriteLine(" - Worker " + item.Name + " is " + item.StatusText);
            }
            if (healthcheckdto == null)
            {
                Console.WriteLine("Error");
            }
            
            File.WriteAllText(ConfigurationManager.AppSettings["path"], Output.ToString());

            Console.WriteLine(Output);
            Console.ReadKey();

        }
    }
}
