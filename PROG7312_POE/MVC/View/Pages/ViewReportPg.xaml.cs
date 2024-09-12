using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using PROG7312_POE.MVC.Controller;
using PROG7312_POE.MVC.Model;
using PROG7312_POE.MVVM.View.Pages;
using PROG7312_POE.MVVM.View.Styles;

namespace PROG7312_POE.MVC.View.Pages
{
    /// <summary>
    /// Interaction logic for ViewReportPg.xaml
    /// </summary>
    public partial class ViewReportPg : Page
    {
        private readonly string[] ImgExtensions = { ".jpg", ".jpeg", ".gif", ".bmp", ".png" };

        public ViewReportPg()
        {
            InitializeComponent();
            ReportItemsControl.Items.Clear();
            ReportItemsControl.DataContext = MainController.reportController;
            ReportItemsControl.ItemsSource = MainController.reportController.ReportData;
        }

        private void BtnViewMedia_Click(object sender, RoutedEventArgs e)
        {
            // Get the Button that was clicked
            var button = sender as Button;
            if (button == null) return;

            // Get the DataContext of the Button
            var report = button.DataContext as ReportModel;
            if (report == null) return;
            try
            {
                if (IsValidImageExtension(report.MediaType))
                {
                    this.ImgMedia.Source = report.image;
                    ImgMedia.Visibility = Visibility.Visible;
                }
                else if (report.MediaType == ".pdf" && report.ReportMedia != null)
                {
                    LoadPDF(report.ReportMedia);
                }
                else
                {
                    MessageBox.Show("Media type not supported", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }catch
            {

            }
            
        }

        private void LoadPDF(byte[] byteStream)
        {
            try
            {
                // Save ByteStream to a temporary file
                string tempFilePath = Path.GetTempFileName() + ".pdf";
                File.WriteAllBytes(tempFilePath, byteStream);

                // Open the file with the default PDF viewer
                Process.Start(new ProcessStartInfo(tempFilePath) { UseShellExecute = true });
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
            }
        }

        private bool IsValidImageExtension(string mediaType)
        {
            if (string.IsNullOrWhiteSpace(mediaType))
                return false;

            // Convert to lowercase to ensure case-insensitive comparison
            mediaType = mediaType.ToLower();

            // Check if the provided extension is in the list of valid extensions
            return ImgExtensions.Contains(mediaType);
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            MainWindow parentWindow = Window.GetWindow(this) as MainWindow;
            HomePg homePg = new HomePg();
            parentWindow.RbtnHome.IsChecked = true;
            parentWindow.ContentPane.Content = homePg;
        }
    }
}
