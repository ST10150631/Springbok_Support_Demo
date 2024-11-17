using LiveCharts;
using LiveCharts.Wpf;
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
        public SeriesCollection PieChartValues { get; set; }
        public ObservableCollection<DataRow> TableData { get; set; }

        public ChartController()
        {
            // Line Chart Data
            LineChartValues = new ChartValues<double> { 10, 20, 30, 40 };

            // Bar Chart Data
            BarChartValues = new ChartValues<double> { 15, 25, 35, 45 };

            // Table Data
            TableData = new ObservableCollection<DataRow>
            {
                new DataRow { Name = "Item A", Value1 = 10, Value2 = 20 },
                new DataRow { Name = "Item B", Value1 = 30, Value2 = 40 },
                new DataRow { Name = "Item C", Value1 = 50, Value2 = 60 },
            };
            // Pie Chart Data
            PieChartValues = new SeriesCollection
            {
                new PieSeries { Title = "Submitted", Values = new ChartValues<double> { 50 }, DataLabels = true },
                new PieSeries { Title = "In Progress", Values = new ChartValues<double> { 30 }, DataLabels = true },
                new PieSeries { Title = "Complete", Values = new ChartValues<double> { 20 }, DataLabels = true }
            };

        }
    }

    public class DataRow
    {
        public string Name { get; set; }
        public int Value1 { get; set; }
        public int Value2 { get; set; }
    }
}

