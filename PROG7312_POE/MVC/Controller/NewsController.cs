using PROG7312_POE.MVC.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Media.Imaging;
using MessageBox = System.Windows.Forms.MessageBox;

namespace PROG7312_POE.MVC.Controller
{
    public class NewsController : INotifyPropertyChanged
    {
        /// <summary>
        /// Holds Categries as a hashSet
        /// </summary>
        public HashSet<string> Categories ;
        /// <summary>
        /// Holds categories to be displayed as a list
        /// </summary>
        public ObservableCollection<string> CategoryList { get; set; }
        // List to store event models
        private List<EventsModel> _events;
        /// <summary>
        /// Holds Category Scores
        /// </summary>
        private Dictionary<string,int> _categoryScores;
        /// <summary>
        /// Property to get or set events list, triggers property change notification
        /// </summary>
        public List<EventsModel> Events
        {
            get { return _events; }
            set
            {
                _events = value;
                OnPropertyChanged();// Notify listeners of property change
            }
        }
        /// <summary>
        ///  ObservableCollection to store announcements
        /// </summary>
        private ObservableCollection<AnnouncementModel> _announcements;
        /// <summary>
        /// Property to get or set announcements list, triggers property change notification
        /// </summary>
        public ObservableCollection<AnnouncementModel> Announcements
        {
            get { return _announcements; }
            set
            {
                _announcements = value;
                OnPropertyChanged();// Notify listeners of property change
            }
        }
        /// <summary>
        ///  Event handler for property change
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Constructor to initialize the NewsController
        /// </summary>
        /// >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>> Start of Method >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
        public NewsController()
        {
            Categories = new HashSet<string>();
            _categoryScores = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);
            //Pre populate the categories
            Categories.Add("General".ToUpper());
            Categories.Add("Sport".ToUpper());
            Categories.Add("Entertainment".ToUpper());
            Categories.Add("Business".ToUpper());
            Categories.Add("Community".ToUpper());
            Categories.Add("Arts".ToUpper());
            Categories.Add("Lifestyle".ToUpper());
            Categories.Add("Other".ToUpper());
            CategoryList = new ObservableCollection<string>(Categories);
            Events = new List<EventsModel>();
            Announcements = new ObservableCollection<AnnouncementModel>();
            LoadEvents();
        }
        //------------------------------------------------------------------------ End of Method ------------------------------------------------------------------------------------------

        /// <summary>
        /// Creates a new Category Score
        /// </summary>
        /// <param name="Category"></param>
        /// >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>> Start of Method >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
        public void AddCategoryScore(string category)
        {
            if (!_categoryScores.ContainsKey(category))
            {
                _categoryScores[category] = 0;
                Categories.Add(category);
            }
        }
        //------------------------------------------------------------------------ End of Method ------------------------------------------------------------------------------------------
        /// <summary>
        /// Adds Score to a category
        /// </summary>
        /// <param name="Category"></param>
        /// >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>> Start of Method >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
        public void UpdateCategoryScore(string category)
        {
            if (_categoryScores.ContainsKey(category))
            {
                _categoryScores[category]++;
            }
            else
            {
                AddCategoryScore(category);
                _categoryScores[category]++;
            }
        }
        //------------------------------------------------------------------------ End of Method ------------------------------------------------------------------------------------------


        /// <summary>
        /// Adds a new event to the list if it doesn't already exist.
        /// </summary>
        /// >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>> Start of Method >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
        public void AddEvent(EventsModel newEvent)
        {
            Events.Add(newEvent);
            var category = newEvent.EventDetails["Category"];
            AddCategoryScore(category); // Ensure category exists
            UpdateCategoryScore(category); // Increment score

            Categories.Add(newEvent.EventDetails["Category"]);
            CategoryList = new ObservableCollection<string>(Categories);
            OnPropertyChanged(nameof(Events));

        }
        //------------------------------------------------------------------------ End of Method ------------------------------------------------------------------------------------------
        
