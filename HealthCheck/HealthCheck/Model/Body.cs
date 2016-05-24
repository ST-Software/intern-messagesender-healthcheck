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
                        if()
                        if (element == "Name")
                        {
                            name = xr.Value;
                        }
                        else if (element == "Status")
                        {
                            status = xr.Value;
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
