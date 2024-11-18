using PROG7312_POE.MVC.Controller;
using PROG7312_POE.MVC.Model;
using PROG7312_POE.MVVM.View.Pages;
using PROG7312_POE.MVVM.View.Styles;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PROG7312_POE.MVC.View.Pages
{
    /// <summary>
    /// Interaction logic for NewsPage.xaml
    /// </summary>
    public partial class NewsPage : Page
    {
        private EventsModel SelectedEvent;
        private Stack<BitmapImage> previousMedia = new Stack<BitmapImage>();
        private Stack<BitmapImage> nextMedia = new Stack<BitmapImage>();



        /// <summary>
        /// Constructor for the NewsPage
        /// </summary>
        /// >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>> Start of Method >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
        public NewsPage()
        {
            InitializeComponent();
            ItemsCntrlEvents.Items.Clear();
            ItemsCntrlEvents.DataContext = MainController.newsController;
            ItemsCntrlEvents.ItemsSource = MainController.newsController.GetEvents();
            AnnouncementsItemsControl.Items.Clear();
            AnnouncementsItemsControl.DataContext = MainController.newsController;
            AnnouncementsItemsControl.ItemsSource = MainController.newsController.GetAnnouncements();
        }
        //------------------------------------------------------------------------ End of Method ------------------------------------------------------------------------------------------

        /// <summary>
        ///  Navigate to the AddEvent Page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>> Start of Method >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
        private void btnCreatePost_Click(object sender, RoutedEventArgs e)
        {
            MainWindow parentWindow = Window.GetWindow(this) as MainWindow;
            AddEventPg addEventPg = new AddEventPg();
            parentWindow.RbtnEvents.IsChecked = true;
            parentWindow.ContentPane.Content = addEventPg;
        }
        //------------------------------------------------------------------------ End of Method ------------------------------------------------------------------------------------------

        /// <summary>
        /// Will display the Event Details of the selected Event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>> Start of Method >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
        private void BtnViewEvent_Click(object sender, RoutedEventArgs e)
        {
            this.EventDetails.Visibility = Visibility.Visible;
            var button = sender as Button;
            if (button == null) return;

            SelectedEvent = button.DataContext as EventsModel;

            if (SelectedEvent == null) return;

            MainController.newsController.AddCategoryScore(SelectedEvent.Name);

            // Display event details
            this.TxtEventName.Text = SelectedEvent.EventDetails.TryGetValue("Name", out string eventName) ? eventName : "No Name";
            this.TxtDescription.Text = SelectedEvent.EventDetails.TryGetValue("Description", out string description) ? description : "No Description";
            this.TxtStartDate.Text = SelectedEvent.EventDetails.TryGetValue("StartTime", out string startTime) &&
                                    SelectedEvent.EventDetails.TryGetValue("StartDate", out string startDate)
                ? $"{startTime} {startDate}"
                : "No Start Date";
            this.TxtEndDate.Text = SelectedEvent.EventDetails.TryGetValue("EndTime", out string endTime) &&
                                  SelectedEvent.EventDetails.TryGetValue("EndDate", out string endDate)
                ? $"{endTime} {endDate}"
                : "No End Date";
            this.TxtVenue.Text = SelectedEvent.EventDetails.TryGetValue("Venue", out string venue) ? venue : "No Venue";
            this.TxtOrganizer.Text = SelectedEvent.EventDetails.TryGetValue("Organizer", out string organizer) ? organizer : "No Organizer";
            TxtCategory.Text = SelectedEvent.EventDetails.TryGetValue("Category", out string category) ? category : "General";


            // Set event logo
            if (SelectedEvent.eventLogo != null)
            {
                this.LogoImg.Source = SelectedEvent.eventLogo.mediaData;
            }
            else
            {
                this.LogoImg.Source = null;
            }

            // Clear previous stacks
            previousMedia.Clear();
            nextMedia.Clear();

            // Populate nextMedia stack with event media
            if (SelectedEvent.eventMedia != null)
            {
                foreach (var media in SelectedEvent.eventMedia.Values)
                {
                    nextMedia.Push(media.mediaData);
                }

                // Display the first media item
                if (nextMedia.Count > 0)
                {
                    var currentMedia = nextMedia.Pop();
                    previousMedia.Push(currentMedia);
                    this.MediaDisplay.Source = currentMedia;
                }
            }
            else
            {
                MediaDisplay.Source = null;
            }
            var enentToSearch = SelectedEvent.EventDetails.TryGetValue("Name", out string result) ? eventName : "No Name";
            var posts = MainController.newsController.GetAnnouncementsForEvent(enentToSearch);
            this.EventDetails.DataContext = SelectedEvent;
            this.AnnouncementsItemsControl.DataContext = posts;
            this.AnnouncementsItemsControl.ItemsSource = posts;
        }
        //------------------------------------------------------------------------ End of Method ------------------------------------------------------------------------------------------

        /// <summary>
        ///  Will iterate through Next Media Stack and display the next media
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>> Start of Method >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
        private void BtnNextMedia_Click(object sender, RoutedEventArgs e)
        {
            if (nextMedia.Count > 0)
            {
                var nextImage = nextMedia.Pop();
                previousMedia.Push(nextImage);
                MediaDisplay.Source = nextImage;
            }
            else
            {
                MessageBox.Show("No more media.");
            }
        }
        //------------------------------------------------------------------------ End of Method ------------------------------------------------------------------------------------------

        /// <summary>
        ///  Will Iterate through Previous Media Stack and display the previous media
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>> Start of Method >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
        private void BtnBackMedia_Click(object sender, RoutedEventArgs e)
        {
            if (previousMedia.Count > 1) // Keep at least one item in previous stack
            {
                var lastImage = previousMedia.Pop();
                nextMedia.Push(lastImage);
                MediaDisplay.Source = previousMedia.Peek(); // Show previous media
            }
            else
            {
                MessageBox.Show("No more previous media.");
            }
        }
        //------------------------------------------------------------------------ End of Method ------------------------------------------------------------------------------------------

        /// <summary>
        /// Iterates through the Next Media Stack and displays the next media
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>> Start of Method >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
        private void BtnNextAnnouncementMedia_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            if (button == null) return;

            // Get the DataContext of the Button
            var announcement = button.DataContext as AnnouncementModel;
            if (announcement == null) return;

                MainController.newsController.NextMedia(announcement);
                AnnouncementsItemsControl.DataContext = MainController.newsController;
                AnnouncementsItemsControl.ItemsSource = MainController.newsController.GetAnnouncements();
            
        }
        // ------------------------------------------------------------------------ End of Method ------------------------------------------------------------------------------------------

        /// <summary>
        /// Iterates through the Previous Media Stack and displays the previous media
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>> Start of Method >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
        private void BtnBackAnnouncementMedia_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            if (button == null) return;

            // Get the DataContext of the Button
            var announcement = button.DataContext as AnnouncementModel;
            if (announcement == null) return;

            MainController.newsController.PreviousMedia(announcement);
            AnnouncementsItemsControl.DataContext = MainController.newsController;
            AnnouncementsItemsControl.ItemsSource = MainController.newsController.GetAnnouncements();
        }
        //------------------------------------------------------------------------ End of Method ------------------------------------------------------------------------------------------

        /// <summary>
        ///  Changes the visibility of the EventDetails Grid to Collapsed and will display all Posts
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>> Start of Method >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
        private void btnAllEvents_Click(object sender, RoutedEventArgs e)
        {
            this.EventDetails.Visibility = Visibility.Collapsed;
            AnnouncementsItemsControl.ItemsSource = MainController.newsController.GetAnnouncements();
        }
        // ------------------------------------------------------------------------ End of Method ------------------------------------------------------------------------------------------

        /// <summary>
        /// Displays all the Events in the EventsItemsControl
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>> Start of Method >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
        private void BtnAll_Click(object sender, RoutedEventArgs e)
        {
            ItemsCntrlEvents.DataContext = MainController.newsController;
            ItemsCntrlEvents.ItemsSource = MainController.newsController.GetEvents();
        }
        // ------------------------------------------------------------------------ End of Method ------------------------------------------------------------------------------------------

        /// <summary>
        ///  Displays all the Events in the EventsItemsControl that are of the Category Sport
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>> Start of Method >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
        private void BtnCatSport_Click(object sender, RoutedEventArgs e)
        {
            ItemsCntrlEvents.DataContext = MainController.newsController;

            ItemsCntrlEvents.ItemsSource = MainController.newsController.GetEventsForCategory("Sport");
        }
        // ------------------------------------------------------------------------ End of Method ------------------------------------------------------------------------------------------

        /// <summary>
        /// Displays all the Events in the EventsItemsControl that are of the Category Entertainment
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>> Start of Method >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
        private void BtnCatEntertainment_Click(object sender, RoutedEventArgs e)
        {
            ItemsCntrlEvents.DataContext = MainController.newsController;
            ItemsCntrlEvents.ItemsSource = MainController.newsController.GetEventsForCategory("Entertainment");
        }
        // ------------------------------------------------------------------------ End of Method ------------------------------------------------------------------------------------------

        /// <summary>
        ///  Displays all the Events in the EventsItemsControl that are of the Category Community
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>> Start of Method >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
        private void BtnCommunity_Click(object sender, RoutedEventArgs e)
        {
            ItemsCntrlEvents.DataContext = MainController.newsController;
            ItemsCntrlEvents.ItemsSource = MainController.newsController.GetEventsForCategory("Community");
        }
        // ------------------------------------------------------------------------ End of Method ------------------------------------------------------------------------------------------
        /// <summary>
        ///  Displays all the Events in the EventsItemsControl that are of the Category Business
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>> Start of Method >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
        private void BtnBusiness_Click(object sender, RoutedEventArgs e)
        {
            ItemsCntrlEvents.DataContext = MainController.newsController;
            ItemsCntrlEvents.ItemsSource = MainController.newsController.GetEventsForCategory("Business");
        }
        // ------------------------------------------------------------------------ End of Method ------------------------------------------------------------------------------------------

        /// <summary>
        /// Displays all the Events in the EventsItemsControl that are of the Category Art
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>> Start of Method >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
        private void BtnCatArt_Click(object sender, RoutedEventArgs e)
        {
            ItemsCntrlEvents.DataContext = MainController.newsController;
            ItemsCntrlEvents.ItemsSource = MainController.newsController.GetEventsForCategory("Art");

        }
        // ------------------------------------------------------------------------ End of Method ------------------------------------------------------------------------------------------
        /// <summary>
        /// Displays all the Events in the EventsItemsControl that are of the Category Lifestyle
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>> Start of Method >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
        private void BtnCatLifestyle_Click(object sender, RoutedEventArgs e)
        {
            ItemsCntrlEvents.DataContext = MainController.newsController;
            ItemsCntrlEvents.ItemsSource = MainController.newsController.GetEventsForCategory("Lifestyle");
        }
        //------------------------------------------------------------------------ End of Method ------------------------------------------------------------------------------------------
        /// <summary>
        /// Displays all the Events in the EventsItemsControl that are of the Category General
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>> Start of Method >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
        private void btnRecomendedEvents_Click(object sender, RoutedEventArgs e)
        {
            this.EventDetails.Visibility = Visibility.Collapsed;
            AnnouncementsItemsControl.DataContext = MainController.newsController;

            AnnouncementsItemsControl.ItemsSource = MainController.newsController.GetReccommended();
        }
        //------------------------------------------------------------------------ End of Method ------------------------------------------------------------------------------------------


    }
}
//=============================================================================== End of File =============================================================================