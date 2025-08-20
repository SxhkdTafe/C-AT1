using Dr;
using Comp;
using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;
using System.Threading.Tasks.Dataflow;
namespace C_2AT1
{
    public class DroneController
    {
        Queue<Drone> Express;
        Queue<Drone> Regular;
        List<Completed> Completed;
        public DroneController() 
        {
            Regular = new Queue<Drone>();
            Express = new Queue<Drone>();
            Completed = new List<Completed>();
        }
        public void DroneAddReg(string model, string problem, string client, double cost)
        {
            Regular.Enqueue(new Drone(model, problem, client, cost, RegTagGen()));
        }
        public void DroneAddExp(string model, string problem, string client, double cost)
        {
            Express.Enqueue(new Drone(model, problem, client, ExpCostCalc(cost,0.15), ExpTagGen()));
        }
        public void DroneRemoveReg()
        {
            if (Regular.Count > 0) 
            {
                Regular.Dequeue();
            }
        }
        public void DroneRemoveExp()
        {
            if (Express.Count > 0) 
            {
                Express.Dequeue();
            }

        }
        public string DisplayQueueExp()
        {
            if (Express.Count > 0)
            {
                string result = "";
                foreach (var drone in Express)
                {
                    result += $"Client Name: {drone.ClientName} | Drone Model: {drone.DroneModel} | Problem: {drone.Problem} | Cost: ${drone.Cost} | Tag:{drone.Tag}\n";
                }
                return result;
            }           
            return ("Express Queue is Empty");             
        }
        public string DisplayQueueReg()
        {
            if (Regular.Count > 0)
            {
                string result = "";
                foreach (var drone in Regular)
                {
                    result += $"Client Name: {drone.ClientName} | Drone Model: {drone.DroneModel} | Problem: {drone.Problem} | Cost: ${drone.Cost} | Tag:{drone.Tag}\n";
                }
                return result;
            }
            return "Regular Queue is Empty";
        }
        public string DisplayQueueCom()
        {
            if (Completed.Count > 0)
            {
                string result = "";
                foreach (var drone in Completed)
                {
                    result += $"Client Name: {drone.ClientName} | Drone Model: {drone.DroneModel} | Problem: {drone.Problem} | Cost: ${drone.Cost} | Tag:{drone.Tag} | Type: {drone.Type}\n";
                }
                return result;
            }
            return "Complete Queue is Empty";
        }
        public double ExpCostCalc(double cost,double surcharge)
        {
            return cost + (cost * surcharge);
        }
        public string ExpTagGen()
        {
            int num = 0; 
            foreach (var d in Completed)
            {
                if (d.Type == "Express")
                {
                    num++;
                }
            }
            return $"#{Express.Count + 1 + num}"; 
        }
        public string RegTagGen()
        {
            int num = 0;
            foreach (var d in Completed)
            {
                if (d.Type == "Regular")
                {
                    num++;
                }
            }
            return $"#{Regular.Count + 1 + num}";
        }
        public void AddCompleteListReg()
        {
            if (Regular.Count > 0)
            {
                Drone d = Regular.Peek();
                Completed.Add(new Completed(d, "Regular"));
            }
        }
        public void AddCompleteListExp()
        {
            if (Express.Count > 0)
            {
                Drone d = Express.Peek();
                Completed.Add(new Completed(d, "Express"));
            }
        }
        public void PaymentProcess(double Paid, string model, string Tag, string Type)
        {
            foreach (Completed item in Completed) 
            {
                if (item.DroneModel == model && item.Tag == Tag && item.Type == Type)
                {
                    item.Cost -= Paid;
                }
            }
        }
        public void RemovePayed()
        {
            for (int i = Completed.Count - 1; i >= 0; i--)
            {
                if (Completed[i].Cost <= 0)
                {
                    Completed.RemoveAt(i);
                }
            }
        }
    }
}
