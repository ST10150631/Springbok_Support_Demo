using PROG7312_POE.MVC.Model;
using System.Xml.Linq;
using System;
using PROG7312_POE.MVC.Controller.Tree_Structures;

internal class ReportBinarySearchTree
{
    /// <summary>
    /// Holds the Root of the tree which will be the first node inserted
    /// </summary>
    public ReportNode root;

    /// <summary>
    /// default constructor
    /// </summary>
    /// >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>> Start of Method >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
    public ReportBinarySearchTree()
    {
        root = null;
    }
    // ------------------------------------------------------------------------ End of Method ------------------------------------------------------------------------------------------

    /// <summary>
    /// Insert a ReportModel into the binary tree
    /// </summary>
    /// <param name="data"></param>
    /// >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>> Start of Method >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
    public void Insert(ReportModel data)
    {
        root = InsertRec(root, data);
    }
    // ------------------------------------------------------------------------ End of Method ------------------------------------------------------------------------------------------

    /// <summary>
    /// Insert ReportModel into the binary tree
    /// </summary>
    /// <param name="root"></param>
    /// <param name="data"></param>
    /// <returns></returns>
    /// >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>> Start of Method >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
    public ReportNode InsertRec(ReportNode root, ReportModel data)
    {
        if (root == null)
        {
            root = new ReportNode(data);
            return root;
        }

        // Compare based on report ID (or any other property of ReportModel)
        if (data.ID < root.data.ID)  // Assuming ReportModel has a ReportID
        {
            root.left = InsertRec(root.left, data);
        }
        else if (data.ID > root.data.ID)
        {
            root.right = InsertRec(root.right, data);
        }

        return root;
    }
    // ------------------------------------------------------------------------ End of Method ------------------------------------------------------------------------------------------

    /// <summary>
    /// InOrder Traversal
    /// </summary>
    /// >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>> Start of Method >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
    public void InOrder()
    {
        InOrderRec(root);
    }
    // ------------------------------------------------------------------------ End of Method ------------------------------------------------------------------------------------------

    /// <summary>
    /// InOrder Traversal Recursive
    /// </summary>
    /// <param name="root"></param>
    /// >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>> Start of Method >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
    public void InOrderRec(ReportNode root)
    {
        if (root != null)
        {
            InOrderRec(root.left);
            InOrderRec(root.right);
        }
    }
    // ------------------------------------------------------------------------ End of Method ------------------------------------------------------------------------------------------

    /// <summary>
    /// PreOrder Traversal 
    /// </summary>
    /// >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>> Start of Method >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
    public void PreOrder()
    {
        PreOrderRec(root);
    }
    // ------------------------------------------------------------------------ End of Method ------------------------------------------------------------------------------------------

    /// <summary>
    /// PreOrder Traversal Recursive
    /// </summary>
    /// <param name="root"></param>
    /// >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>> Start of Method >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
    public void PreOrderRec(ReportNode root)
    {
        if (root != null)
        {
            PreOrderRec(root.left);
            PreOrderRec(root.right);
        }
    }
    // ------------------------------------------------------------------------ End of Method ------------------------------------------------------------------------------------------

    /// <summary>
    /// PostOrder Traversal
    /// </summary>
    /// >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>> Start of Method >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
    public void PostOrder()
    {
        PostOrderRec(root);
    }
    // ------------------------------------------------------------------------ End of Method ------------------------------------------------------------------------------------------

    /// <summary>
    /// PostOrder Traversal Recursive
    /// </summary>
    /// <param name="root"></param>
    /// >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>> Start of Method >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
    public void PostOrderRec(ReportNode root)
    {
        if (root != null)
        {
            PostOrderRec(root.left);
            PostOrderRec(root.right);
        }
    }
    // ------------------------------------------------------------------------ End of Method ------------------------------------------------------------------------------------------
}
//=============================================================================== End of File =============================================================================