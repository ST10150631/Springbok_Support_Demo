using PROG7312_POE.MVC.Controller;
using PROG7312_POE.MVC.Model;
using PROG7312_POE.MVC.View.Pages;
using PROG7312_POE.MVVM.View.Styles;
using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;

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
            OpenFileDialog openFileDialog = new OpenFileDialog()
            {
                Filter = "Media Files (*.pdf;*.jpg;*.jpeg;*.gif;*.bmp;*.png;*.mp4;*.avi)|*.pdf;*.jpg;*.jpeg;*.gif;*.bmp;*.png;*.mp4;*.avi",
                FilterIndex = 1,
                Multiselect = true // Allow selecting multiple files
            };

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                foreach (string filePath in openFileDialog.FileNames)
                {
                    try
                    {
                        string fileType = Path.GetExtension(filePath).ToLower();
                        byte[] fileData = File.ReadAllBytes(filePath);

                        // Create a MediaNode for this file
                        var mediaNode = new MediaNode
                        {
                            Name = Path.GetFileName(filePath), // File name as the node name
                            MediaData = fileData,
                            MediaType = fileType,
                            FilePath = filePath
                        };

                        if (fileType == ".pdf")
                        {
                            model.AddPdf(mediaNode);
                        }
                        else if (fileType == ".mp4")
                        {
                            string videoPath =System.IO.Path.GetTempFileName()+".mp4";
                            File.WriteAllBytes(videoPath, fileData);
                            mediaNode.VideoUri = new Uri(videoPath, UriKind.RelativeOrAbsolute);
                            model.AddVideo(mediaNode);
                        }
                        else 
                        {
                            BitmapImage bitmapImage = new BitmapImage();
                            using (MemoryStream memoryStream = new MemoryStream(fileData))
                            {
                                bitmapImage.BeginInit();
                                bitmapImage.StreamSource = memoryStream;
                                bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                                bitmapImage.EndInit();
                            }
                            mediaNode.Image = bitmapImage;

                            model.AddImage(mediaNode);
                        }
                        MediaNameTxt.Text += Path.GetFileName(filePath) + ", ";
                    }
                    catch (Exception ex)
                    {
                        System.Windows.MessageBox.Show($"Error loading file: {filePath}", "Error", MessageBoxButton.OK);
                    }
                }
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
            
            if (currentStep == 1)
            {
                if (BtnNext.Opacity == 0.5)
                {
                    SiyaCntrl.SiyaTxtBg.Background = Brushes.Red;
                    SiyaCntrl.SiyaTxt.Text = "  Please enter a location to proceed.";
                }
                else
                {
                    SiyaCntrl.SiyaTxtBg.Background = this.FindResource("MenuBrush") as Brush;
                    animateForward(StckPnlLocation, StckPnlCategory);
                    model.ReportLocation = LocationTextBox.Text;
                    currentStep++;
                    StckPnlLocation.Visibility = Visibility.Hidden;
                    this.BtnPrevious.Visibility = Visibility.Visible;
                    this.ProgressBar.Value = 25;
                    this.SiyaCntrl.SiyaTxt.Text = "  Great! Now let's categorize your issue:";

                    BtnNext.Opacity = 0.5;
                }

            }
            else if (currentStep == 2)
            {
                if (BtnNext.Opacity == 0.5)
                {
                    SiyaCntrl.SiyaTxtBg.Background = Brushes.Red;
                    SiyaCntrl.SiyaTxt.Text = "  Please select a category to proceed";
                }
                else
                {
                    SiyaCntrl.SiyaTxtBg.Background = this.FindResource("MenuBrush") as Brush;
                    model.ReportType = CategoryComboBox.SelectionBoxItem.ToString();
                    animateForward(StckPnlCategory, StckPnlDescription);
                    currentStep++;
                    StckPnlCategory.Visibility = Visibility.Hidden;
                    this.ProgressBar.Value = 50;
                    this.SiyaCntrl.SiyaTxt.Text = " Almost there... \nCan you please offer a description of the issue.";

                    BtnNext.Opacity = 0.5;
                }

            }
            else if (currentStep == 3)
            {
                if (BtnNext.Opacity == 0.5)
                {
                    SiyaCntrl.SiyaTxtBg.Background = Brushes.Red;
                    SiyaCntrl.SiyaTxt.Text = "  Please give a description of the issue to proceed.";
                }
                else
                {
                    SiyaCntrl.SiyaTxtBg.Background = this.FindResource("MenuBrush") as Brush;
                    TextRange TxtRange = new TextRange(DescriptionRichTextBox.Document.ContentStart, DescriptionRichTextBox.Document.ContentEnd);
                    model.ReportDescription = TxtRange.Text;
                    animateForward(StckPnlDescription, StckPnlMedia);
                    currentStep++;
                    StckPnlDescription.Visibility = Visibility.Collapsed;
                    this.ProgressBar.Value = 75;
                    this.SiyaCntrl.SiyaTxt.Text = "We're Right there all we need is a photo or PDF from you and we're Done!. *Optional*";
                    BtnNext.IsEnabled = true;
                    this.BtnNext.Content = "Submit";
                }

            }
            else if (currentStep == 4)
            {
                model.ReportStatus = "Submitted";
                MainController.reportController.AddReport(model);
                this.SiyaCntrl.SiyaTxt.Text = "  Thank you for submitting your report. \n  You will be redirected to the home page shortly.";
                this.ProgressBar.Value = 100;
                StckPnlSubmit.Visibility = Visibility.Visible;
                StckPnlMedia.Visibility = Visibility.Collapsed;
                BtnPrevious.Visibility = Visibility.Hidden;
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
                SiyaCntrl.SiyaTxtBg.Background = this.FindResource("MenuBrush") as Brush;
                animateBack(StckPnlCategory, StckPnlLocation);
                currentStep--;
                StckPnlCategory.Visibility = Visibility.Collapsed;
                this.BtnPrevious.Visibility = Visibility.Hidden;
                this.ProgressBar.Value = 0;
                this.SiyaCntrl.SiyaTxt.Text = "  Let's take another look at that location and make sure everything is right.";
            }
            else if (currentStep == 3)
            {
                SiyaCntrl.SiyaTxtBg.Background = this.FindResource("MenuBrush") as Brush;
                animateBack(StckPnlDescription, StckPnlCategory);
                currentStep--;
                StckPnlDescription.Visibility = Visibility.Collapsed;
                this.ProgressBar.Value = 25;
                this.SiyaCntrl.SiyaTxt.Text = "  Select the wrong category?\nNo problem! Let's change that quick.";
                
            }
            else if (currentStep == 4)
            {
                SiyaCntrl.SiyaTxtBg.Background = this.FindResource("MenuBrush") as Brush;
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
                BtnNext.Opacity = 0.5;
            }
            else
            {
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
            TextRange TxtRange = new TextRange(DescriptionRichTextBox.Document.ContentStart, DescriptionRichTextBox.Document.ContentEnd);
            string text = TxtRange.Text.Trim();
            if (string.IsNullOrEmpty(text))
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