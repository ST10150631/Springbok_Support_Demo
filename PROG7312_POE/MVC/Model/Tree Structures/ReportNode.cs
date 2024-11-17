using PROG7312_POE.MVC.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace PROG7312_POE.MVC.Controller.Tree_Structures
{
    // Report node class used to create a node out of the ReportModel
    internal class ReportNode
    {
        /// <summary>
        /// Holds the reportModel data
        /// </summary>
        public ReportModel data;
        /// <summary>
        /// Left/ right child nodes for branching
        /// </summary>
        public ReportNode left, right;

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="item"></param>
        /// >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>> Start of Method >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
        public ReportNode(ReportModel item)
        {
            data = item;
            left = right = null;
        }
        //------------------------------------------------------------------------ End of Method ------------------------------------------------------------------------------------------
    }
}
//=============================================================================== End of File =============================================================================