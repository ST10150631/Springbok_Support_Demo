using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Media.Imaging;

namespace PROG7312_POE.MVC.Model
{
    public class ReportModel 
    {
        /// <summary>
        /// Holds the ID of the report
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// Holds the Description of the report
        /// </summary>
        public string ReportDescription { get; set; }
        /// <summary>
        /// Holds the Type of the report
        /// </summary>
        public string ReportType { get; set; }
        /// <summary>
        /// Holds the Date of the report
        /// </summary>
        public DateTime ReportDate { get; set; }
        /// <summary>
        /// Holds the Location of the report
        /// </summary>
        public string ReportLocation { get; set; }
        /// <summary>
        /// Holds the Report Status
        /// </summary>
        public string ReportStatus { get; set; }
        /// <summary>
        /// Holds the Province of the report
        /// </summary>
        public string Province { get; set; }

        /// <summary>
        /// Holds the data for the PDF files
        /// </summary>
        public ObservableCollection<MediaNode> ReportPdfsData { get; set; }
        /// <summary>
        /// Holds a collection of video files
        /// </summary>
        public ObservableCollection<MediaNode> ReportVideosData { get; set; }
        /// <summary>
        /// Holds a collection of image files
        /// </summary>
        public ObservableCollection<MediaNode> ReportImagesData { get; set; }


        // Tree structure for media files
        public MediaNode MediaTreeRoot { get; set; }
        /// <summary>
        /// Holds the current node in the media tree
        /// </summary>
        public MediaNode currentNode { get; set; }
        /// <summary>
        /// Holds a collection of valid image extensions
        /// </summary>
        public string[] validImageExtensions = { ".jpg", ".jpeg", ".gif", ".bmp", ".png" };

        /// <summary>
        /// constructor for the ReportModel
        /// </summary>
        /// >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>> Start of Method >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
        public ReportModel()
        {
            MediaTreeRoot = new MediaNode { Name = "Root" };  // Initialize the root of the media tree
            ReportPdfsData = new ObservableCollection<MediaNode>();
            ReportVideosData = new ObservableCollection<MediaNode>();
            ReportImagesData = new ObservableCollection<MediaNode>();
        }
        //------------------------------------------------------------------------ End of Method ------------------------------------------------------------------------------------------

        /// <summary>
        /// Adds a PDF to the report
        /// </summary>
        /// <param name="pdfNode"></param>
        /// >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>> Start of Method >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
        public void AddPdf(MediaNode pdfNode)
        {
            ReportPdfsData.Add(pdfNode);
        }
        //------------------------------------------------------------------------ End of Method ------------------------------------------------------------------------------------------

        /// <summary>
        /// Adds a video to the report
        /// </summary>
        /// <param name="videoNode"></param>
        /// >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>> Start of Method >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
        public void AddVideo(MediaNode videoNode)
        {
            ReportVideosData.Add(videoNode);
        }
        //------------------------------------------------------------------------ End of Method ------------------------------------------------------------------------------------------

        /// <summary>
        /// Adds an image to the report
        /// </summary>
        /// <param name="imageNode"></param>
        /// >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>> Start of Method >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
        public void AddImage(MediaNode imageNode)
        {
            ReportImagesData.Add(imageNode);
        }
        //------------------------------------------------------------------------ End of Method ------------------------------------------------------------------------------------------

        /// <summary>
        /// Finds all image files in the media tree
        /// </summary>
        /// <returns></returns>
        /// >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>> Start of Method >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
        public List<MediaNode> FindImageFiles()
        {
            var images = new List<MediaNode>();
            TraverseTree(MediaTreeRoot, node =>
            {
                if (node.MediaType != null && validImageExtensions.Contains(node.MediaType))
                {
                    images.Add(node);
                }
            });
            return images;
        }
        //------------------------------------------------------------------------ End of Method ------------------------------------------------------------------------------------------

        /// <summary>
        /// Finds all PDF files in the media tree
        /// </summary>
        /// <returns></returns>
        /// >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>> Start of Method >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
        public List<MediaNode> FindPdfFiles()
        {
            return FindFilesByType(".pdf");
        }
        //------------------------------------------------------------------------ End of Method ------------------------------------------------------------------------------------------

        /// <summary>
        /// Finds All MP4 files in the media tree
        /// </summary>
        /// <returns></returns>
        /// >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>> Start of Method >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
        public List<MediaNode> FindMp4Files()
        {
            return FindFilesByType(".mp4");
        }
        //------------------------------------------------------------------------ End of Method ------------------------------------------------------------------------------------------

        /// <summary>
        /// Finds Files by type in the media tree
        /// </summary>
        /// <param name="fileType"></param>
        /// <returns></returns>
        /// >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>> Start of Method >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
        private List<MediaNode> FindFilesByType(string fileType)
        {
            var files = new List<MediaNode>();
            TraverseTree(MediaTreeRoot, node =>
            {
                if (node.MediaType != null && node.MediaType.Equals(fileType, StringComparison.OrdinalIgnoreCase))
                {
                    files.Add(node);
                }
            });
            return files;
        }
        // ------------------------------------------------------------------------ End of Method ------------------------------------------------------------------------------------------

        /// <summary>
        /// Traverses the media tree
        /// </summary>
        /// <param name="node"></param>
        /// <param name="action"></param>
        /// >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>> Start of Method >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
        private void TraverseTree(MediaNode node, Action<MediaNode> action)
        {
            if (node == null)
                return;

            action(node);

            foreach (var child in node.Children)
            {
                TraverseTree(child, action);
            }
        }
        //------------------------------------------------------------------------ End of Method ------------------------------------------------------------------------------------------
    }
}
//=============================================================================== End of File =============================================================================