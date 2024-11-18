using PROG7312_POE.MVC.Controller;
using PROG7312_POE.MVC.Model;
using PROG7312_POE.MVVM.View.Pages;
using PROG7312_POE.MVVM.View.Styles;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Media.Imaging;

namespace PROG7312_POE.MVC.View.Pages
{
    /// <summary>
    /// Interaction logic for AddEventPg.xaml
    /// </summary>

    public partial class AddEventPg : Page
    {
        /// <summary>
        /// Dictionary to hold media
        /// </summary>
        Dictionary<string, MediaModel> mediaDict = new Dictionary<string, MediaModel>();
        /// <summary>
        /// Holds media as observable collection
        /// </summary>
        ObservableCollection<MediaModel> mediaModels = new ObservableCollection<MediaModel>();

        /// <summary>
        /// Holds media Image Logo
        /// </summary>
        private MediaModel logo;

        /// <summary>
        /// Constructor for AddEventPg
        /// </summary>
        /// >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>> Start of Method >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
        public AddEventPg()
        {
            InitializeComponent();
            this.RbtnEvent.IsChecked = true;

            this.StartHourComboBox.SelectedIndex = 0;
            this.StartMinuteComboBox.SelectedIndex = 0;

            this.EndHourComboBox.SelectedIndex = 0;
            this.EndMinuteComboBox.SelectedIndex = 0;

            this.EventDatePickerStart.SelectedDate = DateTime.Now;
            this.EventDatePickerEnd.SelectedDate = DateTime.Now;

        }
        // ------------------------------------------------------------------------ End of Method ------------------------------------------------------------------------------------------

