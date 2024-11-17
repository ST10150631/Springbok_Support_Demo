using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;
using PROG7312_POE.MVC.Model;
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
        public ChartValues<double> LineChartValues { get; set; }
        public ChartValues<double> BarChartValues { get; set; }
        public ObservableCollection<string> BarChartLabels { get; set; }
        public SeriesCollection PieChartValues { get; set; }
        public ObservableCollection<string> LineChartAxisX { get; set; }
        public ObservableCollection<DataRow> TableData { get; set; }
        public int TotalReports { get; set; }
        public int TotalResolvedReports { get; set; }
        public int InProgress { get; set; }
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


        public ChartController()
        {
            var reports = MainController.reportController.getAllReportsFromTree();
            TotalReports = reports.Count;
            TotalResolvedReports = reports.Count(r => r.ReportStatus == "Completed");
            InProgress = reports.Count(r => r.ReportStatus == "In Progress");
            // Pie Chart Data
            InitPieChart(reports);
            // Line Chart Data
            InitLineChart(reports);

            // Bar Chart Data
            InitBarGraph();
            //Initialize Table Data
            InitTableData(reports);
            


        }

        private void InitPieChart(ObservableCollection<ReportModel> reports)
        {
            PieChartValues = new SeriesCollection();
            foreach (var category in categoryNames)
            {
                double chartValue = 0.0;
                chartValue = MainController.reportController.getCategoryReportCount(category);
                if (chartValue > 0)
                {
                    PieChartValues.Add(new PieSeries
                    {
                        Title = category,
                        Values = new ChartValues<double> { chartValue }
                    });
                }
            }
        }
        /// <summary>
        /// Initialize Table with data
        /// </summary>
        /// >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>> Start of Method >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
        private void InitTableData(ObservableCollection<ReportModel> reports)
        {
            foreach (var report in reports)
            {
                TableData.Add(new DataRow { ID = "Report: " + report.ID, Description = report.ReportDescription , Category = report.ReportType, Location = report.ReportLocation,Status = report.ReportStatus });
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

    public class DataRow
    {
        public string ID { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public string Location { get; set; }
        public string Status { get; set; } // Add Status Property
    }
}

//=============================================================================== End of File =============================================================================