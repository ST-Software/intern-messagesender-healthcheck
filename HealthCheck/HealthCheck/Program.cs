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
        public static void Main()
        {
            Program prog = new Program();
            prog.Code();
            int interval;
            Timer t = new Timer();
            int.TryParse(ConfigurationManager.AppSettings["refresh"], out interval);
            t.Interval = interval;
            t.Start();
            t.Elapsed += prog.OnTimeEvent;
            Console.ReadKey();
        }

        public void OnTimeEvent(object sender, ElapsedEventArgs e)
        {
            Code();
        }

        public void Code()
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
            if (healthcheckdto == null)
            {
                Console.WriteLine("Error");
            }
            
            File.WriteAllText(ConfigurationManager.AppSettings["path"], Output.ToString());

            Console.WriteLine(Output);
            

        }
    }
}
