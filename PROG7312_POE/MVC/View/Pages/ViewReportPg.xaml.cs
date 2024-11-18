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
        /// <summary>
        /// Constructor for the ViewReportPg class
        /// </summary>
        /// >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>> Start of Method >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
        public ViewReportPg()
        {
            InitializeComponent();
            ReportItemsControl.Items.Clear();
            ReportItemsControl.DataContext = MainController.reportController;
            ReportItemsControl.ItemsSource = MainController.reportController.ReportData;

        }
        // ------------------------------------------------------------------------ End of Method ------------------------------------------------------------------------------------------

        /// <summary>
        /// Opens the PDF file when the button is clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>> Start of Method >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
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
        // ------------------------------------------------------------------------ End of Method ------------------------------------------------------------------------------------------

        /// <summary>
        /// when the button is clicked, the video is played
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>> Start of Method >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
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
        // ------------------------------------------------------------------------ End of Method ------------------------------------------------------------------------------------------

        /// <summary>
        ///  Navigate to the Home page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>> Start of Method >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            var parentWindow = Window.GetWindow(this) as MainWindow;
            var homePg = new HomePg();
            parentWindow.RbtnHome.IsChecked = true;
            parentWindow.ContentPane.Content = homePg;
        }
        // ------------------------------------------------------------------------ End of Method ------------------------------------------------------------------------------------------

        /// <summary>
        /// Plays or pauses the video when the play button is clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>> Start of Method >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
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
        //------------------------------------------------------------------------ End of Method ------------------------------------------------------------------------------------------

        /// <summary>
        /// Navigate to the Service Status page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>> Start of Method >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
        private void BtnStatus_Click(object sender, RoutedEventArgs e)
        {
            var parentWindow = Window.GetWindow(this) as MainWindow;
            var statusPg = new ServiceStatusPage();
            parentWindow.RbtnHome.IsChecked = true;
            parentWindow.ContentPane.Content = statusPg;
        }
        //------------------------------------------------------------------------ End of Method ------------------------------------------------------------------------------------------

    }
}
//=============================================================================== End of File =============================================================================