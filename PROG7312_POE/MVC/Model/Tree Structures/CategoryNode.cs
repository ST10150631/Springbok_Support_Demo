using PROG7312_POE.MVC.Controller.Tree_Structures;
using System.Collections.Generic;

namespace PROG7312_POE.MVC.Model.Tree_Structures
{
    internal class CategoryNode
    {

        public string Content { get; set; }
        public List<CategoryNode> Children { get; set; }
        public ReportBinarySearchTree Reports { get; set; }

        public CategoryNode(string content)
        {
            Content = content;
            Children = new List<CategoryNode>();
            Reports = new ReportBinarySearchTree();
        }

        // Add a child node
        public void AddChild(CategoryNode child)
        {
            Children.Add(child);
        }
        // Count the number of reports in this category
        public int CountReports()
        {
            return CountReportsRecursively(Reports.root);
        }

        private int CountReportsRecursively(ReportNode node)
        {
            if (node == null)
                return 0;

            // Count current node + left subtree + right subtree
            return 1 + CountReportsRecursively(node.left) + CountReportsRecursively(node.right);
        }
    }
}
