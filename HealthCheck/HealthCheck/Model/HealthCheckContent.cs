using System;
using System.Net;
using System.IO;
using Newtonsoft.Json;

namespace HealthCheck.Model
{
    class HealthCheckContent
    {
        

        public HealthCheckDto GetStatusAndContent(string adress,HealthCheckDto healthclass, out string status)
        {
            string text = "";
            status = "";
            try
            {
                HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(adress);
                webRequest.AllowAutoRedirect = false;
                HttpWebResponse response = (HttpWebResponse)webRequest.GetResponse();
                int statuscode = (int)response.StatusCode;
                status = statuscode + " (" + response.StatusCode.ToString()+ ")";
                if (statuscode != 200) { return null; }

                Stream stream = response.GetResponseStream();
                StreamReader readStream = new StreamReader(stream);
                text = readStream.ReadToEnd();
                return healthclass = JsonConvert.DeserializeObject<HealthCheckDto>(text); 
               
            }
            catch
            {
                return null;
            }
        }
    }
}
