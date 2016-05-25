using System.Net;
using Newtonsoft.Json.Linq;
using System.IO;

namespace HealthCheck.Model
{
    class Conclusion

    {
        public StringWriter output = new StringWriter();
        public string GetText(string adress)
        {
            WebClient webClient = new WebClient();
            JObject stuff = JObject.Parse(webClient.DownloadString(adress));
            JArray workers = (JArray)stuff["Workers"];
            output.WriteLine("Is Db Connected - " + stuff["IsDbConnected"]);
            output.WriteLine("Version - " + stuff["Version"]);
            output.WriteLine("Workers count - " + workers.Count);
            for (int i = 0; i < workers.Count; i++)
            {
                output.WriteLine(" - Worker " + workers[i]["Name"] + " - " + workers[i]["StatusText"]);
            }
            return output.ToString();
        }
    }
}
