using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PROG7312_POE.MVC.Controller
{
    internal class AppThemes
    {
        /// <summary>
        /// Changes Styles based on user Input
        /// </summary>
        /// <param name="themeUri"></param>
        public static void ChangeTheme(Uri themeUri)
        {
            ResourceDictionary Theme = new ResourceDictionary() { Source = themeUri };
            ResourceDictionary AppStyle = new ResourceDictionary() { Source = new Uri("MVC/View/Styles/AppStyle.xaml", UriKind.Relative) };


            App.Current.Resources.Clear();
            App.Current.Resources.MergedDictionaries.Add(Theme);
            App.Current.Resources.MergedDictionaries.Add(AppStyle);

        }
    }
}
