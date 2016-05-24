using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace HealthCheck.Model
{
    class Body
    {
        List<Worker> Workers = new List<Worker>();

        public Body()
        {
            DoThings();
            
        }

        public void DoThings()
        {
            
            using (XmlReader xr = XmlReader.Create(@"workers.xml"))
            {
                string name = "";
                string status = "";
                string element = "";

                while (xr.Read())
                {
                    if (xr.NodeType == XmlNodeType.Element)
                    {
                        element = xr.Name; // název aktuálního elementu
                        
                    }
                    else if (xr.NodeType == XmlNodeType.Text)
                    {
                        switch (element)
                        {
                            case "IsDbConnected":
                                Console.WriteLine("IsDbConnected - " + xr.Value);
                                break;
                            case "ServiceStatus":
                                Console.WriteLine("ServiceStatus - " + xr.Value);
                                break;
                            case "Version":
                                Console.WriteLine("Version: " + xr.Value);
                                break;
                            case "Name":
                                name = xr.Value;
                                break;
                            case "Status":
                                status = xr.Value;
                                break;
                        }
                    }

                    else if ((xr.NodeType == XmlNodeType.EndElement) && (xr.Name == "WorkerStatusDto"))
                        Workers.Add(new Worker(name, status));
                }
                WriteIt(Workers);
            }
        }

        public void WriteIt(List<Worker> workers)
        {
            foreach (Worker wor in workers)
            {
                Console.WriteLine(wor);
            }
            Console.ReadKey();
        }
    }
}
