using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            string adress = "https://10.0.1.221:9000/";
            string status;
            Status stat = new Status();
            if (stat.GetStatus(adress, out status) == false)
            {
                Console.WriteLine("Connection error.");
                Console.ReadKey();
                return;
            }
            StringWriter output = new StringWriter();
            output.WriteLine("Connection status - " + status);
            Conclusion con = new Conclusion();
            output.WriteLine(con.GetText(adress));


            Console.WriteLine(output);
            Console.ReadKey();

        }
    }
}
