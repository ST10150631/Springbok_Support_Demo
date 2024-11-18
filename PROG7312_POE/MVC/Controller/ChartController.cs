using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;
using PROG7312_POE.MVC.Model;
using PROG7312_POE.MVC.Model.Tree_Structures;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PROG7312_POE.MVC.Controller
{
    internal class ChartController
    {
        /// <summary>
        /// Holds the report min heap instance
        /// </summary>
        ReportMinHeap reportMinHeap;

        /// <summary>
        /// Holds Line Chart Values
        /// </summary>
        public ChartValues<double> LineChartValues { get; set; }
        /// <summary>
        /// Holds Bar Chart Values
        /// </summary>
        public ChartValues<double> BarChartValues { get; set; }
        /// <summary>
        /// Holds Bar Chart Labels
        /// </summary>
        public ObservableCollection<string> BarChartLabels { get; set; }
        /// <summary>
        /// Holds Pie Chart Values
        /// </summary>
        public SeriesCollection PieChartValues { get; set; }
        /// <summary>
        /// Holds Line Chart Axis X labels
        /// </summary>
        public ObservableCollection<string> LineChartAxisX { get; set; }
        /// <summary>
        /// Holds Table Data
        /// </summary>
        public ObservableCollection<DataRow> TableData { get; set; }
        /// <summary>
        /// Holds Total Reports number
        /// </summary>
        public int TotalReports { get; set; }
        /// <summary>
        /// Holds Total Resolved Reports number
        /// </summary>
        public int TotalResolvedReports { get; set; }
        /// <summary>
        /// Holds In Progress Reports number
        /// </summary>
        public int InProgress { get; set; }
        /// <summary>
        /// Holds a list of category names as an array
        /// </summary>
        public string[] categoryNames = new string[]
        {
    "Sanitation",
    "Roads",
    "Utilities",
    "Health",
    "Birth and Parenting",
    "Education",
    "Agriculture and Land",
    "Sports, Arts and Culture",
    "Business and Economic Activity",
    "Consumer Protection",
    "Citizenship and Immigration",
    "Employment and Labour",
    "Environment",
    "Money and Tax",
    "Legal and Defence",
    "Housing and Local Services",
    "Transport",
    "Social Services",
    "Retirement and Death"
};

        /// <summary>
        /// Constructor
        /// </summary>
        /// >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>> Start of Method >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
        public ChartController()
        {
            reportMinHeap = new ReportMinHeap();
            var reports = MainController.reportController.getAllReportsFromTree();
            foreach (var report in reports)
            {
                reportMinHeap.Insert(report);
            }
            TotalReports = reports.Count;
            TotalResolvedReports = reports.Count(r => r.ReportStatus == "Completed");
            InProgress = reports.Count(r => r.ReportStatus == "In Progress");
            // Pie Chart Data
            InitPieChart();
            // Line Chart Data
            InitLineChart(reports);

            // Bar Chart Data
            InitBarGraph();
            //Initialize Table Data
            InitTableData();
            


        }
        //------------------------------------------------------------------------ End of Method ------------------------------------------------------------------------------------------

        /// <summary>
        /// Initialize Pie Chart with data
        /// </summary>
        /// >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>> Start of Method >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
        private void InitPieChart()
        {
            var provineReports = MainController.reportController.getLocationReports();
            PieChartValues = new SeriesCollection();
            foreach (var province in provineReports)
            {
                PieChartValues.Add(new PieSeries
                {
                    Title = province.Key,
                    Values = new ChartValues<double> { province.Value }
                });
            }
        }
        //------------------------------------------------------------------------ End of Method ------------------------------------------------------------------------------------------

        /// <summary>
        /// Initialize Table with data
        /// </summary>
        /// >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>> Start of Method >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
        private void InitTableData()
        {
            ///Gets reports from the heap ordered by Date
            var reportsFromHeap = MainController.reportController.getAllReportsFromTree();
            // Group reports by their status
            var groupedReports = reportsFromHeap
                .GroupBy(r => r.ReportStatus)
                .OrderByDescending(g => g.Key);
            // Iterate through each group and add reports to TableData
            foreach (var group in groupedReports)
            {
                foreach (var report in group)
                {
                    //The resulting data will be orderd by status being Submitted first, then In Progress and finally Completed 
                    //Displaying the oldest reports first
                    TableData.Add(new DataRow
                    {
                        ID = "Report: " + report.ID,
                        Description = report.ReportDescription,
                        Category = report.ReportType,
                        Location = report.ReportLocation,
                        Status = report.ReportStatus,
                        Date = report.ReportDate.ToShortDateString()
                    });
                }
            }
        }
        //------------------------------------------------------------------------ End of Method ------------------------------------------------------------------------------------------

        /// <summary>
        /// Initialize the bar graph with data
        /// </summary>
        /// >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>> Start of Method >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
        private void InitBarGraph()
        {
            TableData = new ObservableCollection<DataRow>();
            BarChartValues = new ChartValues<double>();
            BarChartLabels = new ObservableCollection<string>();
            foreach (var category in categoryNames)
            {
                double chartValue = 0.0;
                chartValue = MainController.reportController.getCategoryReportCount(category);
                if (chartValue > 0)
                {
                    BarChartValues.Add(chartValue);
                    BarChartLabels.Add(category);
                }
                
            }

        }
        //------------------------------------------------------------------------ End of Method ------------------------------------------------------------------------------------------
        /// <summary>
        /// Initializes the line chart with data
        /// </summary>
        /// <param name="reports"></param>
        /// >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>> Start of Method >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
        private void InitLineChart(ObservableCollection<ReportModel> reports)
        {
            LineChartValues = new ChartValues<double>();
            ObservableCollection<string> lineChartLabels = new ObservableCollection<string>();

            // Group reports by SubmissionDate
            var groupedReports = reports
                .GroupBy(r => r.ReportDate.Date);
            groupedReports = groupedReports.OrderBy(g => g.Key);



            foreach (var group in groupedReports)
            {
                LineChartValues.Add(group.Count()); // Add the count of reports for each day
                lineChartLabels.Add(group.Key.ToString("yyyy-MM-dd")); // Format the date
            }

            // Bind labels to the X-axis
            LineChartAxisX = new ObservableCollection<string>(lineChartLabels);
        }

    }
    //------------------------------------------------------------------------ End of Method ------------------------------------------------------------------------------------------

    /// <summary>
    /// Data Row for Table
    /// </summary>
    /// 
    public class DataRow
    {
        public string ID { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public string Location { get; set; }
        public string Status { get; set; }
        public string Date { get; set; }    
    }
}

//=============================================================================== End of File =============================================================================