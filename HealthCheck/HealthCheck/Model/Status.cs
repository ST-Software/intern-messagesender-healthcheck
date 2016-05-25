using System;
using System.Net;

namespace HealthCheck.Model
{
    class Status
    {
        public bool GetStatus(string adress, out string output)
        {
            output = "";
            try
            {
                HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(adress);
                webRequest.AllowAutoRedirect = false;
                HttpWebResponse response = (HttpWebResponse)webRequest.GetResponse();
                output = response.StatusCode.ToString() + Environment.NewLine;
            }
            catch
            {
                return false;
            }
            return true;
        }
    }
}
