using PROG7312_POE.MVVM.View.Pages;
using PROG7312_POE.MVVM.View.Styles;
using PROG7312_POE.MVVM.View.UserControls;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PROG7312_POE.MVC.View.Pages
{
    /// <summary>
    /// Interaction logic for IntroPg.xaml
    /// </summary>
    public partial class IntroPg : Page
    {
        private int stepCnt = 0;
        public IntroPg()
        {
            InitializeComponent();
            SiyaCntrl.SiyaTxt.Text = "Welcome to Springbok Support!\nMy name is Siya the Springbok and I will be guiding you through the app.\nYou can skip this at anytime by clicking the skip button.";
        }

        private void BtnNext_Click(object sender, RoutedEventArgs e)
        {
            stepCnt++;
            switch (stepCnt)
            {
                case 1:
                    SiyaCntrl.SiyaTxt.Text = "Here is the Expnading Navbar always located on the left of the window";
                    this.StckPnlTut01.Visibility = Visibility.Visible;

                    break;
                case 2:
                    SiyaCntrl.SiyaTxt.Text = "This Panel will expand if your mouse is over and will aid in app navigation. ";
                    this.StckPnlTut01.Visibility = Visibility.Collapsed;
                    this.StckPnlTut02.Visibility = Visibility.Visible;
                    break;
                case 3:
                    SiyaCntrl.SiyaTxt.Text = "On the home page you can click here to file a report. Hopefully you never have to but just in case.";
                    this.StckPnlTut02.Visibility = Visibility.Collapsed;
                    this.StckPnlTut03.Visibility = Visibility.Visible;
                    break;
                case 4:
                    SiyaCntrl.SiyaTxt.Text = "This feature is currently locked and under development and will allow the user to access news on events and announcements.";
                    this.StckPnlTut03.Visibility = Visibility.Collapsed;
                    this.StckPnlTut04.Visibility = Visibility.Visible;
                    break;
                case 5:
                    SiyaCntrl.SiyaTxt.Text = "This feature is currently locked and under development. It will allow the user to view the progress of their reports and their details ";
                    this.StckPnlTut04.Visibility = Visibility.Collapsed;
                    this.StckPnlTut05.Visibility = Visibility.Visible;
                    break;
                    case 6:
                    MainWindow parentWindow = Window.GetWindow(this) as MainWindow;
                    HomePg homePg = new HomePg();
                    parentWindow.RbtnHome.IsChecked = true;
                    parentWindow.ContentPane.Content = homePg;
                    break;
            }

        }

        private void BtnSkip_Click(object sender, RoutedEventArgs e)
        {
            MainWindow parentWindow = Window.GetWindow(this) as MainWindow;
            HomePg homePg = new HomePg();
            parentWindow.RbtnHome.IsChecked = true;
            parentWindow.ContentPane.Content = homePg;
        }
    }
}
