using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using System.Configuration;

namespace HealthCheck.Model
{
    class HealthCheckBody
    {
        public static void Check(string text)
        {
            string adress = ConfigurationManager.AppSettings["adress"];
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
        }
    }
}
