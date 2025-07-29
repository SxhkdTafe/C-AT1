using Dr;
using Comp;
using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;
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
                Completed.Add(Express.ToList());
            }
            else if (choice == 2) 
            {

            }
        }
        public void convert()
        {
            Express.Peek();
            Express.ToList();
        }
    }
}
