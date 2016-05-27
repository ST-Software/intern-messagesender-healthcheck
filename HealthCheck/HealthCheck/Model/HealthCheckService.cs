using System.Net;
using System.IO;
using Newtonsoft.Json;

namespace HealthCheck.Model
{
    class HealthCheckService
    {
        public static HealthCheckDto GetHealthCheck(string adress)
        {
            string text = "";
            string status = "";
            try
            {
                HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(adress);
                webRequest.AllowAutoRedirect = false;
                HttpWebResponse response = (HttpWebResponse)webRequest.GetResponse();
                int statuscode = (int)response.StatusCode;
                status = statuscode + " (" + response.StatusCode.ToString()+ ")";
                if (statuscode != 200) { return new HealthCheckDto() { HttpResponseStatus = statuscode, HttpResponseStatusText = status }; }

                Stream stream = response.GetResponseStream();
                StreamReader readStream = new StreamReader(stream);
                text = readStream.ReadToEnd();
                var healthcheck = JsonConvert.DeserializeObject<HealthCheckDto>(text);
                healthcheck.HttpResponseStatus = statuscode;
                healthcheck.HttpResponseStatusText = status;
                return healthcheck; 
            }
            catch
            {
                return null;
            }
        }
    }
}
