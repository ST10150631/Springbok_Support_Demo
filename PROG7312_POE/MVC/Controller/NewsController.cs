using PROG7312_POE.MVC.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using MessageBox = System.Windows.Forms.MessageBox;

namespace PROG7312_POE.MVC.Controller
{
    public class NewsController : INotifyPropertyChanged
    {
        /// <summary>
        /// Holds Categries as a hashSet
        /// </summary>
        public HashSet<string> Categories ;
        // List to store event models
        private List<EventsModel> _events;

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
            jobFair.addDataToEvent("Category", "Job Fair");

            Events.Add(jobFair);
        }
        //------------------------------------------------------------------------ End of Method ------------------------------------------------------------------------------------------

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