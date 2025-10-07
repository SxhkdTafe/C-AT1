using Dr;
using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;
using System.Threading.Tasks.Dataflow;
namespace C_2AT1
{
    public class DroneController
    {
        //accessibility reference
        private Queue<Drone> Express;
        private Queue<Drone> Regular;
        private List<Drone> Completed;


        public DroneController() 
        {
            Regular = new Queue<Drone>();
            Express = new Queue<Drone>();
            Completed = new List<Drone>();
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
        public Queue<Drone> DisplayQueueExp()
        {
            Queue<Drone> lol = new Queue<Drone>();
            if (Express.Count > 0)
            {
                return Express;
            }
            return lol;
        }
        public Queue<Drone> DisplayQueueReg()
        {
            Queue<Drone> lol = new Queue<Drone>();
            if (Regular.Count > 0)
            {
                return Regular;
            }
            return lol;
        }
        public List<Drone> DisplayQueueCom()
        {
            List<Drone> lol = new List<Drone>();
            if (Completed.Count > 0)
            {
                return Completed;
            }
            return lol;
        }
        public double ExpCostCalc(double cost,double surcharge)
        {
            return cost + (cost * surcharge);
        }
        public string ExpTagGen()
        {
            int first = 0;
            int last = 900;
            if (Express.Count != 0)
            {
                string sfirst = Express.Peek().Tag;
                first = Convert.ToInt16(sfirst.Substring(1));
                string slast = Express.Last().Tag;
                last = Convert.ToInt16(slast.Substring(1));
            }
            if (first != 100)
            {
                return "#100";
            }
            else if (last > 900)
            {
                return "#100";
            }
            return $"#{last + 10}"; 
        }
        public string RegTagGen()
        {
            int first = 0;
            int last = 900;
            if (Regular.Count != 0)
            {
                string sfirst = Regular.Peek().Tag;
                first = Convert.ToInt16(sfirst.Substring(1));
                string slast = Regular.Last().Tag;
                last = Convert.ToInt16(slast.Substring(1));
            }

            if (first != 100)
            {
                return "#100";
            }
            else if ( last > 900)
            {
                return "100";
            }
            return $"#{last + 10}";
        }
        public void AddCompleteListReg()
        {
            if (Regular.Count > 0)
            {
                Drone d = Regular.Peek();
                Completed.Add(d);
            }
        }
        public void AddCompleteListExp()
        {
            if (Express.Count > 0)
            {
                Drone d = Express.Peek();
                Completed.Add(d);
            }
        }
        public void PaymentProcess(double Paid, string model, string Tag)
        {
            foreach (Drone item in Completed) 
            {
                if (item.DroneModel == model && item.Tag == Tag)
                {
                    item.Cost -= Paid;
                    break;
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
