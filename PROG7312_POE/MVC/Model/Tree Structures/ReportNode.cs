using PROG7312_POE.MVC.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace PROG7312_POE.MVC.Controller.Tree_Structures
{
    internal class ReportNode
    {
        public ReportModel data;
        public ReportNode left, right;

        public ReportNode(ReportModel item)
        {
            data = item;
            left = right = null;
        }
    }
}
