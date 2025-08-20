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
using C_2AT1;
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
        public MainWindow()
        {
            InitializeComponent();
            Type.Items.Add("Regular");
            Type.Items.Add("Express");
            CompleteType.Items.Add("Regular");
            CompleteType.Items.Add("Express");
            controller = new DroneController();
        }
   
        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
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
                RegularDisplayBox.Document.Blocks.Clear();
                RegularDisplayBox.AppendText(controller.DisplayQueueReg());
            }
            else if (selectedindex == 1) 
            {
                controller.DroneAddExp(DroneModel, Problem, ClientName, cost);
                ExpressDisplayBox.Document.Blocks.Clear();
                ExpressDisplayBox.AppendText(controller.DisplayQueueExp());
            }
        }

        private void RegCompleteButton_Click(object sender, RoutedEventArgs e)
        {
            controller.AddCompleteListReg();
            controller.DroneRemoveReg();
            RegularDisplayBox.Document.Blocks.Clear();
            RegularDisplayBox.AppendText(controller.DisplayQueueReg());
            CompleteDisplayBox.Document.Blocks.Clear();
            CompleteDisplayBox.AppendText(controller.DisplayQueueCom());
        }

        private void ExpCompleteButton_Click(object sender, RoutedEventArgs e)
        {
            
            controller.AddCompleteListExp();
            controller.DroneRemoveExp();
            ExpressDisplayBox.Document.Blocks.Clear();
            ExpressDisplayBox.AppendText(controller.DisplayQueueExp());
            CompleteDisplayBox.Document.Blocks.Clear();
            CompleteDisplayBox.AppendText(controller.DisplayQueueCom());
        }

        private void PayedButton_Click(object sender, RoutedEventArgs e)
        {
            var errors = new List<string>();
            int selectedindex = CompleteType.SelectedIndex;
            if (!WPFHelper.ValidateInput(CompleteDroneTagBox, "", out string CompleteDroneTagError))
            {
                errors.Add($"Drone Tag: {CompleteDroneTagError}");
            }
            if (!WPFHelper.ValidateInput(CompleteDroneModelBox, "", out string CompleteDroneModelError))
            {
                errors.Add($"Drone Model: {CompleteDroneModelError}");
            }
            if (selectedindex < 0)
            {
                errors.Add("Please select a index");
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

            string type = CompleteType.Text;
            string DroneTag = ("#"+CompleteDroneTagBox.Text);
            string DroneModel = CompleteDroneModelBox.Text;
            double Payed;
            double.TryParse(AmountPayedBox.Text, out Payed);

            controller.PaymentProcess(Payed, DroneModel, DroneTag, type);
            controller.RemovePayed();
            CompleteDisplayBox.Document.Blocks.Clear();
            CompleteDisplayBox.AppendText(controller.DisplayQueueCom());
        }
    }
}