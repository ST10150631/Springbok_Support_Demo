using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace PROG7312_POE.MVC.Model
{
    public class MediaNode
    {
        /// <summary>
        /// Holds the node name with the first node being the root
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Holds the media data
        /// </summary>
        public byte[] MediaData { get; set; }
        /// <summary>
        /// Images
        /// </summary>
        public BitmapImage Image { get; set; }
        /// <summary>
        /// Holds the Video Uri
        /// </summary>
        public Uri VideoUri { get; set; }
        /// <summary>
        /// Holds the media type
        /// </summary>
        public string MediaType { get; set; }
        /// <summary>
        /// Holds the Media File Path
        /// </summary>
        public string FilePath { get; set; }
        /// <summary>
        /// List of subnodes for the tree as it will be a generic tree
        /// </summary>
        public List<MediaNode> Children { get; set; } // Subnodes

        /// <summary>
        /// Constructor
        /// </summary>
        /// ----------------------------------------------------- Start of Method ------------------------------------------------
        public MediaNode()
        {
            Children = new List<MediaNode>();
        }
        //======================================================= End of Method ===================================================

        /// <summary>
        /// Returns the media data as a BitmapImage
        /// </summary>
        /// <returns></returns>
        /// ----------------------------------------------------- Start of Method ------------------------------------------------
        public BitmapImage getImage()
        {
            if (MediaData == null || MediaData.Length == 0)
                return null;
            if (MediaType == null || MediaType =="pdf" || MediaType=="mp4")
                return null;

            BitmapImage bitmapImage = new BitmapImage();
            using (MemoryStream stream = new MemoryStream(MediaData))
            {
                stream.Seek(0, SeekOrigin.Begin); // Ensure the stream is at the beginning
                bitmapImage.BeginInit();
                bitmapImage.StreamSource = stream;
                bitmapImage.CacheOption = BitmapCacheOption.OnLoad; // Cache the image data
                bitmapImage.EndInit();
                bitmapImage.Freeze(); // Freeze to make it cross-thread accessible
            }
            return bitmapImage;
        }
        //======================================================= End of Method ===================================================
    }
}
// ############################################################### End of File ###############################################################