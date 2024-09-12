using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace PROG7312_POE.MVC.Model
{
    public class ReportModel
    {
        public int ID { get; set; }
        public string ReportDescription { get; set; }
        public string ReportType { get; set; }
        public DateTime ReportDate { get; set; }
        public string ReportLocation { get; set; }
        public string ReportStatus { get; set; }
        public Byte[] ReportMedia { get; set; }
        public string MediaType { get; set;}
        public BitmapImage image { get; set; }
        public ReportModel() { }

        public ReportModel(string location, string category, string description, byte[] media,DateTime submissionDate,string status)
        {
            ReportLocation = location;
            ReportType = category;
            ReportDescription = description;
            ReportMedia = media;
            ReportDate = submissionDate;
            ReportStatus = status;

        }



    }
}
