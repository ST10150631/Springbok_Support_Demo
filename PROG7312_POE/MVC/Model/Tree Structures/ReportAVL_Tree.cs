using PROG7312_POE.MVC.Model;
using System.Xml.Linq;
using System;
using PROG7312_POE.MVC.Controller.Tree_Structures;

internal class ReportAVL_Tree
{
    /// <summary>
    /// Holds the Root of the tree which will be the first node inserted
    /// </summary>
    public ReportNode root;

    /// <summary>
    /// default constructor
    /// </summary>
    /// >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>> Start of Method >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
    public ReportAVL_Tree()
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
        // 1. Perform the normal BST insert
        if (root == null)
        {
            root = new ReportNode(data);
            return root;
        }

        if (data.ID < root.data.ID)
        {
            root.left = InsertRec(root.left, data);
        }
        else if (data.ID > root.data.ID)
        {
            root.right = InsertRec(root.right, data);
        }
        else // Duplicate IDs are not allowed in the tree
        {
            return root;
        }

        // 2. Update the height of this ancestor node
        root.height = Math.Max(GetHeight(root.left), GetHeight(root.right)) + 1;

        // 3. Get the balance factor of this ancestor node to check whether it became unbalanced
        int balance = GetBalance(root);

        // 4. If this node becomes unbalanced, then there are 4 cases

        // Left Left Case
        if (balance > 1 && data.ID < root.left.data.ID)
        {
            return RightRotate(root);
        }

        // Right Right Case
        if (balance < -1 && data.ID > root.right.data.ID)
        {
            return LeftRotate(root);
        }

        // Left Right Case
        if (balance > 1 && data.ID > root.left.data.ID)
        {
            root.left = LeftRotate(root.left);
            return RightRotate(root);
        }

        // Right Left Case
        if (balance < -1 && data.ID < root.right.data.ID)
        {
            root.right = RightRotate(root.right);
            return LeftRotate(root);
        }

        // return the (unchanged) node pointer
        return root;
    }
    // ------------------------------------------------------------------------ End of Method ------------------------------------------------------------------------------------------

    /// <summary>
    /// Right rotate the subtree rooted with y
    /// </summary>
    /// <param name="y"></param>
    /// <returns></returns>
    /// >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>> Start of Method >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
    private ReportNode RightRotate(ReportNode y)
    {
        ReportNode x = y.left;
        ReportNode T2 = x.right;

        // Perform rotation
        x.right = y;
        y.left = T2;

        // Update heights
        y.height = Math.Max(GetHeight(y.left), GetHeight(y.right)) + 1;
        x.height = Math.Max(GetHeight(x.left), GetHeight(x.right)) + 1;

        // Return the new root
        return x;
    }
    // ------------------------------------------------------------------------ End of Method ------------------------------------------------------------------------------------------

    /// <summary>
    /// Left rotate the subtree rooted with x
    /// </summary>
    /// <param name="x"></param>
    /// <returns></returns>
    /// >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>> Start of Method >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
    private ReportNode LeftRotate(ReportNode x)
    {
        ReportNode y = x.right;
        ReportNode T2 = y.left;

        // Perform rotation
        y.left = x;
        x.right = T2;

        // Update heights
        x.height = Math.Max(GetHeight(x.left), GetHeight(x.right)) + 1;
        y.height = Math.Max(GetHeight(y.left), GetHeight(y.right)) + 1;

        // Return the new root
        return y;
    }
    // ------------------------------------------------------------------------ End of Method ------------------------------------------------------------------------------------------

    /// <summary>
    /// Get the height of a node
    /// </summary>
    /// <param name="node"></param>
    /// <returns></returns>
    /// >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>> Start of Method >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
    private int GetHeight(ReportNode node)
    {
        if (node == null)
        {
            return 0;
        }
        return node.height;
    }
    // ------------------------------------------------------------------------ End of Method ------------------------------------------------------------------------------------------

    /// <summary>
    /// Get the balance factor of a node
    /// </summary>
    /// <param name="node"></param>
    /// <returns></returns>
    /// >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>> Start of Method >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
    private int GetBalance(ReportNode node)
    {
        if (node == null)
        {
            return 0;
        }
        return GetHeight(node.left) - GetHeight(node.right);
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