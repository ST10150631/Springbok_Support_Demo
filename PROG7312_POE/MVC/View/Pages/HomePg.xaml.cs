using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using PROG7312_POE.MVVM.View.Pages;
using PROG7312_POE.MVVM.View;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using PROG7312_POE.MVVM.View.Styles;

namespace PROG7312_POE.MVVM.View.Pages
{
    /// <summary>
    /// Interaction logic for HomePg.xaml
    /// </summary>
    public partial class HomePg : Page
    {
        public HomePg()
        {
            InitializeComponent();
        }

        private void BtnReport_Click(object sender, RoutedEventArgs e)
        {
            MainWindow parentWindow = Window.GetWindow(this) as MainWindow;
            NewReportPg newReportPg = new NewReportPg();
            parentWindow.RbtnReport.IsChecked = true;
            parentWindow.ContentPane.Content = newReportPg;
        }
    }
}
