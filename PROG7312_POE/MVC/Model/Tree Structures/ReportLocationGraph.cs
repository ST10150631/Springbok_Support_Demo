using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PROG7312_POE.MVC.Model.Tree_Structures
{
    public class ReportLocationGraph
    {
        private Dictionary<string, ProvinceNode> _provinceNodes;

        /// <summary>
        /// Constructor
        /// </summary>
        /// >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>> Start of Method >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
        public ReportLocationGraph()
        {
            _provinceNodes = new Dictionary<string, ProvinceNode>();
        }
        //------------------------------------------------------------------------ End of Method ------------------------------------------------------------------------------------------

        /// <summary>
        /// Adds a report to the graph
        /// </summary>
        /// <param name="report"></param>
        /// >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>> Start of Method >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
        public void AddReport(ReportModel report)
        {
            if (!_provinceNodes.ContainsKey(report.Province))
            {
                _provinceNodes[report.Province] = new ProvinceNode(report.Province);
            }
            _provinceNodes[report.Province].AddReport(report);
        }
        //------------------------------------------------------------------------ End of Method ------------------------------------------------------------------------------------------

        /// <summary>
        /// Gets the number of reports for each province
        /// </summary>
        /// <returns></returns>
        /// >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>> Start of Method >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
        public Dictionary<string, int> GetReportCountsByProvince()
        {
            return _provinceNodes.ToDictionary(
                node => node.Key,
                node => node.Value.Reports.Count
            );
        }
        //------------------------------------------------------------------------ End of Method ------------------------------------------------------------------------------------------

        /// <summary>
        /// Gets all the provinces in the graph
        /// </summary>
        /// <returns></returns>
        /// >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>> Start of Method >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
        public List<string> GetAllProvinces()
        {
            return _provinceNodes.Keys.ToList();
        }
        //------------------------------------------------------------------------ End of Method ------------------------------------------------------------------------------------------

        /// <summary>
        ///  Find Minimum Spanning Tree using Prim's algorithm
        /// </summary>
        /// <returns></returns>
        /// >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>> Start of Method >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
        public List<string> GetMinimumSpanningTree()
        {
            var provinces = _provinceNodes.Keys.ToList();
            var mst = new List<string>();
            var visited = new HashSet<string>();
            var edges = new List<Tuple<string, string, int>>();  // (from, to, weight)

            // Start from any province (let's pick the first one)
            string startProvince = provinces[0];
            visited.Add(startProvince);

            // Add edges based on report count (this is where the "weight" is defined)
            foreach (var province in provinces)
            {
                if (province != startProvince)
                {
                    edges.Add(new Tuple<string, string, int>(startProvince, province, GetReportCountsByProvince()[province]));
                }
            }

            // Use a priority queue to get the minimum edge
            while (visited.Count < provinces.Count)
            {
                var minEdge = edges
                    .Where(e => visited.Contains(e.Item1) && !visited.Contains(e.Item2))
                    .OrderBy(e => e.Item3)
                    .FirstOrDefault();

                if (minEdge != null)
                {
                    mst.Add(minEdge.Item2);  // Add the connected province to MST
                    visited.Add(minEdge.Item2);
                }
            }

            return mst;  // Minimum Spanning Tree
        }
    }
    //------------------------------------------------------------------------ End of Method ------------------------------------------------------------------------------------------
}
//=============================================================================== End of File =============================================================================