using PROG7312_POE.MVC.Controller.Tree_Structures;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PROG7312_POE.MVC.Model.Tree_Structures
{
    internal class CategoryGeneralTree
    {
        /// <summary>
        /// Holds the root value of the tree
        /// </summary>
        private CategoryNode Root;

        


        /// <summary>
        /// Constructor
        /// </summary>
        /// >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>> Start of Method >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
        public CategoryGeneralTree()
        {
            InitializeTree();
        }
        // ------------------------------------------------------------------------ End of Method ------------------------------------------------------------------------------------------

        /// <summary>
        /// Initialize the tree with categories and subcategories
        /// </summary>
        /// >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>> Start of Method >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
        private void InitializeTree()
        {
            Root = new CategoryNode("Categories");

            // Add first-level categories
            Root.AddChild(new CategoryNode("Sanitation"));
            Root.AddChild(new CategoryNode("Roads"));
            Root.AddChild(new CategoryNode("Utilities"));
            Root.AddChild(new CategoryNode("Health"));
            Root.AddChild(new CategoryNode("Birth and Parenting"));
            Root.AddChild(new CategoryNode("Education"));
            Root.AddChild(new CategoryNode("Agriculture and Land"));
            Root.AddChild(new CategoryNode("Sports, Arts and Culture"));
            Root.AddChild(new CategoryNode("Business and Economic Activity"));
            Root.AddChild(new CategoryNode("Consumer Protection"));
            Root.AddChild(new CategoryNode("Citizenship and Immigration"));
            Root.AddChild(new CategoryNode("Employment and Labour"));
            Root.AddChild(new CategoryNode("Environment"));
            Root.AddChild(new CategoryNode("Money and Tax"));
            Root.AddChild(new CategoryNode("Legal and Defence"));
            Root.AddChild(new CategoryNode("Housing and Local Services"));
            Root.AddChild(new CategoryNode("Transport"));
            Root.AddChild(new CategoryNode("Social Services"));
            Root.AddChild(new CategoryNode("Retirement and Death"));
        }
        //------------------------------------------------------------------------ End of Method ------------------------------------------------------------------------------------------


        /// <summary>
        /// Recursive method to find a node by its content
        /// </summary>
        /// <param name="currentNode"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        /// >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>> Start of Method >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
        private CategoryNode FindNode(CategoryNode currentNode, string content)
        {
            if (currentNode.Content == content)
                return currentNode;

            foreach (var child in currentNode.Children)
            {
                var found = FindNode(child, content);
                if (found != null)
                    return found;
            }

            return null; // Node not found
        }
        //------------------------------------------------------------------------ End of Method ------------------------------------------------------------------------------------------


        /// <summary>
        /// Add a report to the binary tree of reports in a specific category 
        /// in the category tree
        /// </summary>
        /// <param name="category"></param>
        /// <param name="report"></param>
        /// <returns></returns>
        /// >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>> Start of Method >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
        public bool AddReportToCategory(string category, ReportModel report)
        {
            var categoryNode = FindNode(Root, category);
            if (categoryNode != null)
            {
                categoryNode.Reports.Insert(report);
                return true;
            }
            return false; // Category not found
        }
        //------------------------------------------------------------------------ End of Method ------------------------------------------------------------------------------------------

        /// <summary>
        /// Count reports in a specific category
        /// </summary>
        /// <param name="category"></param>
        /// <returns></returns>
        /// >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>> Start of Method >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
        public int CountReportsInCategory(string category)
        {
            var categoryNode = FindNode(Root, category);
            if (categoryNode != null)
            {
                return categoryNode.CountReports();
            }
            return 0; // Category not found or no reports
        }
        // ------------------------------------------------------------------------ End of Method ------------------------------------------------------------------------------------------

        /// <summary>
        /// Returns all reports in the tree as observable collection
        /// </summary>
        /// <returns></returns>
        /// >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>> Start of Method >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
        public ObservableCollection<ReportModel> GetAllReports()
        {
            var allReports = new ObservableCollection<ReportModel>();
            CollectReports(Root, allReports);
            return allReports;
        }
        //------------------------------------------------------------------------ End of Method ------------------------------------------------------------------------------------------

        /// <summary>
        /// Collects Reports from CategoryNode and its children
        /// </summary>
        /// <param name="node"></param>
        /// <param name="reports"></param>
        /// >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>> Start of Method >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
        private void CollectReports(CategoryNode node, ObservableCollection<ReportModel> reports)
        {
            if (node == null) return;

            // Traverse the binary tree of reports for the current category
            CollectReportsFromBinaryTree(node.Reports.root, reports);

            // Recursively collect reports from child nodes
            foreach (var child in node.Children)
            {
                CollectReports(child, reports);
            }
        }
        //------------------------------------------------------------------------ End of Method ------------------------------------------------------------------------------------------

        /// <summary>
        /// Collects Reports from the binary tree of a category
        /// </summary>
        /// <param name="binaryNode"></param>
        /// <param name="reports"></param>
        /// >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>> Start of Method >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
        private void CollectReportsFromBinaryTree(ReportNode binaryNode, ObservableCollection<ReportModel> reports)
        {
            if (binaryNode == null) return;

            // In-order traversal to collect reports
            CollectReportsFromBinaryTree(binaryNode.left, reports);

            if (binaryNode.data != null) // Check for non-null ReportModel
            {
                reports.Add(binaryNode.data);
            }

            CollectReportsFromBinaryTree(binaryNode.right, reports);
        }
        //------------------------------------------------------------------------ End of Method ------------------------------------------------------------------------------------------

        /// <summary>
        /// Display the tree and reports
        /// </summary>
        /// <param name="node"></param>
        /// <param name="depth"></param>
        /// >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>> Start of Method >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
        public void DisplayTree(CategoryNode node = null, int depth = 0)
        {
            if (node == null) node = Root;

            Console.WriteLine(new string('-', depth * 2) + node.Content);

            // Display reports in this node
            node.Reports.InOrder();

            foreach (var child in node.Children)
            {
                DisplayTree(child, depth + 1);
            }
        }
        // ------------------------------------------------------------------------ End of Method ------------------------------------------------------------------------------------------
    }
}
//=============================================================================== End of File =============================================================================