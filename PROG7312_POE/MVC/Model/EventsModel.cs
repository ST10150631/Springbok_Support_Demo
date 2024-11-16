using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace PROG7312_POE.MVC.Model
{
    public class EventsModel
    {
       public SortedDictionary<string, string> EventDetails;
        public int RelevenceScore { get; set; }
    // public string eventName { get; set; }
    //public string eventDescription { get; set; }
    //public string eventStartDate { get; set; }
    // public string eventEndDate { get; set; }
    //public string eventStartTime { get; set; }
    //public string eventEndTime { get; set; }
    //public string eventVenue { get; set; }
    // public string status { get; set; }
    public Dictionary<string, MediaModel> eventMedia;
    public MediaModel eventLogo { get; set; }
    private Stack<BitmapImage> previousMedia = new Stack<BitmapImage>();
    private Stack<BitmapImage> nextMedia = new Stack<BitmapImage>();

        /// <summary>
        //public string eventOrganizer { get; set; }
        /// </summary>
        public EventsModel()
        {
            EventDetails = new SortedDictionary<string, string>();
        }
        public void addDataToEvent(string key, string value)
        {
            EventDetails.Add(key, value);
        }

            public string Name
            {
                get => EventDetails.TryGetValue("Name", out string name) ? name : "Unnamed Event";
            }

            public string Description
            {
                get => EventDetails.TryGetValue("Description", out string description) ? description : "No Description";
            }

            public string Venue
            {
                get => EventDetails.TryGetValue("Venue", out string venue) ? venue : "Unknown Venue";
            }

            public string StartDate
            {
                get => EventDetails.TryGetValue("StartDate", out string startDate) ? startDate : "No Start Date";
            }

            public string EndDate
            {
                get => EventDetails.TryGetValue("EndDate", out string endDate) ? endDate : "No End Date";
            }

            public string StartTime
            {
                get => EventDetails.TryGetValue("StartTime", out string startTime) ? startTime : "No Start Time";
            }

            public string EndTime
            {
                get => EventDetails.TryGetValue("EndTime", out string endTime) ? endTime : "No End Time";
            }

            public string Organizer
            {
                get => EventDetails.TryGetValue("Organizer", out string organizer) ? organizer : "No Organizer";
            }

            public string Category
            {
                get => EventDetails.TryGetValue("Category", out string category) ? category : "Uncategorized";
            }
        

    }
}
