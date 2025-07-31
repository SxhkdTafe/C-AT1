using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dr;

namespace Comp
{
    public class Completed
    {
        public string ClientName { get; set; }
        public string DroneModel { get; set; }
        public string Problem { get; set; }
        public double Cost { get; set; }
        public string Tag { get; set; }
        public string Type { get; set; }
        public Completed()
        {

        }
        public Completed(Drone d,string type)
        {
            ClientName = d.ClientName;
            DroneModel = d.DroneModel;
            Problem = d.Problem;
            Cost = d.Cost;
            Tag = d.Tag;
            Type = type;
        }
    }
}
