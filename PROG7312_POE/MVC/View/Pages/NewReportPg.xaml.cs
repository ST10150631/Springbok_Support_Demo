using PROG7312_POE.MVC.Controller;
using PROG7312_POE.MVC.Model;
using PROG7312_POE.MVC.View.Pages;
using PROG7312_POE.MVVM.View.Styles;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using static System.Net.Mime.MediaTypeNames;

namespace PROG7312_POE.MVVM.View.Pages
{
    /// <summary>
    /// Interaction logic for NewReportPg.xaml
    /// </summary>
    public partial class NewReportPg : Page
    {
        /// <summary>
        /// Instance of the Storyboard class to animate the transition between steps
        /// </summary>
        private Storyboard storyboard;
        /// <summary>
        /// The current step in the report creation process
        /// </summary>
        private int currentStep = 1;

        Byte[] MediaData;
        string MediaType;
        string FilePath;

        ReportModel model;

        /// <summary>
        /// default constructor for the NewReportPg class
        /// </summary>
        /// >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>> Start of Method >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
        public NewReportPg()
        {
            storyboard = new Storyboard();
            model = new ReportModel();
            InitializeComponent();
            BtnNext.IsEnabled = false;
            BtnNext.Opacity = 0.5;
            this.SiyaCntrl.SiyaTxt.Text = "  Hi there!\r\n  Welcome to the Springbok Support System.\r\n  My name is Siya the Springbok and I am here to help.\r\n  Let's start with the location of your issue:";
            
        }
        //------------------------------------------------------------------------ End of Method ------------------------------------------------------------------------------------------

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>> Start of Method >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
        private void BtnAttachMedia_Click(object sender, RoutedEventArgs e)
        {
          (MediaData,MediaType,FilePath) =  MainController.reportController.UploadMedia();
            this.MediaNameTxt.Text = FilePath;
            model.ReportMedia = MediaData;
            model.MediaType = MediaType;
            if (MediaType != ".pdf" && MediaType != null)
            {
                BitmapImage bitmapImage = new BitmapImage();
                using (MemoryStream memoryStream = new MemoryStream(MediaData))
                {
                    bitmapImage.BeginInit();
                    bitmapImage.StreamSource = memoryStream;
                    bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                    bitmapImage.EndInit();
                }
                model.image = bitmapImage;

            }
        }
        //------------------------------------------------------------------------ End of Method ------------------------------------------------------------------------------------------

        /// <summary>
        /// Checks the current step in the report creation process and animates the transition to the next step
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>> Start of Method >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
        private void BtnNext_Click(object sender, RoutedEventArgs e)
        {
            BtnNext.IsEnabled = false;
            BtnNext.Opacity = 0.5;
            if (currentStep == 1)
            {
                animateForward(StckPnlLocation, StckPnlCategory);
                model.ReportLocation = LocationTextBox.Text;
                currentStep++;
                StckPnlLocation.Visibility = Visibility.Hidden;
                this.BtnPrevious.Visibility = Visibility.Visible;
                this.ProgressBar.Value = 25;
                this.SiyaCntrl.SiyaTxt.Text = "  Great! Now let's categorize your issue:";
            }
            else if (currentStep == 2)
            {
                model.ReportType = CategoryComboBox.SelectionBoxItem.ToString();
                animateForward(StckPnlCategory, StckPnlDescription);
                currentStep++;
                StckPnlCategory.Visibility = Visibility.Hidden;
                this.ProgressBar.Value = 50;
                this.SiyaCntrl.SiyaTxt.Text = " Almost there... \nCan you please offer a description of the issue.";
            }
            else if (currentStep == 3)
            {
                TextRange TxtRange = new TextRange(DescriptionRichTextBox.Document.ContentStart, DescriptionRichTextBox.Document.ContentEnd);
                model.ReportDescription = TxtRange.Text;
                animateForward(StckPnlDescription, StckPnlMedia);
                currentStep++;
                StckPnlDescription.Visibility = Visibility.Collapsed;
                this.ProgressBar.Value = 75;
                this.SiyaCntrl.SiyaTxt.Text = "We're Right there all we need is a photo from you and we're Done!.";
                BtnNext.IsEnabled = true;
                this.BtnNext.Content = "Submit";
            }
            else if (currentStep == 4)
            {
                MainController.reportController.AddReport(model);
                this.SiyaCntrl.SiyaTxt.Text = "  Thank you for submitting your report. \n  You will be redirected to the home page shortly.";
                this.ProgressBar.Value = 100;
                StckPnlSubmit.Visibility = Visibility.Visible;
                StckPnlMedia.Visibility = Visibility.Collapsed;
                this.BtnNext.Visibility = Visibility.Hidden;

            };



        }
        //------------------------------------------------------------------------ End of Method ------------------------------------------------------------------------------------------


