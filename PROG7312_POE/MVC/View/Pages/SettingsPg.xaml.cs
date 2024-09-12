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
using PROG7312_POE.MVC.Controller;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PROG7312_POE.MVC.View.Pages
{
    /// <summary>
    /// Interaction logic for SettingsPg.xaml
    /// </summary>
    public partial class SettingsPg : Page
    {
        public SettingsPg()
        {
            InitializeComponent();
        }

        private void BtnOriginal_Click(object sender, RoutedEventArgs e)
        {
            AppThemes.ChangeTheme(new Uri("MVC/View/Styles/LightMode.xaml", UriKind.Relative));
        }

        private void BtnDarkMode_Click(object sender, RoutedEventArgs e)
        {
            AppThemes.ChangeTheme(new Uri("MVC/View/Styles/DarkMode.xaml", UriKind.Relative));
        }

        private void BtnLightMode_Click(object sender, RoutedEventArgs e)
        {
            AppThemes.ChangeTheme(new Uri("MVC/View/Styles/Red.xaml", UriKind.Relative));
        }
    }
}
