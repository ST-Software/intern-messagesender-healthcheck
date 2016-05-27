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
        private static string path;
        public static void Main()
        {
            ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, sslPolicyErrors) => true;
            adress = ConfigurationManager.AppSettings["adress"];
            path = ConfigurationManager.AppSettings["path"];
            HealthCheckBody();
            int interval;
            Timer t = new Timer();
            int.TryParse(ConfigurationManager.AppSettings["refresh"], out interval);
            t.Interval = interval;
            t.Start();
            t.Elapsed += OnTimeEvent;
            Console.ReadKey();
        }

        public static void OnTimeEvent(object sender, ElapsedEventArgs e) { HealthCheckBody(); }

        public static void HealthCheckBody()
        {
            var healthcheckdto = HealthCheckService.GetHealthCheck(adress);
            
            StringWriter Output = new StringWriter();
            if (healthcheckdto == null)
            {
                Output.WriteLine("Connection to server failed.");
            }
            else
            {
                Output.WriteLine("Status - " + healthcheckdto.HttpResponseStatusText);
                Output.WriteLine("IsDbConnected - " + healthcheckdto.IsDbConnected);
                Output.WriteLine("Version - " + healthcheckdto.Version);
                if (healthcheckdto.Workers != null)
                {
                    Output.WriteLine("Workers count - " + healthcheckdto.Workers.Count);
                    foreach (var item in healthcheckdto.Workers)
                    {
                        Output.WriteLine(" - Worker " + item.Name + " is " + item.StatusText);
                    }
                }
            }
            File.WriteAllText(path, Output.ToString());
            Console.WriteLine(Output);
        }
    }
}