        /// <summary>
        /// Animates the transition from one step to the next
        /// </summary>
        /// <param name="target"></param>
        /// <param name="replacement"></param>
        /// >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>> Start of Method >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
        private void animateForward(StackPanel target, StackPanel replacement)
        {
            // Create DoubleAnimation for StckPnlLocation
            DoubleAnimation targetAnimation = new DoubleAnimation
            {
                From = 0,
                To = -600,
                Duration = new Duration(TimeSpan.FromSeconds(0.5))
            };
            // Assign target and property
            Storyboard.SetTarget(targetAnimation, target);
            Storyboard.SetTargetProperty(targetAnimation, new PropertyPath("(Canvas.Left)"));
            storyboard.Children.Add(targetAnimation);

            // Create DoubleAnimation for StckPnlCategory
            DoubleAnimation replacementAnimation = new DoubleAnimation
            {
                From = 600,
                To = 0,
                Duration = new Duration(TimeSpan.FromSeconds(0.5)),
            };
            // Assign target and property
            Storyboard.SetTarget(replacementAnimation, replacement);
            Storyboard.SetTargetProperty(replacementAnimation, new PropertyPath("(Canvas.Left)"));
            storyboard.Children.Add(replacementAnimation);

            replacement.Visibility = Visibility.Visible;
            storyboard.Begin();
        }
        //------------------------------------------------------------------------ End of Method ------------------------------------------------------------------------------------------

        /// <summary>
        ///  Checks the current step in the report creation process and animates the transition to the previous step
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>> Start of Method >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
        private void BtnPrevious_Click(object sender, RoutedEventArgs e)
        {
            BtnNext.IsEnabled = true;
            BtnNext.Opacity = 1;
            if (currentStep == 2)
            {
                animateBack(StckPnlCategory, StckPnlLocation);
                currentStep--;
                StckPnlCategory.Visibility = Visibility.Collapsed;
                this.BtnPrevious.Visibility = Visibility.Hidden;
                this.ProgressBar.Value = 0;
                this.SiyaCntrl.SiyaTxt.Text = "  Let's take another look at that location and make sure everything is right.";
            }
            else if (currentStep == 3)
            {
                animateBack(StckPnlDescription, StckPnlCategory);
                currentStep--;
                StckPnlDescription.Visibility = Visibility.Collapsed;
                this.ProgressBar.Value = 25;
                this.SiyaCntrl.SiyaTxt.Text = "  Select the wrong category?\nNo problem! Let's change that quick.";
                
            }
            else if (currentStep == 4)
            {
                animateBack(StckPnlMedia, StckPnlDescription);
                currentStep--;
                StckPnlMedia.Visibility = Visibility.Collapsed;
                this.ProgressBar.Value = 50;
                this.BtnNext.Content = "Next";
                this.SiyaCntrl.SiyaTxt.Text = " Oops let's adjust the issue description.";
            }
        }
        //------------------------------------------------------------------------ End of Method ------------------------------------------------------------------------------------------

