using System;
using System.Collections.Generic;

namespace PROG7312_POE.MVC.Model.Tree_Structures
{
    internal class ReportMinHeap
    {
        /// <summary>
        /// Holds the heap structure
        /// </summary>
        private List<ReportModel> heap;

        /// <summary>
        /// Constructor to initialize the heap
        /// </summary>
        /// >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>> Start of Method >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
        public ReportMinHeap()
        {
            heap = new List<ReportModel>(); // Initialize an empty list to represent the heap.
        }
        //------------------------------------------------------------------------ End of Method ------------------------------------------------------------------------------------------

        // Get the index of the left child in the heap.
        private int LeftChild(int index) => 2 * index + 1;

        // Get the index of the right child in the heap.
        private int RightChild(int index) => 2 * index + 2;

        // Get the index of the parent node in the heap.
        private int Parent(int index) => (index - 1) / 2;

        /// <summary>
        /// Insert a ReportModel into the heap and maintain the heap property.
        /// </summary>
        /// >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>> Start of Method >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
        public void Insert(ReportModel report)
        {
            heap.Add(report); // Add the report at the end of the heap.
            HeapifyUp(heap.Count - 1); // Ensure the heap property is maintained after insertion.
        }
        //------------------------------------------------------------------------ End of Method ------------------------------------------------------------------------------------------

        /// <summary>
        /// Reorder the heap after inserting an element by moving it up to the correct position.
        /// This method compares both the status and ID of reports.
        /// </summary>
        /// >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>> Start of Method >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
        private void HeapifyUp(int index)
        {
            while (index > 0 && CompareReports(heap[Parent(index)], heap[index]) > 0)
            {
                Swap(index, Parent(index)); // Swap the current node with its parent.
                index = Parent(index); // Move to the parent's index.
            }
        }
        //------------------------------------------------------------------------ End of Method ------------------------------------------------------------------------------------------

        /// <summary>
        /// Extract the minimum element (root) from the heap, which is the report with the highest priority.
        /// The comparison considers both the status and ID of the reports.
        /// </summary>
        /// >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>> Start of Method >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
        public ReportModel ExtractMin()
        {
            if (heap.Count == 0) return null; // If the heap is empty, return null.

            ReportModel min = heap[0]; // The root of the heap is the minimum element.
            heap[0] = heap[heap.Count - 1]; // Replace the root with the last element in the heap.
            heap.RemoveAt(heap.Count - 1); // Remove the last element from the heap.
            HeapifyDown(0); // Restore the heap property after removing the root.
            return min; // Return the extracted minimum element.
        }
        //------------------------------------------------------------------------ End of Method ------------------------------------------------------------------------------------------

        /// <summary>
        /// Retrieve all elements in the heap in sorted order, emptying the heap in the process.
        /// </summary>
        /// >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>> Start of Method >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
        public List<ReportModel> GetAllElementsInOrder()
        {
            List<ReportModel> sortedList = new List<ReportModel>(); // Initialize a list to hold the sorted elements.
            while (heap.Count > 0)
            {
                sortedList.Add(ExtractMin()); // Extract the minimum element from the heap and add it to the sorted list.
            }
            return sortedList; // Return the sorted list of reports.
        }
        //------------------------------------------------------------------------ End of Method ------------------------------------------------------------------------------------------

        /// <summary>
        /// Reorder the heap after extracting the minimum element by moving the root down.
        /// This method compares both the status and ID of reports.
        /// </summary>
        /// >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>> Start of Method >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
        private void HeapifyDown(int index)
        {
            int left = LeftChild(index); // Get the index of the left child.
            int right = RightChild(index); // Get the index of the right child.
            int smallest = index; // Assume the current node is the smallest.

            // Compare the left child with the current node.
            if (left < heap.Count && CompareReports(heap[left], heap[smallest]) < 0)
                smallest = left;

            // Compare the right child with the smallest of the left child and current node.
            if (right < heap.Count && CompareReports(heap[right], heap[smallest]) < 0)
                smallest = right;

            // If the smallest element is not the current node, swap and heapify down the affected child.
            if (smallest != index)
            {
                Swap(index, smallest); // Swap the current node with the smallest child.
                HeapifyDown(smallest); // Recursively heapify down the affected child.
            }
        }
        //------------------------------------------------------------------------ End of Method ------------------------------------------------------------------------------------------

        /// <summary>
        /// Compare two reports based on their status and ID.
        /// </summary>
        /// >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>> Start of Method >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
        private int CompareReports(ReportModel report1, ReportModel report2)
        {
            // Compare based on the Date first.
            int dateComparison = report1.ReportDate.CompareTo(report2.ReportDate);
            if (dateComparison != 0) return dateComparison; // If dates are different, return the comparison result.

            // If dates are the same, compare by ID.
            return report1.ID.CompareTo(report2.ID); // Compare the IDs if the dates are equal.
        }
        //------------------------------------------------------------------------ End of Method ------------------------------------------------------------------------------------------

        /// <summary>
        /// Swap two elements in the heap.
        /// </summary>
        /// >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>> Start of Method >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
        private void Swap(int index1, int index2)
        {
            var temp = heap[index1]; // Store the first element temporarily.
            heap[index1] = heap[index2]; // Move the second element to the first position.
            heap[index2] = temp; // Place the first element in the second position.
        }
        //------------------------------------------------------------------------ End of Method ------------------------------------------------------------------------------------------

        /// <summary>
        /// Peek at the minimum element (root) without extracting it.
        /// </summary>
        /// >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>> Start of Method >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
        public ReportModel Peek()
        {
            return heap.Count > 0 ? heap[0] : null; // If the heap is not empty, return the root, else return null.
        }
        //------------------------------------------------------------------------ End of Method ------------------------------------------------------------------------------------------

        /// <summary>
        /// Return the count of elements in the heap.
        /// </summary>
        /// >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>> Start of Method >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
        public int Count => heap.Count; // Return the number of elements in the heap.
        //------------------------------------------------------------------------ End of Method ------------------------------------------------------------------------------------------
    }
}
