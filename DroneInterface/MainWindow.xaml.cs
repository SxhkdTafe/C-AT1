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
            int i = Type.SelectedIndex;
            string ClientName = ClientNameBox.Text;
            string DroneModel = DroneModelBox.Text;
            string Problem = ProblemBox.Text;
            double Cost;
            double.TryParse(CostBox.Text, out Cost);
            if (i == 0)
            {
                controller.DroneAddReg(DroneModel,Problem,ClientName,Cost);
                RegularDisplayBox.Document.Blocks.Clear();
                RegularDisplayBox.AppendText(controller.DisplayQueueReg());
            }
            else if (i == 1) 
            {
                controller.DroneAddExp(DroneModel, Problem, ClientName, Cost);
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
            string type = CompleteType.Text;
            string DroneTag = CompleteDroneTagBox.Text;
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