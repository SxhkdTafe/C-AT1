using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comp
{
    public class Completed
    {
        private string ClientName { get; set; }
        private string DroneModel { get; set; }
        private string Problem { get; set; }
        private double Cost { get; set; }
        private string Tag { get; set; }
        private string Type { get; set; }
        public Completed()
        {

        }
        public Completed(string clientName, string droneModel, string problem, double cost, string tag, string type)
        {
            ClientName = clientName;
            DroneModel = droneModel;
            Problem = problem;
            Cost = cost;
            Tag = tag;
            Type = type;
        }
    }
}
