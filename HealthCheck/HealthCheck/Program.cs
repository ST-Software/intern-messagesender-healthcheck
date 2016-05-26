using System;
using System.Configuration;
using HealthCheck.Model;
using System.Net;
using System.IO;
using System.Timers;


namespace HealthCheck
{
    class Program
    {
        private static string adress;
        public static void Main()
        {
            ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, sslPolicyErrors) => true;
            adress = ConfigurationManager.AppSettings["adress"];
            Code();
            int interval;
            Timer t = new Timer();
            int.TryParse(ConfigurationManager.AppSettings["refresh"], out interval);
            t.Interval = interval;
            t.Start();
            t.Elapsed += OnTimeEvent;
            Console.ReadKey();
        }

        public static void OnTimeEvent(object sender, ElapsedEventArgs e) { Code(); }

        public static void Code()
        {
            string status;
            HealthCheckDto healthcheckdto = new HealthCheckDto();
            HealthCheckContent stat = new HealthCheckContent();
            healthcheckdto = stat.GetStatusAndContent(adress, healthcheckdto, out status);
            StringWriter Output = new StringWriter();
            if (healthcheckdto == null)
            {
                Console.WriteLine("Connection error");
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
            File.WriteAllText(ConfigurationManager.AppSettings["path"], Output.ToString());
            Console.WriteLine(Output);
        }
    }
}
