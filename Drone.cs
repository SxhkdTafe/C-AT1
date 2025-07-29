using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dr
{
    public class Drone
    {
        private string ClientName { get; set; }
        private string DroneModel { get; set; }
        private string Problem { get; set; }
        private double Cost { get; set; }
        private string Tag { get; set; }
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
