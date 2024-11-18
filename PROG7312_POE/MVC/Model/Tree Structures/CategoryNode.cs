using PROG7312_POE.MVC.Controller.Tree_Structures;
using System.Collections.Generic;

namespace PROG7312_POE.MVC.Model.Tree_Structures
{
    internal class CategoryNode
    {
        /// <summary>
        /// Holds the CategoryNode content
        /// </summary>
        public string Content { get; set; }
        /// <summary>
        /// List of children nodes
        /// </summary>
        public List<CategoryNode> Children { get; set; }
        /// <summary>
        /// Binary search tree of reports
        /// </summary>
        public ReportAVL_Tree Reports { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="content"></param>
        /// >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>> Start of Method >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
        public CategoryNode(string content)
        {
            Content = content;
            Children = new List<CategoryNode>();
            Reports = new ReportAVL_Tree();
        }
        //------------------------------------------------------------------------ End of Method ------------------------------------------------------------------------------------------

        /// <summary>
        /// Adds a child to the category (a ReportBinarySearchTree)
        /// </summary>
        /// <param name="child"></param>
        /// >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>> Start of Method >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
        public void AddChild(CategoryNode child)
        {
            Children.Add(child);
        }
        //------------------------------------------------------------------------ End of Method ------------------------------------------------------------------------------------------

        /// <summary>
        /// Returns The number of reports in the tree
        /// </summary>
        /// <returns></returns>
        /// >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>> Start of Method >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
        public int CountReports()
        {
            return CountReportsRecursively(Reports.root);
        }
        // ------------------------------------------------------------------------ End of Method ------------------------------------------------------------------------------------------

        /// <summary>
        /// Counts the number of reports in the tree recursively
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        /// >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>> Start of Method >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
        private int CountReportsRecursively(ReportNode node)
        {
            if (node == null)
                return 0;

            // Count current node + left subtree + right subtree
            return 1 + CountReportsRecursively(node.left) + CountReportsRecursively(node.right);
        }
        // ------------------------------------------------------------------------ End of Method ------------------------------------------------------------------------------------------
    }
}
//=============================================================================== End of File =============================================================================