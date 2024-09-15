using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using PROG7312_POE.MVC.Model;

namespace PROG7312_POE.MVC.Controller
{
    public class ReportController : INotifyPropertyChanged
    {
        /// <summary>
        /// Holds the reports in memmory
        /// </summary>
        private ObservableCollection<ReportModel> _reportData;
        /// <summary>
        /// Stores data in a list to be used for searching and for rubric purposes 
        /// </summary>
        private List <ReportModel> ReportList = new List<ReportModel>();

        public readonly string[] ImgExtensions = { ".jpg", ".jpeg", ".gif", ".bmp", ".png" };
        /// <summary>
        /// Holds reports as an observable collection to bu used for binding
        /// </summary>
        public ObservableCollection<ReportModel> ReportData
        {
            get { return _reportData; }
            set
            {
                _reportData = value;

                OnPropertyChanged(nameof(ReportData));
            }
        }

        /// <summary>
        /// default constructor
        /// </summary>
        /// ----------------------------------------------------- Start of Method ------------------------------------------------
        public ReportController()
        {
            _reportData = new ObservableCollection<ReportModel>();
        }
        //======================================================= End of Method ===================================================


        /// <summary>
        /// An event for when the semesterData is changed
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Invokes the onpropertyChanged event
        /// </summary>
        /// <param name="propertyName"></param>
        /// ----------------------------------------------------- Start of Method ------------------------------------------------
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        //======================================================= End of Method ===================================================

        /// <summary>
        /// Adds A report to the observable collection of reports
        /// </summary>
        /// <param name="report"></param>
        /// ----------------------------------------------------- Start of Method ------------------------------------------------
        public void AddReport(ReportModel report)
        {
            report.ID = ReportData.Count + 1;
            report.ReportDate = DateTime.Now;
            report.ReportStatus = "Filed";
            ReportData.Add(report);
            ReportList.Add(report);
            OnPropertyChanged(nameof(ReportData));
        }
        //======================================================= End of Method ===================================================


        public ReportModel SearchByID(int ID)
        {
            var foundReport = ReportData.FirstOrDefault(x => x.ID == ID);
            return foundReport;

        }

        /// <summary>
        /// Opens file dialog to select media to upload then returns the media content, path and type
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        /// ----------------------------------------------------- Start of Method ------------------------------------------------
        public (byte[] MediaData, string MediaType, string FilePath) UploadMedia()
        {
            
            OpenFileDialog openFileDialog = new OpenFileDialog()
            {
                Filter = "PDF Files (*.pdf)|*.pdf|Image Files (*.jpg;*.jpeg;*.gif;*.bmp;*.png)|" +
                "*.jpg;*.jpeg;*.gif;*.bmp;*.png",
                FilterIndex = 1
            };
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    string filePath = openFileDialog.FileName;
                    string fileType = Path.GetExtension(filePath).ToLower();
                   
                    // Read the file data into a byte array
                    byte[] fileData = File.ReadAllBytes(filePath);

                    // Return the byte array and file type
                    return (fileData, fileType, filePath);
                }
                catch (Exception ex)
                {
                    System.Windows.MessageBox.Show("Error Loading image.", "Error", MessageBoxButton.OK);
                    // Handle exceptions (e.g., file not found, access denied)
                    throw new Exception($"Error reading file: {ex.Message}", ex);
                    
                }
            }
            else
            {
                return (null, null, null);
            }
        }
        //======================================================= End of Method ===================================================

        /// <summary>
        /// Checks if the provided media type is a valid image extension
        /// </summary>
        /// <param name="mediaType"></param>
        /// <returns></returns>
        /// ----------------------------------------------------- Start of Method ------------------------------------------------
        private bool IsValidImageExtension(string mediaType)
        {
            if (string.IsNullOrWhiteSpace(mediaType))
                return false;

            // Convert to lowercase to ensure case-insensitive comparison
            mediaType = mediaType.ToLower();

            // Check if the provided extension is in the list of valid extensions
            return ImgExtensions.Contains(mediaType);
        }
        //======================================================= End of Method ===================================================

    }
}
//=============================================================================== End of File =============================================================================