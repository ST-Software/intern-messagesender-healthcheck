using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Net;
using System.IO;
using Newtonsoft.Json.Linq;


namespace HealthCheck.Model
{
    class Body
    {
        List<Worker> Workers = new List<Worker>();
        public StringWriter Output = new StringWriter();

        public Body()
        {
            Output.Write("Status - ");
            //get certificate
            ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, sslPolicyErrors) => true;
            //ask for http status
            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create("https://10.0.1.221:9000/");
            webRequest.AllowAutoRedirect = false;
            HttpWebResponse response = (HttpWebResponse)webRequest.GetResponse();
            Output.Write(response.StatusCode.ToString());
            if(Output. != "Status - OK") { Console.WriteLine(Output); return; }
            //download file (as string)
            WebClient webClient = new WebClient();
            string text = webClient.DownloadString("https://10.0.1.221:9000/");
            //Prepare for output
            JObject stuff = JObject.Parse(text);
            JArray workers = (JArray)stuff["Workers"];

            Console.WriteLine(workers[0]["Name"]);

            Output += Environment.NewLine + "Is Db Connected - " + stuff["IsDbConnected"] + Environment.NewLine +
                      "Workers count - " + workers.Count + Environment.NewLine;
                      
            Console.WriteLine(Output);
            Console.ReadKey();

        }

        public void WriteIt(List<Worker> workers)
        {
            foreach (Worker wor in workers)
            {
                Output += wor + Environment.NewLine;
            }
            Console.WriteLine(Output);
            File.WriteAllText(@"C:\Users\Vítek\ST_SW\intern-messagesender-healthcheck\logs\Data.txt", Output);
            Console.ReadKey();
        }

        
    }
}
