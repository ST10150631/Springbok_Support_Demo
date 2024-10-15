using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace PROG7312_POE.MVC.Model
{
    public class MediaModel
    {
        public string mediaName { get; set; }
        public string mediaType { get; set; }
        public byte[] mediabytes { get; set; }
        public BitmapImage mediaData { get; set; }
        public MediaModel()
        {

        }
    }
}