        /// <summary>
        /// Animates the transition from one step to the previous step
        /// </summary>
        /// <param name="target"></param>
        /// <param name="replacement"></param>
        /// >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>> Start of Method >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
        private void animateBack(StackPanel target, StackPanel replacement)
        {
            // Create DoubleAnimation for the target panel to move out to the right
            DoubleAnimation targetAnimation = new DoubleAnimation
            {
                From = 0,
                To = 600, // Adjust this value based on your needs
                Duration = new Duration(TimeSpan.FromSeconds(0.5))
            };
            // Assign target and property
            Storyboard.SetTarget(targetAnimation, target);
            Storyboard.SetTargetProperty(targetAnimation, new PropertyPath("(Canvas.Left)"));
            storyboard.Children.Add(targetAnimation);

            // Create DoubleAnimation for the replacement panel to move into view from the right
            DoubleAnimation replacementAnimation = new DoubleAnimation
            {
                From = 600, // Adjust this value based on your needs
                To = 0,
                Duration = new Duration(TimeSpan.FromSeconds(0.5)),
            };
            // Assign target and property
            Storyboard.SetTarget(replacementAnimation, replacement);
            Storyboard.SetTargetProperty(replacementAnimation, new PropertyPath("(Canvas.Left)"));
            storyboard.Children.Add(replacementAnimation);

            // Set the replacement panel to be visible
            replacement.Visibility = Visibility.Visible;

            // Start the animation
            storyboard.Begin();

        }
        //------------------------------------------------------------------------ End of Method ------------------------------------------------------------------------------------------


        /// <summary>
        /// Change button based on the text in the location textbox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>> Start of Method >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
        private void LocationTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (LocationTextBox.Text == "")
            {
                BtnNext.IsEnabled = false;
                BtnNext.Opacity = 0.5;
            }
            else
            {
                BtnNext.IsEnabled = true;
                BtnNext.Opacity = 1;
            }
        }
        //------------------------------------------------------------------------ End of Method ------------------------------------------------------------------------------------------

        /// <summary>
        ///  Disables Button based on selected index 
        ///  Side note more can be added just for now these are the categories
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>> Start of Method >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
        private void CategoryComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CategoryComboBox.SelectedIndex == -1)
            {
                BtnNext.IsEnabled = false;
                BtnNext.Opacity = 0.5;
            }
            else
            {
                BtnNext.IsEnabled = true;
                BtnNext.Opacity = 1;
            }
        }
        //------------------------------------------------------------------------ End of Method ------------------------------------------------------------------------------------------

        /// <summary>
        /// Disables next Button baed on rich textbox content
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>> Start of Method >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
        private void DescriptionRichTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (LocationTextBox.Text == "")
            {
                BtnNext.IsEnabled = false;
                BtnNext.Opacity = 0.5;
            }
            else
            {
                BtnNext.IsEnabled = true;
                BtnNext.Opacity = 1;
            }
        }
//------------------------------------------------------------------------ End of Method ------------------------------------------------------------------------------------------

        /// <summary>
        /// Navigates to view reports page when the button is clicked 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>> Start of Method >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
        private void BtnViewReport_Click(object sender, RoutedEventArgs e)
        {
            MainWindow parentWindow = Window.GetWindow(this) as MainWindow;
            ViewReportPg viewReportPg = new ViewReportPg();
            parentWindow.RbtnHome.IsChecked = false;
            parentWindow.ContentPane.Content = viewReportPg;
        }
        //------------------------------------------------------------------------ End of Method ------------------------------------------------------------------------------------------

        /// <summary>
        /// Navigates to the home page when the button is clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>> Start of Method >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
        private void BtnHome_Click(object sender, RoutedEventArgs e)
        {
            MainWindow parentWindow = Window.GetWindow(this) as MainWindow;
            HomePg homePg = new HomePg();
            parentWindow.RbtnHome.IsChecked = true;
            parentWindow.ContentPane.Content = homePg;
        }
//------------------------------------------------------------------------ End of Method ------------------------------------------------------------------------------------------
    }
}
//=============================================================================== End of File =============================================================================