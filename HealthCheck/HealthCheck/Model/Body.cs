using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Net;
using System.IO;

namespace HealthCheck.Model
{
    class Body
    {
        List<Worker> Workers = new List<Worker>();
        public string Output { get; private set; } = "";

        public Body()
        {
            DoThings();
            
        }

        public void DoThings()
        {
            //Need download or copy the XML file from 10.0.1.221:9000

            using (XmlReader xr = XmlReader.Create(@"workers.xml"))
            {
                string name = "";
                string status = "";
                string element = "";

                while (xr.Read())
                {
                    if (xr.NodeType == XmlNodeType.Element)
                    {
                        element = xr.Name;
                        
                    }
                    else if (xr.NodeType == XmlNodeType.Text)
                    {
                        switch (element)
                        {
                            case "IsDbConnected":
                                Output +="IsDbConnected - " + xr.Value + Environment.NewLine;
                                break;
                            case "ServiceStatus":
                                Output +="ServiceStatus - " + xr.Value + Environment.NewLine;
                                break;
                            case "Version":
                                Output += "Version: " + xr.Value + Environment.NewLine;
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
                Output += wor + Environment.NewLine;
            }
            Console.WriteLine(Output);
            File.WriteAllText(@"C:\Users\Vítek\ST_SW\intern-messagesender-healthcheck\logs\Data.txt", Output);
            Console.ReadKey();
        }
    }
}
