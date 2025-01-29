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
using warsztatsamochodowy.Views;

namespace warsztatsamochodowy
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        
        private void OpenCarWindow_Click(object sender, RoutedEventArgs e)
        {
            var carWindow = new CarWindow(); 
            carWindow.Show(); 
        }

        
        private void OpenMechanicWindow_Click(object sender, RoutedEventArgs e)
        {
            var mechanicWindow = new MechanicWindow(); 
            mechanicWindow.Show(); 
        }

        private void OpenJobWindow_Click(object sender, RoutedEventArgs e)
        {
            var jobWindow = new JobWindow();
            jobWindow.Show();
        }

        private void  OpenOrderWindow_Click(object sender, RoutedEventArgs e)
        {
            var orderWindow = new OrderWindow();
            orderWindow.Show();
        }

        private void OpenClientWindow_Click(object sender, RoutedEventArgs e)
        {
            var clientWindow = new ClientWindow();
            clientWindow.Show();
        }

        private void OpenOrderReport_Click(object sender, RoutedEventArgs e)
        {
            var orderReportWindow = new OrderReportView();
            orderReportWindow.Show();
        }

    }
}
