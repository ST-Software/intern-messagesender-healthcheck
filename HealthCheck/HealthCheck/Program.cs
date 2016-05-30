using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using System.Timers;
using System.Configuration;


namespace HealthCheck
{
    class Program
    {
       

        static void Main(string[] args)
        {
            ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, sslPolicyErrors) => true;
            string adress = ConfigurationManager.AppSettings["adress"];
            string path = ConfigurationManager.AppSettings["filepPath"];
            string text = "";
            try
            {
                HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(adress);
                httpWebRequest.AllowAutoRedirect = false;
                Stream stream = httpWebRequest.GetRequestStream();
                stream.Close();

                WebResponse response = httpWebRequest.GetResponse();
                stream = response.GetResponseStream();

                StreamReader streamreader = new StreamReader(stream);
                text = streamreader.ReadToEnd();

                streamreader.Close();
                stream.Close();
            }
            catch 
            {
                Console.WriteLine("Error");
            }
            Console.WriteLine(adress);
            Console.ReadKey();
        }
    }
}
