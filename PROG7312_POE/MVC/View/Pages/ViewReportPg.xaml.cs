using System;
using System.Collections.Generic;
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
    public partial class ViewReportPg : Page
    {

        public ViewReportPg()
        {
            InitializeComponent();
            ReportItemsControl.Items.Clear();
            ReportItemsControl.DataContext = MainController.reportController;
            ReportItemsControl.ItemsSource = MainController.reportController.ReportData;

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

        private void displayReports()
        {
            foreach (var report in MainController.reportController.ReportData)
            {
                ReportItemsControl.Items.Add(report);
            }
        }

        private void BtnOpenPdf_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var pdfNode = button?.Tag as MediaNode;
            if (pdfNode != null)
            {
                try
                {
                    string tempFilePath = Path.Combine(Path.GetTempPath(), pdfNode.Name);
                    File.WriteAllBytes(tempFilePath, pdfNode.MediaData);
                    Process.Start(new ProcessStartInfo(tempFilePath) { UseShellExecute = true });
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Failed to open PDF: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void BtnPlayVideo_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var videoNode = button?.Tag as MediaNode;
            if (videoNode != null)
            {
                try
                {
                    Process.Start(new ProcessStartInfo(videoNode.MediaData.ToString()) { UseShellExecute = true });
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Failed to play video: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            var parentWindow = Window.GetWindow(this) as MainWindow;
            var homePg = new HomePg();
            parentWindow.RbtnHome.IsChecked = true;
            parentWindow.ContentPane.Content = homePg;
        }

        private void BtnVideoPlay_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            if (button != null)
            {
                // Find the StackPanel (parent) of the button clicked
                var stackPanel = button.Parent as StackPanel;
                if (stackPanel != null)
                {
                    // Find the MediaElement inside the StackPanel
                    MediaElement mediaElement = stackPanel.Children.OfType<MediaElement>().FirstOrDefault();
                   
                    if (mediaElement != null)
                    {
                        // Toggle play/pause based on the MediaElement's current state
                        if (mediaElement.LoadedBehavior == MediaState.Play)
                        {
                            mediaElement.Pause();
                            
                        }
                        else
                        {
                            
                            mediaElement.Play();
                        }
                    }
                }
            }
        }

    }
}
