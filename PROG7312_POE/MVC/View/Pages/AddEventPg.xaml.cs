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
using MessageBox = System.Windows.MessageBox;

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
        ObservableCollection<MediaModel> mediaModels = new ObservableCollection<MediaModel>();

        private MediaModel logo;

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

        private void btnAddMedia_Click(object sender, RoutedEventArgs e)
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
                        mediaDict.Add(media.mediaName, media);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error adding media: {ex.Message}", "Error", MessageBoxButton.OK);
                    }
                }
            }
            else
            {
                MessageBox.Show("No file selected.");
            }
        }

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
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error adding media: {ex.Message}", "Error", MessageBoxButton.OK);
                    }
                }
            }
            else
            {
                MessageBox.Show("No file selected.");
            }
        }



        private void btncancel_Click(object sender, RoutedEventArgs e)
        {
            MainWindow parentWindow = Window.GetWindow(this) as MainWindow;
            NewsPage news = new NewsPage();
            parentWindow.RbtnEvents.IsChecked = true;
            parentWindow.ContentPane.Content = news;
        }

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
                    System.Windows.MessageBox.Show("Error Loading image.", "Error", MessageBoxButton.OK);
                    // Handle exceptions (e.g., file not found, access denied)
                    throw new Exception($"Error reading file: {ex.Message}", ex);

                }
            }
            else
            {
                System.Windows.MessageBox.Show("No file selected");
            }
        }

        private bool Validate()
        {
            bool isValid = true;
            if(this.RbtnEvent.IsChecked == true)
            {
                if (string.IsNullOrEmpty(this.EventTxtName.Text))
                {
                    MessageBox.Show("Please enter an event name");
                    EventTxtName.Style = (Style)FindResource("TxtBxInvalid");
                    isValid = false;
                    return isValid;
                }

                if (string.IsNullOrEmpty(this.EventTxtDescription.Text))
                {
                    MessageBox.Show("Please enter a description");
                    EventTxtDescription.Style = (Style)FindResource("TxtBxInvalid");
                    isValid = false;
                    return isValid;
                }
                if (string.IsNullOrEmpty(this.EventTxtVenue.Text))
                {
                    MessageBox.Show("Please enter a location");
                    EventTxtVenue.Style = (Style)FindResource("TxtBxInvalid");
                    isValid = false;
                    return isValid;
                }

                if (EventDatePickerStart.SelectedDate > EventDatePickerEnd.SelectedDate)
                {
                    MessageBox.Show("End Date cannot be first");
                    isValid = false;
                    return isValid;
                }

                
            }
            else
            {
                if(string.IsNullOrEmpty(this.TxtTitle.Text))
                {
                    MessageBox.Show("Please enter an announcement title");
                    TxtTitle.Style = (Style)FindResource("TxtBxInvalid");
                    isValid = false;
                    return isValid;
                }
                if (string.IsNullOrEmpty(this.TxtDescription.Text))
                {
                    MessageBox.Show("Please enter a description");
                    TxtDescription.Style = (Style)FindResource("TxtBxInvalid");
                    isValid = false;
                    return isValid;
                }
                if(string.IsNullOrEmpty(this.TxtAuthor.Text))
                {
                    MessageBox.Show("Please enter an author");
                    TxtAuthor.Style = (Style)FindResource("TxtBxInvalid");
                    isValid = false;
                    return isValid;
                }
                if (string.IsNullOrEmpty(this.TxtLocation.Text))
                {
                    MessageBox.Show("Please enter a Location");
                    TxtLocation.Style = (Style)FindResource("TxtBxInvalid");
                    isValid = false;
                    return isValid;
                }

            }
            return isValid;
        }

        private void btnRemoveMedia_Click(object sender, RoutedEventArgs e)
        {
            mediaDict.Clear();
            this.LblEventMedia.Text = "No Media Selected";
            MessageBox.Show("All media removed.");
        }

        private void btnRemoveLogo_Click(object sender, RoutedEventArgs e)
        {
            logo = null;
            this.LblLogo.Text = "No Logo Selected";
            MessageBox.Show("Logo removed.");
        }


        private void RbtnEvent_Checked(object sender, RoutedEventArgs e)
        {
            this.EventGrd.Visibility = Visibility.Visible;
            this.AnnouncementGrd.Visibility = Visibility.Collapsed;
        }

        private void RbtnAnnouncement_Checked(object sender, RoutedEventArgs e)
        {
            this.AnnouncementGrd.Visibility = Visibility.Visible;
            this.EventGrd.Visibility = Visibility.Collapsed;
            CmboBxEvent.ItemsSource = MainController.newsController.GetEvents();
            CmboBxEvent.DisplayMemberPath = "Name";  // Ensure the event name is displayed
            CmboBxEvent.SelectedValuePath = "Name";
        }
    }
}
