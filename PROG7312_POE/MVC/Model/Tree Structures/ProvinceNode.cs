using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PROG7312_POE.MVC.Model.Tree_Structures
{
    public class ProvinceNode
    {
        /// <summary>
        /// Holds the Province name
        /// </summary>
        public string Province { get; set; }
        /// <summary>
        /// Holds A list of reports for the province
        /// </summary>
        public List<ReportModel> Reports { get; set; }

        /// <summary>
        /// default constructor
        /// </summary>
        /// <param name="province"></param>
        /// >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>> Start of Method >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
        public ProvinceNode(string province)
        {
            Province = province;
            Reports = new List<ReportModel>();
        }
        //------------------------------------------------------------------------ End of Method ------------------------------------------------------------------------------------------

        /// <summary>
        /// Adds a report to the list of reports for the province
        /// </summary>
        /// <param name="report"></param>
        /// >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>> Start of Method >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
        public void AddReport(ReportModel report)
        {
            Reports.Add(report);
        }            
        //------------------------------------------------------------------------ End of Method ------------------------------------------------------------------------------------------

    }
}
//=============================================================================== End of File =============================================================================