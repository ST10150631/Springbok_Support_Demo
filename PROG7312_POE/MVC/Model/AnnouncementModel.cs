using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace PROG7312_POE.MVC.Model
{
    public class AnnouncementModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Author { get; set; }
        public string Location { get; set; }
        public DateTime Date { get; set; }
        public ObservableCollection<MediaModel> Media { get; set; }
        public BitmapImage CurrentImage { get; set; }
        public int CurrentMediaIndex { get; set; }
        public string Event { get; set; }
        public string Website { get; set; }

        public AnnouncementModel()
        {

        }
    }
}
