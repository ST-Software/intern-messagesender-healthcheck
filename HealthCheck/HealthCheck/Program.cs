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
            ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, sslPolicyErrors) => true;
            string adress = ConfigurationManager.AppSettings["adress"];
            
            Status stat = new Status();
            if (stat.GetStatus(adress) == false)
            {
                Console.WriteLine("Connection error.");
                Console.ReadKey();
                return;
            }
            StringWriter output = new StringWriter();
            output.WriteLine("Connection status - " + stat.Output);
            //Conclusion con = new Conclusion();
            //output.WriteLine(con.GetText(adress));
            File.WriteAllText(ConfigurationManager.AppSettings["path"], output.ToString());

            Console.WriteLine(output);
            Console.ReadKey();

        }
    }
}
