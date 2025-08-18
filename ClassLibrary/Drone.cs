using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dr
{
    public class Drone
    {
        public string ClientName { get; set; }
        public string DroneModel { get; set; }
        public string Problem { get; set; }
        public double Cost { get; set; }
        public string Tag { get; set; }
        public Drone()
        {

        }
        public Drone(string model, string problem, string client, double cost, string tag)
        {
            this.ClientName = model;
            this.Problem = problem;
            this.Cost = cost;
            this.Tag = tag;
            this.DroneModel = model;
        }

    }
}
