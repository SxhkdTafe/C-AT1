using Dr;
using Comp;
using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;
using System.Threading.Tasks.Dataflow;
namespace C_2AT1
{
    public class Controller
    {
        Queue<Drone> Express;
        Queue<Drone> Regular;
        List<Completed> Completed;
        public Controller() 
        {
            Regular = new Queue<Drone>();
            Express = new Queue<Drone>();
            Completed = new List<Completed>();
        }
        public void DroneAddReg(string model, string problem, string client, double cost, string tag)
        {
            Regular.Enqueue(new Drone(model, problem, client, cost, RegTagGen()));
        }
        public void DroneAddExp(string model, string problem, string client, double cost, string tag)
        {
            Express.Enqueue(new Drone(model, problem, client, ExpCostCalc(cost,0.15), ExpTagGen()));
        }
        public void DroneRemoveReg()
        {
            Regular.Dequeue();
        }
        public void DroneRemoveExp()
        {
            Express.Dequeue();
        }
        public double ExpCostCalc(double cost,double surcharge)
        {
            return cost + (cost * surcharge);
        }
        public string ExpTagGen()
        {
            return $"#{Express.Count + 1}"; 
        }
        public string RegTagGen()
        {
            return $"#{Regular.Count + 1}";
        }
        public void AddCompleteList(int choice)
        {
            if (choice == 1)
            {
                Drone d = Regular.Peek();
                Completed.Add(new Completed(d, "Regular"));
            }
            else if (choice == 2) 
            {
                Drone d = Express.Peek();
                Completed.Add(new Completed(d, "Express"));
            }
        }
        public void PaymentProcess(double Paid, string Name, string Tag, string Type)
        {
            foreach (Completed item in Completed) 
            {
                if (item.ClientName == Name && item.Tag == Tag && item.Type == Type)
                {
                    item.Cost = item.Cost - Paid;
                }
            }
        }
        public void RemovePayed()
        {
            foreach (Completed item in Completed)
            {
                if (item.Cost == 0)
                {
                    Completed.Remove(item);
                }
            }
        }
        public string HandleOverPay()
        {

        }
    }
}