        /// <summary>
        /// Searches for an event by its name.
        /// </summary>
        /// >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>> Start of Method >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
        public EventsModel GetEventByName(string eventName)
        {

            var result = Events.FirstOrDefault(e =>
                           e.EventDetails.ContainsKey("Name") );
            return result;
        }
        //------------------------------------------------------------------------ End of Method ------------------------------------------------------------------------------------------

        /// <summary>
        /// Returns a list of all events.
        /// </summary>
        /// <returns></returns>
        /// >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>> Start of Method >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
        public ObservableCollection<EventsModel> GetEvents()
        {
            return new ObservableCollection<EventsModel>(Events);
        }
        //------------------------------------------------------------------------ End of Method ------------------------------------------------------------------------------------------

        /// <summary>
        /// Adds an announcement and links it to a specific event.
        /// </summary
        /// >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>> Start of Method >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
        public void AddAnnouncement(string eventName, AnnouncementModel announcement)
        {
            try
            {
                announcement.CurrentImage = announcement.Media[0].mediaData;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            var associatedEvent = GetEventByName(eventName);
            if (associatedEvent != null)
            {
                UpdateCategoryScore(associatedEvent.EventDetails["Category"]);
                announcement.Event = eventName;
            }

            Announcements.Add(announcement);
            var an = _announcements.OrderByDescending(a => a.Date);
            Announcements = new ObservableCollection<AnnouncementModel>(an);
            OnPropertyChanged(nameof(Announcements));
        }
        //------------------------------------------------------------------------ End of Method ------------------------------------------------------------------------------------------

        /// <summary>
        /// Adds relevence points to an event.
        /// </summary>
        /// <param name="name"></param>
        /// >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>> Start of Method >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
        public void AddPointsToEvent(string name)
        {
            var evnt = GetEventByName(name);
            if (evnt != null)
            {
                var category = evnt.EventDetails["Category"];
                UpdateCategoryScore(category);
                evnt.RelevenceScore += 2;
            }
        }// ------------------------------------------------------------------------ End of Method ------------------------------------------------------------------------------------------

        /// <summary>
        ///  Returns a list of recommended announcements based on the specified category.
        /// </summary>
        /// <param name="category"></param>
        /// <returns></returns>
        /// >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>> Start of Method >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
        public ObservableCollection<AnnouncementModel> GetReccommended()
        {
            // Check if there are any categories with scores
            if (_categoryScores.Count == 0)
            {
                MessageBox.Show("No categories with scores found.");
                return new ObservableCollection<AnnouncementModel>(); // Return empty collection
            }

            // Check if there are any events
            var topCategories = _categoryScores
                .OrderByDescending(cs => cs.Value) // Sort by score, descending
                .Take(3) // Get at most 3 top-scoring categories
                .Select(cs => cs.Key) // Extract category names
                .ToList();

            // Get top events from the top categories
            var topEvents = Events
                .Where(e => topCategories.Contains(e.EventDetails["Category"], StringComparer.OrdinalIgnoreCase))
                .OrderByDescending(e => e.RelevenceScore) // Sort by relevance score
                .Take(3) // Get at most 3 events
                .Select(e => e.EventDetails["Name"]) // Extract event names
                .ToList();
            // if no events are found
            if (topEvents.Count == 0)
            {
                MessageBox.Show("No relevant events found in the top categories.");
                return new ObservableCollection<AnnouncementModel>(); // Return empty collection
            }

            // get announcements for the top events
            var recommendedAnnouncements = Announcements
                .Where(a => topEvents.Contains(a.Event)) // Filter announcements for the top events
                .OrderByDescending(a => a.Date) // Sort by most recent
                .Take(3) // Get at most 3 announcements
                .ToList();

            // if no announcements are found
            if (recommendedAnnouncements.Count == 0)
            {
                MessageBox.Show("No announcements found for the top events.");
                return new ObservableCollection<AnnouncementModel>(); // Return empty collection
            }

            // return results :)
            return new ObservableCollection<AnnouncementModel>(recommendedAnnouncements);
        }
        // ------------------------------------------------------------------------ End of Method ------------------------------------------------------------------------------------------


        /// <summary>
        /// Gets a list of all announcements.
        /// </summary>
        /// <returns></returns>
        /// >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>> Start of Method >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
        public ObservableCollection<AnnouncementModel> GetAnnouncements()
        {
            return new ObservableCollection<AnnouncementModel>(Announcements);
        }
        //------------------------------------------------------------------------ End of Method ------------------------------------------------------------------------------------------

        /// <summary>
        /// Gets a list of announcements for a specific event.
        /// <summary> 
        /// <returns></returns>
        /// >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>> Start of Method >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
        public ObservableCollection<AnnouncementModel> GetAnnouncementsForEvent(string eventName)
        {
            return new ObservableCollection<AnnouncementModel>(
                Announcements.Where(a => a.Event == eventName));
        }
        //------------------------------------------------------------------------ End of Method ------------------------------------------------------------------------------------------


        /// <summary>
        /// Gets a list of events for a specific category.
        /// </summary>
        /// <param name="category"></param>
        /// <returns></returns>
        /// >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>> Start of Method >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
        public List<EventsModel> GetEventsForCategory(string category)
        {
            var events = Events
               .Where(e => e.EventDetails["Category"].Equals(category, StringComparison.OrdinalIgnoreCase))
               .ToList();

            if (events.Any())
            {
                UpdateCategoryScore(category); // Update score each time events are accessed
            }

            return events;

        }
        //------------------------------------------------------------------------ End of Method ------------------------------------------------------------------------------------------

        /// <summary>
        /// Removes an event and its associated announcements.
        /// </summary>
        /// >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>> Start of Method >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
        public void RemoveEvent(string eventName)
        {
            var eventToRemove = GetEventByName(eventName);
            if (eventToRemove != null)
            {
                Events.Remove(eventToRemove);

                // Remove associated announcements
                var announcementsToRemove = Announcements
                    .Where(a => a.Event == eventName).ToList();

                foreach (var announcement in announcementsToRemove)
                    Announcements.Remove(announcement);

                OnPropertyChanged(nameof(Events));
                OnPropertyChanged(nameof(Announcements));
            }
            else
            {
                MessageBox.Show($"No event found with the name '{eventName}'.");
            }
        }
        //------------------------------------------------------------------------ End of Method ------------------------------------------------------------------------------------------

        /// <summary>
        /// Iterates through the media list of an announcement to display the next image.
        /// </summary>
        /// <param name="announcement"></param>
        /// >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>> Start of Method >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
        public void NextMedia(AnnouncementModel announcement)
        {
            if (announcement.CurrentMediaIndex < announcement.Media.Count - 1)
            {
                announcement.CurrentMediaIndex++;
                announcement.CurrentImage = announcement.Media[announcement.CurrentMediaIndex].mediaData;
                OnPropertyChanged(nameof(Announcements));
            }
        }
        //------------------------------------------------------------------------ End of Method ------------------------------------------------------------------------------------------

        /// <summary>
        /// Iterates through the media list of an announcement to display the previous image.
        /// </summary>
        /// <param name="announcement"></param>
        /// >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>> Start of Method >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
        public void PreviousMedia(AnnouncementModel announcement)
        {
            if (announcement.CurrentMediaIndex > 0)
            {
                announcement.CurrentMediaIndex--;
                announcement.CurrentImage = announcement.Media[announcement.CurrentMediaIndex].mediaData;
                OnPropertyChanged(nameof(Announcements));
            }
        }
        //------------------------------------------------------------------------ End of Method ------------------------------------------------------------------------------------------

        /// <summary>
        /// Loads sample events into the events list.
        /// </summary>
        /// >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>> Start of Method >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
        public void LoadEvents()
        {
            // Create the first event (Annual Conference)
            var annualConference = new EventsModel();
            annualConference.addDataToEvent("Name", "Annual Conference");
            annualConference.addDataToEvent("Description", "An annual gathering to discuss business strategies.");
            annualConference.addDataToEvent("Venue", "Cape Town Convention Centre");
            annualConference.addDataToEvent("StartDate", "2024-10-20");
            annualConference.addDataToEvent("EndDate", "2024-10-21");
            annualConference.addDataToEvent("StartTime", "09:00");
            annualConference.addDataToEvent("EndTime", "17:00");
            annualConference.addDataToEvent("Organizer", "Business South Africa");
            annualConference.addDataToEvent("Category", "Business");

            Events.Add(annualConference);

            // Create the second event (Job Fair Cape Town)
            var jobFair = new EventsModel();
            jobFair.addDataToEvent("Name", "Job Fair Cape Town");
            jobFair.addDataToEvent("Description", "An event to connect job seekers with potential employers.");
            jobFair.addDataToEvent("Venue", "Career Center, Cape Town");
            jobFair.addDataToEvent("StartDate", "2024-11-15");
            jobFair.addDataToEvent("EndDate", "2024-11-15");
            jobFair.addDataToEvent("StartTime", "10:00");
            jobFair.addDataToEvent("EndTime", "15:00");
            jobFair.addDataToEvent("Organizer", "Career Expo SA");
            jobFair.addDataToEvent("Category", "Business");

            Events.Add(jobFair);
            // Create the second event (Job Fair Cape Town)
            var SpringBokTour = new EventsModel();
            SpringBokTour.addDataToEvent("Name", "Springbok Tour");
            SpringBokTour.addDataToEvent("Description", "The much beloved Springboks will be touring SA in a bus");
            SpringBokTour.addDataToEvent("Venue", "Cape Town, PE, Johannesburg, Pretoria, KZN");
            SpringBokTour.addDataToEvent("StartDate", "2024-10-11");
            SpringBokTour.addDataToEvent("EndDate", "2024-11-11");
            SpringBokTour.addDataToEvent("StartTime", "10:00");
            SpringBokTour.addDataToEvent("EndTime", "15:00");
            SpringBokTour.addDataToEvent("Organizer", "National Rugby Assosciation");
            SpringBokTour.addDataToEvent("Category", "Sport");

            Events.Add(SpringBokTour);
            // Create the fourth event (Community Cleanup Drive)
            var cleanupDrive = new EventsModel();
            cleanupDrive.addDataToEvent("Name", "Community Cleanup Drive");
            cleanupDrive.addDataToEvent("Description", "Join hands with the community to clean local parks and streets.");
            cleanupDrive.addDataToEvent("Venue", "Greenwood Park, Cape Town");
            cleanupDrive.addDataToEvent("StartDate", "2024-12-01");
            cleanupDrive.addDataToEvent("EndDate", "2024-12-01");
            cleanupDrive.addDataToEvent("StartTime", "08:00");
            cleanupDrive.addDataToEvent("EndTime", "12:00");
            cleanupDrive.addDataToEvent("Organizer", "Cape Town Community Organization");
            cleanupDrive.addDataToEvent("Category", "Community");

            Events.Add(cleanupDrive);

            // Create the fifth event (Cape Town Music Festival)
            var musicFestival = new EventsModel();
            musicFestival.addDataToEvent("Name", "Cape Town Music Festival");
            musicFestival.addDataToEvent("Description", "A grand celebration of music featuring top artists and bands.");
            musicFestival.addDataToEvent("Venue", "Cape Town Stadium");
            musicFestival.addDataToEvent("StartDate", "2024-12-15");
            musicFestival.addDataToEvent("EndDate", "2024-12-17");
            musicFestival.addDataToEvent("StartTime", "15:00");
            musicFestival.addDataToEvent("EndTime", "22:00");
            musicFestival.addDataToEvent("Organizer", "Cape Entertainment Group");
            musicFestival.addDataToEvent("Category", "Entertainment");

            Events.Add(musicFestival);
            LoadAnnoucements();
        }
        //------------------------------------------------------------------------ End of Method ------------------------------------------------------------------------------------------

        private void LoadAnnoucements()
        {
            ///Announcement Dummy Data
            var SpringbokTourAnnouncement1 = new AnnouncementModel()
            {
                Title = "Support the Springboks on Tour!",
                Description = "Join the nation in celebrating the Springboks as they tour South Africa! Show your support at their stops across major cities. Next they are heading to cape Town !",
                Author = "National Rugby Association",
                Location = "Cape Town CBD and main road", // Matches the Springbok Tour venues
                Website = "www.springboksontour.co.za",
                Media = new ObservableCollection<MediaModel>(),
                Date = DateTime.Now
            };
            AddAnnouncement("Springbok Tour", SpringbokTourAnnouncement1);
            ///Announcement Dummy Data
            var SpringbokTourAnnouncement2 = new AnnouncementModel()
            {
                Title = "Support the Springboks on Tour!",
                Description = "Join the nation in celebrating the Springboks as they tour South Africa! Show your support at their stops across major cities. Next they are heading to PE !\"",
                Author = "National Rugby Association",
                Location = "Port Elizibeth Waterfront", // Matches the Springbok Tour venues
                Website = "www.springboksontour.co.za",
                Media = new ObservableCollection<MediaModel>(),
                Date = DateTime.Now
            };
            AddAnnouncement("Springbok Tour", SpringbokTourAnnouncement2);
            // Announcement Dummy Data
            var musicFestivalAnnouncement1 = new AnnouncementModel()
            {
                Title = "Experience the Best of Music in Cape Town!",
                Description = "Join us at the Cape Town Music Festival for an unforgettable weekend of top-notch performances and entertainment. Next, don't miss the evening concert at the Cape Town Stadium!",
                Author = "Cape Entertainment Group",
                Location = "Cape Town Stadium - Main Arena",
                Website = "www.capetownmusicfestival.co.za",
                Media = new ObservableCollection<MediaModel>(),
                Date = DateTime.Now
            };
            AddAnnouncement("Cape Town Music Festival", musicFestivalAnnouncement1);

            // Announcement Dummy Data
            var musicFestivalAnnouncement2 = new AnnouncementModel()
            {
                Title = "Cape Town Music Festival is Here!",
                Description = "Don't miss the Cape Town Music Festival. Enjoy live performances, food stalls, and fun activities all day! Perfect for friends and family!",
                Author = "Cape Entertainment Group",
                Location = "Cape Town Stadium - Outdoor Stages",
                Website = "www.capetownmusicfestival.co.za",
                Media = new ObservableCollection<MediaModel>(),
                Date = DateTime.Now
            };
            AddAnnouncement("Cape Town Music Festival", musicFestivalAnnouncement2);

            // Announcement Dummy Data
            var musicFestivalAnnouncement3 = new AnnouncementModel()
            {
                Title = "Countdown to the Cape Town Music Festival!",
                Description = "The Cape Town Music Festival is almost here! Plan your weekend and join the celebration. The event starts at 3 PM. Tickets are selling fast!",
                Author = "Cape Entertainment Group",
                Location = "Cape Town Stadium",
                Website = "www.capetownmusicfestival.co.za",
                Media = new ObservableCollection<MediaModel>(),
                Date = DateTime.Now
            };
            AddAnnouncement("Cape Town Music Festival", musicFestivalAnnouncement3);
        }

        /// <summary>
        /// Notifies listeners of property changes.
        /// </summary>
        /// <param name="propertyName"></param>
        /// >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>> Start of Method >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        //------------------------------------------------------------------------ End of Method ------------------------------------------------------------------------------------------
    }
}
//=============================================================================== End of File ============================================================================================