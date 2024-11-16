using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Media.Imaging;

namespace PROG7312_POE.MVC.Model
{
    public class ReportModel : INotifyPropertyChanged
    {
        public int ID { get; set; }
        public string ReportDescription { get; set; }
        public string ReportType { get; set; }
        public DateTime ReportDate { get; set; }
        public string ReportLocation { get; set; }
        public string ReportStatus { get; set; }

        public ObservableCollection<MediaNode> ReportPdfsData { get; set; }
        public ObservableCollection<MediaNode> ReportVideosData { get; set; }
        public ObservableCollection<MediaNode> ReportImagesData { get; set; }


        // Tree structure for media files
        public MediaNode MediaTreeRoot { get; set; }

        private BitmapImage _currentImage;
        public BitmapImage currentImage
        {
            get { return _currentImage; }
            set
            {
                if (_currentImage != value)
                {
                    _currentImage = value;
                    OnPropertyChanged();  // Notify the UI about the property change
                }
            }
        }

        public MediaNode currentNode { get; set; }

        public string[] validImageExtensions = { ".jpg", ".jpeg", ".gif", ".bmp", ".png" };

        public ReportModel()
        {
            MediaTreeRoot = new MediaNode { Name = "Root" };  // Initialize the root of the media tree
            ReportPdfsData = new ObservableCollection<MediaNode>();
            ReportVideosData = new ObservableCollection<MediaNode>();
            ReportImagesData = new ObservableCollection<MediaNode>();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        // Notify that a property has changed
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void AddPdf(MediaNode pdfNode)
        {
            ReportPdfsData.Add(pdfNode);
        }

        public void AddVideo(MediaNode videoNode)
        {
            ReportVideosData.Add(videoNode);
        }

        public void AddImage(MediaNode imageNode)
        {
            ReportImagesData.Add(imageNode);
        }

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

        // Method to find PDFs
        public List<MediaNode> FindPdfFiles()
        {
            return FindFilesByType(".pdf");
        }

        // Method to find MP4 files
        public List<MediaNode> FindMp4Files()
        {
            return FindFilesByType(".mp4");
        }

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
    }
}
