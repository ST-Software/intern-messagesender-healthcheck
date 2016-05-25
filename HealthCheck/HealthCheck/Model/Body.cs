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
        public StringWriter Output = new StringWriter();

        public Body()
        {
            Output.Write("Status - ");
            //ask for http status
            try
            {
                HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create("https://10.0.1.221:9000/");
                webRequest.AllowAutoRedirect = false;
                HttpWebResponse response = (HttpWebResponse)webRequest.GetResponse();
                Output.Write(response.StatusCode.ToString() + Environment.NewLine);
                if (response.StatusCode.ToString() != "OK") { Console.WriteLine(Output); return; }
            }
            catch
            {
                throw new System.ArgumentException("Parameter cannot be null", "original");
            }
            //download file (as string)
            WebClient webClient = new WebClient();
            string text = webClient.DownloadString("https://10.0.1.221:9000/");
            //Prepare for output
            JObject stuff = JObject.Parse(text);
            JArray workers = (JArray)stuff["Workers"];

            Output.WriteLine("Is Db Connected - " + stuff["IsDbConnected"]);
            Output.WriteLine("Version - " + stuff["Version"]);
            Output.WriteLine("Workers count - " + workers.Count);
            for (int i = 0; i < workers.Count; i++)
            {
                Output.WriteLine(" - Worker " + workers[i]["Name"] + " - " + workers[i]["StatusText"]);
            }
                      
            Console.WriteLine(Output);
            Console.ReadKey();

        }
        

        
    }
}
