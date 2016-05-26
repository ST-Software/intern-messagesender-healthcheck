using System;
using System.Net;
using System.IO;
using Newtonsoft.Json.Linq;
using System.Web.Script.Serialization;

namespace HealthCheck.Model
{
    class Status
    {
        public StringWriter Output = new StringWriter();

        public bool GetStatus(string adress)
        {
            string text = "";
            try
            {
                HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(adress);
                webRequest.AllowAutoRedirect = false;
                HttpWebResponse response = (HttpWebResponse)webRequest.GetResponse();
                Output.WriteLine(response.StatusCode.ToString());
                Stream stream = response.GetResponseStream();
                StreamReader readStream = new StreamReader(stream);
                Char[] read = new Char[256];    
                int count = readStream.Read(read, 0, 256);
                while (count > 0)
                {
                    text += new String(read, 0, count);
                    count = readStream.Read(read, 0, 256);
                }
                HealthCheckDto healthcheckdto = new JavaScriptSerializer().Deserialize<HealthCheckDto>(text);
                Output.WriteLine("IsDbConnected - " + healthcheckdto.IsDbConnected);
                Output.WriteLine("Version - " + healthcheckdto.Version);
                Output.WriteLine("Workers count - " + healthcheckdto.Workers.Count);
                foreach(var item in healthcheckdto.Workers)
                {
                    Output.WriteLine(" - Worker " + item.Name + " is " + item.StatusText);
                }
                //JObject stuff = JObject.Parse(text);
                //JArray workers = (JArray)stuff["Workers"];
                //Output.WriteLine("Is Db Connected - " + stuff["IsDbConnected"]);
                //Output.WriteLine("Version - " + stuff["Version"]);
                //Output.WriteLine("Workers count - " + workers.Count);
                //for (int i = 0; i < workers.Count; i++)
                //{
                //    Output.WriteLine(" - Worker " + workers[i]["Name"] + " - " + workers[i]["StatusText"]);
                //}
                //Console.WriteLine(Output);
            }
            catch
            {
                return false;
            }
            return true;
        }
    }
}
