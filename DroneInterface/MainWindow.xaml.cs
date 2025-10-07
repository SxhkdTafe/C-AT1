using C_2AT1;
using Dr;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WPFHelpers;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace DroneInterface
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private DroneController controller;
        public ObservableCollection<Drone> ExpressNames { get; set; } = new ObservableCollection<Drone>();
        public ObservableCollection<Drone> RegularNames { get; set; } = new ObservableCollection<Drone>();
        public ObservableCollection<Drone> CompNames { get; set; } = new ObservableCollection<Drone>();
        public MainWindow()
        {
            InitializeComponent();
            Type.Items.Add("Regular");
            Type.Items.Add("Express");
            controller = new DroneController();
            this.DataContext = this;
        }
   
        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }


        //adjust name
        private void SubmitButton_Click(object sender, RoutedEventArgs e)
        {
            int selectedindex = Type.SelectedIndex;
            var errors = new List<string>();
            if (!WPFHelper.ValidateInput(ClientNameBox, "",out string ClientNameError)) 
            {
                errors.Add($"Client Name: {ClientNameError}");
            }
            if (!WPFHelper.ValidateInput(DroneModelBox, "", out string DroneModelError))
            {
                errors.Add($"Drone Model: {DroneModelError}");
            }
            if (!WPFHelper.ValidateInput(ProblemBox, "", out string ProblemError))
            {
                errors.Add($"Probelm Error: {ProblemError}");
            }

            
            if (!WPFHelper.ValidateInput(CostBox, @"^\d+(\.\d{1,2})?$", out string CostBoxError))
            {
                errors.Add($"Cost: {CostBoxError}");
            }

;
            if (selectedindex < 0 ) 
            {
                errors.Add("Please select a index");
            }

            if (errors.Any())
            {
                string errormessage =  string.Join(" | ", errors);
                ErrorBar.Items.Clear();
                ErrorBar.Items.Add(errormessage);
                return;
            }

            string ClientName = ClientNameBox.Text;
            string DroneModel = DroneModelBox.Text;
            string Problem = ProblemBox.Text;
            double cost;
            double.TryParse(CostBox.Text, out cost);
            if (selectedindex == 0)
            {
                controller.DroneAddReg(DroneModel,Problem,ClientName,cost);
                RegularNames.Clear();
                foreach (var item in controller.DisplayQueueReg())
                {
                    RegularNames.Add(item);
                }
            }
            else if (selectedindex == 1) 
            {
                
                controller.DroneAddExp(DroneModel, Problem, ClientName, cost);
                ExpressNames.Clear();
                foreach (var item in controller.DisplayQueueExp())
                {
                    ExpressNames.Add(item);
                }
            }
            ClientNameBox.Clear();
            DroneModelBox.Clear();
            ProblemBox.Clear();
            CostBox.Clear();
            Type.SelectedIndex = -1;
        }
        private void RegQBox_SelectionChanged(object sender, EventArgs e)
        {          
            var item = RegQBox.SelectedItem as Drone;
            if (item != null)
            {
                ClientNameBox.Text = item.ClientName;
                DroneModelBox.Text = item.DroneModel;
                ProblemBox.Text = item.Problem;
                CostBox.Text = item.Cost.ToString();
                Type.SelectedIndex = 0;
            }
        }
        private void ExpQBox_SelectionChanged(object sender, EventArgs e)
        {
            var item = ExpQBox.SelectedItem as Drone;
            if (item != null)
            {
                ClientNameBox.Text = item.ClientName;
                DroneModelBox.Text = item.DroneModel;
                ProblemBox.Text = item.Problem;
                CostBox.Text = item.Cost.ToString();
                Type.SelectedIndex = 1;
            }
        }
        private void CompListBox_SelectionChanged(object sender, EventArgs e)
        {
            var item = CompListBox.SelectedItem as Drone;
            if (item != null)
            {
                CompleteDroneModelBox.Text = item.DroneModel;
                AmountPayedBox.Text = item.Cost.ToString();
                CompleteDroneTagBox.Text = item.Tag;
            }
        }
        private void RegCompleteButton_Click(object sender, RoutedEventArgs e)
        {
            controller.AddCompleteListReg();
            controller.DroneRemoveReg();
            RegularNames.Clear();
            foreach (var item in controller.DisplayQueueReg())
            {
                RegularNames.Add(item);
            }
            foreach (var item in controller.DisplayQueueCom())
            {
                CompNames.Add(item);
            }
        }

        private void ExpCompleteButton_Click(object sender, RoutedEventArgs e)
        {
            
            controller.AddCompleteListExp();
            controller.DroneRemoveExp();
            ExpressNames.Clear();
            foreach (var item in controller.DisplayQueueExp())
            {
                ExpressNames.Add(item);
            }
            CompNames.Clear();
            foreach (var item in controller.DisplayQueueCom())
            {
                CompNames.Add(item);
            }
        }

        private void PayedButton_Click(object sender, RoutedEventArgs e)
        {
            var errors = new List<string>();
            if (!WPFHelper.ValidateInput(CompleteDroneTagBox, "", out string CompleteDroneTagError))
            {
                errors.Add($"Drone Tag: {CompleteDroneTagError}");
            }
            if (!WPFHelper.ValidateInput(CompleteDroneModelBox, "", out string CompleteDroneModelError))
            {
                errors.Add($"Drone Model: {CompleteDroneModelError}");
            }

            if (!WPFHelper.ValidateInput(AmountPayedBox, @"^\d+(\.\d{1,2})?$", out string AmountPayedBoxError))
            {
                errors.Add($"Amount Payed: {AmountPayedBoxError}");
            }

            if (errors.Any())
            {
                string errormessage = string.Join(" | ", errors);
                ErrorBar.Items.Clear();
                ErrorBar.Items.Add(errormessage);
                return;
            }

            string DroneTag = (CompleteDroneTagBox.Text);
            string DroneModel = CompleteDroneModelBox.Text;
            double Payed;
            double.TryParse(AmountPayedBox.Text, out Payed);

            controller.PaymentProcess(Payed, DroneModel, DroneTag);
            controller.RemovePayed();
            CompNames.Clear();
            foreach (var item in controller.DisplayQueueCom())
            {
                CompNames.Add(item);
            }
        }
    }
}