using PROG7312_POE.MVC.View.Pages;
using PROG7312_POE.MVVM.View.Pages;
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

namespace PROG7312_POE.MVVM.View.Styles
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            IntroPg introPg = new IntroPg(); 
            ContentPane.Navigate(introPg);

            HomePg homePg = new HomePg();
        }
        /// <summary>
        /// Ensures that the user can use the mouse to drag the window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        ///>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>> Start of Method >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }
        //------------------------------------------------------------------------ End of Method ------------------------------------------------------------------------------------------


        /// <summary>
        /// When minimized button is clicked the window will minimize
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>> Start of Method >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
        private void BtnMinimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }
        //------------------------------------------------------------------------ End of Method ------------------------------------------------------------------------------------------


        /// <summary>
        /// Shuts down the app when close button clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>> Start of Method >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
        private void BtnClose_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void BtnResize_Click(object sender, RoutedEventArgs e)
        {

            if (WindowState != WindowState.Maximized)
            {

                WindowState = WindowState.Maximized;

            }
            else WindowState = WindowState.Normal;



        }

        private void RbtnHome_Checked(object sender, RoutedEventArgs e)
        {
            ContentPane.Navigate(new HomePg());
        }

        private void RbtnReport_Checked(object sender, RoutedEventArgs e)
        {
            NewReportPg newReportPg = new NewReportPg();
            ContentPane.Navigate(newReportPg);
        }

        private void RbtnEvents_Checked(object sender, RoutedEventArgs e)
        {
            NewsPage events = new NewsPage();
            ContentPane.Navigate(events);
        }

        private void RbtnStatus_Checked(object sender, RoutedEventArgs e)
        {
            ServiceStatusPage serviceStatusPage = new ServiceStatusPage();
            ContentPane.Navigate(serviceStatusPage);
            //ViewReportPg viewReportPg = new ViewReportPg();
            //ContentPane.Navigate(viewReportPg);
        }

        private void RbtnSettings_Checked(object sender, RoutedEventArgs e)
        {
            SettingsPg settingsPg = new SettingsPg();
            ContentPane.Navigate(settingsPg);
        }
        //------------------------------------------------------------------------ End of Method ------------------------------------------------------------------------------------------
    }
}
