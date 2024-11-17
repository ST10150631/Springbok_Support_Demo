using PROG7312_POE.MVC.Model;
using System.Xml.Linq;
using System;
using PROG7312_POE.MVC.Controller.Tree_Structures;

internal class BinaryTree
{
    public ReportNode root;

    public BinaryTree()
    {
        root = null;
    }

    public void Insert(ReportModel data)
    {
        root = InsertRec(root, data);
    }

    // Insert ReportModel into the binary tree
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

    public void InOrder()
    {
        InOrderRec(root);
    }

    // InOrder Traversal
    public void InOrderRec(ReportNode root)
    {
        if (root != null)
        {
            InOrderRec(root.left);
            Console.WriteLine(root.data);  // You can customize this to print specific properties of ReportModel
            InOrderRec(root.right);
        }
    }

    public void PreOrder()
    {
        PreOrderRec(root);
    }

    // PreOrder Traversal
    public void PreOrderRec(ReportNode root)
    {
        if (root != null)
        {
            Console.WriteLine(root.data);  // You can customize this to print specific properties of ReportModel
            PreOrderRec(root.left);
            PreOrderRec(root.right);
        }
    }

    public void PostOrder()
    {
        PostOrderRec(root);
    }

    // PostOrder Traversal
    public void PostOrderRec(ReportNode root)
    {
        if (root != null)
        {
            PostOrderRec(root.left);
            PostOrderRec(root.right);
            Console.WriteLine(root.data);  // You can customize this to print specific properties of ReportModel
        }
    }
}