        /// <summary>
        /// Event handler for adding media
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>> Start of Method >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
        private void btnAddMedia_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog()
            {
                Filter = "Media Files (*.jpg;*.jpeg;*.gif;*.bmp;*.png;)|" +
                         "*.jpg;*.jpeg;*.gif;*.bmp;*.png;",
                FilterIndex = 1,
                Multiselect = true // Enable multiple file selection
            };

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                foreach (string filePath in openFileDialog.FileNames)
                {
                    try
                    {
                        string fileType = Path.GetExtension(filePath).ToLower();
                        byte[] fileData = File.ReadAllBytes(filePath);

                        BitmapImage bitmapImage = new BitmapImage();
                        using (MemoryStream memoryStream = new MemoryStream(fileData))
                        {
                            bitmapImage.BeginInit();
                            bitmapImage.StreamSource = memoryStream;
                            bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                            bitmapImage.EndInit();
                        }

                        var media = new MediaModel
                        {
                            mediaName = Path.GetFileName(filePath),
                            mediaType = fileType,
                            mediabytes = fileData,
                            mediaData = bitmapImage
                        };
                        mediaDict.Add(media.mediaName, media);
                        LblEventMedia.Text = "Media Added";
                    }
                    catch (Exception)
                    {
                        CustomMessageBox.Show("Error adding media:", "Error");
                    }
                }
            }
            else
            {
                CustomMessageBox.Show("No file selected.", "Error");
            }
        }
        // ------------------------------------------------------------------------ End of Method ------------------------------------------------------------------------------------------


        /// <summary>
        /// Event handler for adding announcement media
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>> Start of Method >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
        private void btnAddAnnouncementMedia_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog()
            {
                Filter = "Media Files (*.jpg;*.jpeg;*.gif;*.bmp;*.png;*.mp4;*.mp3)|" +
                         "*.jpg;*.jpeg;*.gif;*.bmp;*.png;*.mp4;*.mp3",
                FilterIndex = 1,
                Multiselect = true // Enable multiple file selection
            };

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                foreach (string filePath in openFileDialog.FileNames)
                {
                    try
                    {
                        string fileType = Path.GetExtension(filePath).ToLower();
                        byte[] fileData = File.ReadAllBytes(filePath);

                        BitmapImage bitmapImage = new BitmapImage();
                        using (MemoryStream memoryStream = new MemoryStream(fileData))
                        {
                            bitmapImage.BeginInit();
                            bitmapImage.StreamSource = memoryStream;
                            bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                            bitmapImage.EndInit();
                        }

                        var media = new MediaModel
                        {
                            mediaName = Path.GetFileName(filePath),
                            mediaType = fileType,
                            mediabytes = fileData,
                            mediaData = bitmapImage
                        };
                        mediaModels.Add(media);

                    }
                    catch (Exception)
                    {
                        CustomMessageBox.Show("Error adding media:", "Error");
                    }
                }
            }
            else
            {
                CustomMessageBox.Show("No file selected.", "Error");
            }
        }
        // ------------------------------------------------------------------------ End of Method ------------------------------------------------------------------------------------------


        /// <summary>
        /// Navigates back to the news page if cancelled
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>> Start of Method >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
        private void btncancel_Click(object sender, RoutedEventArgs e)
        {
            MainWindow parentWindow = Window.GetWindow(this) as MainWindow;
            NewsPage news = new NewsPage();
            parentWindow.RbtnEvents.IsChecked = true;
            parentWindow.ContentPane.Content = news;
        }
        // ------------------------------------------------------------------------ End of Method ------------------------------------------------------------------------------------------

        /// <summary>
        /// Submits the event or announcement to be saved if validation passes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>> Start of Method >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            if (!Validate())
            {
                return;
            }
            if (RbtnEvent.IsChecked == true)
            {
                EventsModel newEvent = new EventsModel()
                {
                    eventMedia = mediaDict,
                    eventLogo = logo
            };


                newEvent.addDataToEvent("Name", EventTxtName.Text);
                newEvent.addDataToEvent("Description", EventTxtDescription.Text);
                newEvent.addDataToEvent("Venue", EventTxtVenue.Text);
                newEvent.addDataToEvent("StartDate", EventDatePickerStart.SelectedDate.Value.ToShortDateString());
                newEvent.addDataToEvent("EndDate", EventDatePickerEnd.SelectedDate.Value.ToShortDateString());
                newEvent.addDataToEvent("StartTime", StartHourComboBox.Text + ":" + StartMinuteComboBox.Text);
                newEvent.addDataToEvent("EndTime", EndHourComboBox.Text + ":" + EndMinuteComboBox.Text);
                newEvent.addDataToEvent("Organizer", EventTxtOrganizer.Text);
                newEvent.addDataToEvent("Category", CmboBxCategory.Text.ToUpper());
               

                //eventName = this.EventTxtName.Text,
                //eventDescription = this.EventTxtDescription.Text,
                //eventVenue = this.EventTxtVenue.Text,
                //eventStartDate = this.EventDatePickerStart.SelectedDate.Value.ToShortDateString(),
                //eventEndDate = this.EventDatePickerEnd.SelectedDate.Value.ToShortDateString(),
                //eventStartTime = this.StartHourComboBox.Text + ":" + this.StartMinuteComboBox.Text,
                //eventEndTime = this.EndHourComboBox.Text + ":" + this.EndMinuteComboBox.Text,
                //eventOrganizer = this.EventTxtOrganizer.Text,
                //eventCategory = this.CmboBxCategory.Text,
                //eventMedia = mediaDict,
                //eventLogo = logo

                MainController.newsController.AddEvent(newEvent);

            }
            else
            {
                AnnouncementModel newAnnouncement = new AnnouncementModel()
                {
                    Title = this.TxtTitle.Text,
                    Description = this.TxtDescription.Text,
                    Author = this.TxtAuthor.Text,
                    Location = this.TxtLocation.Text,//Event, Website
                    Website = this.TxtWebsite.Text,
                    Media = mediaModels,
                    Date = DateTime.Now




                };
                if (CmboBxEvent.SelectedItem != null)
                {
                    newAnnouncement.Event = CmboBxEvent.Text;
                }
                else
                {
                    newAnnouncement.Event = "General";
                }

                MainController.newsController.AddAnnouncement(newAnnouncement.Event,newAnnouncement);
            }
            MainWindow parentWindow = Window.GetWindow(this) as MainWindow;
            NewsPage news = new NewsPage();
            parentWindow.RbtnEvents.IsChecked = true;
            parentWindow.ContentPane.Content = news;
        }
        // ------------------------------------------------------------------------ End of Method ------------------------------------------------------------------------------------------

        /// <summary>
        /// Adds a logo to the event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <exception cref="Exception"></exception>
        /// >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>> Start of Method >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
        private void btnAddLogo_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog()
            {
                Filter = "Image Files (*.jpg;*.jpeg;*.gif;*.bmp;*.png)|" +
               "*.jpg;*.jpeg;*.gif;*.bmp;*.png",
                FilterIndex = 1
            };
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    string filePath = openFileDialog.FileName;
                    string fileType = Path.GetExtension(filePath).ToLower();

                    // Read the file data into a byte array
                    byte[] fileData = File.ReadAllBytes(filePath);

                    BitmapImage bitmapImage = new BitmapImage();
                    using (MemoryStream memoryStream = new MemoryStream(fileData))
                    {
                        bitmapImage.BeginInit();
                        bitmapImage.StreamSource = memoryStream;
                        bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                        bitmapImage.EndInit();
                    }
                    var Data = bitmapImage;

                    // Return the byte array and file type
                    logo = new MediaModel()
                    {
                        mediaData = Data,
                        mediabytes = fileData,
                        mediaType = fileType,
                        mediaName = Path.GetFileName(filePath)
                    };
                    this.LblLogo.Text = logo.mediaName;
                }
                catch (Exception ex)
                {
                    CustomMessageBox.Show("Error Loading image.", "Error");

                }
            }
            else
            {
                CustomMessageBox.Show("No file selected", "Information");
            }
        }
        // ------------------------------------------------------------------------ End of Method ------------------------------------------------------------------------------------------


        /// <summary>
        ///  Checks if the form is valid and all fields are filled
        /// </summary>
        /// <returns></returns>
        /// >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>> Start of Method >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
        private bool Validate()
        {
            bool isValid = true;
            if(this.RbtnEvent.IsChecked == true)
            {
                if (string.IsNullOrEmpty(this.EventTxtName.Text))
                {
                    CustomMessageBox.Show("Please enter an event name", "Invalid");
                    EventTxtName.Style = (Style)FindResource("TxtBxInvalid");
                    isValid = false;
                    return isValid;
                }

                if (string.IsNullOrEmpty(this.EventTxtDescription.Text))
                {
                    CustomMessageBox.Show("Please enter a description", "Invalid");
                    EventTxtDescription.Style = (Style)FindResource("TxtBxInvalid");
                    isValid = false;
                    return isValid;
                }
                if (string.IsNullOrEmpty(this.EventTxtVenue.Text))
                {
                    CustomMessageBox.Show("Please enter a location", "Invalid");
                    EventTxtVenue.Style = (Style)FindResource("TxtBxInvalid");
                    isValid = false;
                    return isValid;
                }

                if (EventDatePickerStart.SelectedDate > EventDatePickerEnd.SelectedDate)
                {
                    CustomMessageBox.Show("End Date cannot be first", "Invalid");
                    isValid = false;
                    return isValid;
                }

                
            }
            else
            {
                if(string.IsNullOrEmpty(this.TxtTitle.Text))
                {
                    CustomMessageBox.Show("Please enter an announcement title", "Invalid");
                    TxtTitle.Style = (Style)FindResource("TxtBxInvalid");
                    isValid = false;
                    return isValid;
                }
                if (string.IsNullOrEmpty(this.TxtDescription.Text))
                {
                    CustomMessageBox.Show("Please enter a description", "Invalid");
                    TxtDescription.Style = (Style)FindResource("TxtBxInvalid");
                    isValid = false;
                    return isValid;
                }
                if(string.IsNullOrEmpty(this.TxtAuthor.Text))
                {
                    CustomMessageBox.Show("Please enter an author", "Invalid");
                    TxtAuthor.Style = (Style)FindResource("TxtBxInvalid");
                    isValid = false;
                    return isValid;
                }
                if (string.IsNullOrEmpty(this.TxtLocation.Text))
                {
                    CustomMessageBox.Show("Please enter a Location","Invalid");
                    TxtLocation.Style = (Style)FindResource("TxtBxInvalid");
                    isValid = false;
                    return isValid;
                }

            }
            return isValid;
        }
        // ------------------------------------------------------------------------ End of Method ------------------------------------------------------------------------------------------

        /// <summary>
        /// Removes media from the event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>> Start of Method >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
        private void btnRemoveMedia_Click(object sender, RoutedEventArgs e)
        {
            mediaDict.Clear();
            this.LblEventMedia.Text = "No Media Selected";
            CustomMessageBox.Show("Media Removed", "Information");
        }
        // ------------------------------------------------------------------------ End of Method ------------------------------------------------------------------------------------------

        /// <summary>
        /// Removes Logo from the event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>> Start of Method >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
        private void btnRemoveLogo_Click(object sender, RoutedEventArgs e)
        {
            logo = null;
            this.LblLogo.Text = "No Logo Selected";
            CustomMessageBox.Show("Logo Removed", "Information");
        }
        // ------------------------------------------------------------------------ End of Method ------------------------------------------------------------------------------------------

        /// <summary>
        /// Changes the visibility of the event and announcement grids
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>> Start of Method >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
        private void RbtnEvent_Checked(object sender, RoutedEventArgs e)
        {
            this.EventGrd.Visibility = Visibility.Visible;
            this.AnnouncementGrd.Visibility = Visibility.Collapsed;
        }
        // ------------------------------------------------------------------------ End of Method ------------------------------------------------------------------------------------------

        /// <summary>
        /// Changes the visibility of the event and announcement grids
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>> Start of Method >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
        private void RbtnAnnouncement_Checked(object sender, RoutedEventArgs e)
        {
            this.AnnouncementGrd.Visibility = Visibility.Visible;
            this.EventGrd.Visibility = Visibility.Collapsed;
            CmboBxEvent.ItemsSource = MainController.newsController.GetEvents();
            CmboBxEvent.DisplayMemberPath = "Name";  // Ensure the event name is displayed
            CmboBxEvent.SelectedValuePath = "Name";
        }
        // ------------------------------------------------------------------------ End of Method ------------------------------------------------------------------------------------------
    }
}
//=============================================================================== End of File =============================================================================