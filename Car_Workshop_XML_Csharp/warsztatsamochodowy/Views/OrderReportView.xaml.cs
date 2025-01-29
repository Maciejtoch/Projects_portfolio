using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using warsztatsamochodowy.Models;
using warsztatsamochodowy.ViewModels;

namespace warsztatsamochodowy.Views
{
    /// <summary>
    /// Logika interakcji dla klasy OrderReportView.xaml
    /// </summary>
    public partial class OrderReportView : Window
    {
        public OrderReportView()
        {
            InitializeComponent();
            DataContext = new OrderReportViewModel();
        }
    }
}
