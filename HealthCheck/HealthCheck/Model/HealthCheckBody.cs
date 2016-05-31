using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using System.Configuration;
using Newtonsoft.Json;
using System.Net.Http;
using System.Web;

namespace HealthCheck.Model
{
    class HealthCheckBody
    {
        public static HealthCheckDto Check(string adress)
        {
            string text = "";


                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(adress);
                    var result = client.GetAsync("").Result;
                    string resultContent = result.Content.ReadAsStringAsync().Result;
                    text = resultContent;

                }

                var healthCheckBody = JsonConvert.DeserializeObject<HealthCheckDto>(text);
                return healthCheckBody;
                

            

        }
    }
}
