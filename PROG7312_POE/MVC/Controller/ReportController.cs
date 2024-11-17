using PROG7312_POE.MVC.Model;
using PROG7312_POE.MVC.Model.Tree_Structures;
using PROG7312_POE.MVC.Model.Tree_Structures.PROG7312_POE.MVC.Model.Tree_Structures;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Media.Imaging;

namespace PROG7312_POE.MVC.Controller
{
    public class ReportController : INotifyPropertyChanged
    {
        /// <summary>
        /// Holds the reports in memmory
        /// </summary>
        private ObservableCollection<ReportModel> _reportData;

        /// <summary>
        /// Holds the report min heap instance
        /// </summary>
        ReportMinHeap reportMinHeap;

        /// <summary>
        /// Instance of the category general tree
        /// </summary>
        private CategoryGeneralTree categoryGeneralTree;
        /// <summary>
        /// Stores data in a list to be used for searching and for rubric purposes 
        /// </summary>
        private List<ReportModel> ReportList = new List<ReportModel>();

        /// <summary>
        /// Holds a list of image extensions
        /// </summary>
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
            categoryGeneralTree = new CategoryGeneralTree();
            _reportData = new ObservableCollection<ReportModel>();
             reportMinHeap = new ReportMinHeap();
            CreateDummyData();
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
            report.FindImageFiles();
            report.FindPdfFiles();
            report.FindMp4Files();
            ReportData.Add(report);
            ReportList.Add(report);
            reportMinHeap.Insert(report);
            categoryGeneralTree.AddReportToCategory(report.ReportType, report);
            OnPropertyChanged(nameof(ReportData));
        }
        //======================================================= End of Method ===================================================

        /// <summary>
        /// Gets all reports from the tree as an observable collection
        /// </summary>
        /// <returns></returns>
        /// >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>> Start of Method >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
        public ObservableCollection<ReportModel> getAllReportsFromTree()
        {
            var reports = categoryGeneralTree.GetAllReports();
            return reports;
        }
        //------------------------------------------------------------------------ End of Method ------------------------------------------------------------------------------------------

        /// <summary>
        ///  Creates Dummy Data 
        /// </summary>
        ///  >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>> Start of Method >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
        private void CreateDummyData()
        {
            var Report1 = new ReportModel
            {
                ReportDate = DateTime.Now.AddDays(-4),
                ReportDescription = "I need help on getting a birth certificate for my son as I lost his previous one",
                ReportLocation = "12 Bree Street,CBD,Cape Town, Western Province,South Africa",
                Province = "Western Province",
                ReportStatus = "Completed",
                ReportType = "Birth and Parenting"
            };
            AddReport(Report1);
            var Report2 = new ReportModel
            {
                ReportDate = DateTime.Now,
                ReportDescription = "A large pothole has formed on Johan Street",
                ReportLocation = "12 Johan Street,CBD,Johannesburg, Guateng,South Africa",
                Province = "Guateng",
                ReportStatus = "In Progress",
                ReportType = "Roads"
            };
            AddReport(Report2);
            // Report 3
            var Report3 = new ReportModel
            {
                ReportDate = DateTime.Now.AddDays(-1),
                ReportDescription = "There is a water leakage in the street causing a flood.",
                ReportLocation = "14 Sunset Avenue, Sea Point, Cape Town, South Africa",
                Province = "Western Province",
                ReportStatus = "In Progress",
                ReportType = "Utilities"
            };
            AddReport(Report3);

            // Report 4
            var Report4 = new ReportModel
            {
                ReportDate = DateTime.Now.AddDays(-2),
                ReportDescription = "A tree has fallen blocking the road on Pine Street.",
                ReportLocation = "45 Pine Street, Green Point, Cape Town, South Africa",
                Province = "Western Province",
                ReportStatus = "Completed",
                ReportType = "Environment"
            };
            AddReport(Report4);

            // Report 5
            var Report5 = new ReportModel
            {
                ReportDate = DateTime.Now.AddDays(-3),
                ReportDescription = "I need assistance with registering my business online.",
                ReportLocation = "10 Main Street, Johannesburg, Guateng, South Africa",
                Province = "Guateng",
                ReportStatus = "In Progress",
                ReportType = "Business and Economic Activity"
            };
            AddReport(Report5);

            // Report 6
            var Report6 = new ReportModel
            {
                ReportDate = DateTime.Now.AddDays(-2),
                ReportDescription = "The streetlights are not working on Oak Avenue.",
                ReportLocation = "78 Oak Avenue, Johannesburg, Guateng, South Africa",
                Province = "Guateng",
                ReportStatus = "Completed",
                ReportType = "Transport"
            };
            AddReport(Report6);

            // Report 7
            var Report7 = new ReportModel
            {
                ReportDate = DateTime.Now.AddDays(-2),
                ReportDescription = "A stray animal was found wandering around the neighborhood.",
                ReportLocation = "22 Willow Road, Durban, Kwazulu-Natal, South Africa",
                Province = "Kwazulu-Natal",
                ReportStatus = "In Progress",
                ReportType = "Social Services"
            };
            AddReport(Report7);

            var Report8 = new ReportModel
            {
                ReportDate = DateTime.Now,
                ReportDescription = "I need to aplly for a grant",
                ReportLocation = "15 Campground Street,Rondebosch,Cape Town, Western Province,South Africa",
                Province = "Western Province",
                ReportStatus = "Submitted",
                ReportType = "Birth and Parenting"
            };
            AddReport(Report8);


        }
        //=============================================================================== End of File =============================================================================

        /// <summary>
        /// Searches for a report by the report type in the tree and rturns a count of the reports in that category
        /// </summary>
        /// <param name="Category"></param>
        /// <returns></returns>
        /// >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>> Start of Method >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
        public int getCategoryReportCount(string Category)
        {
            var count = categoryGeneralTree.CountReportsInCategory(Category);
            return count;
        }
        //=============================================================================== End of File =============================================================================

    }
}
//=============================================================================== End of File =============================================================================