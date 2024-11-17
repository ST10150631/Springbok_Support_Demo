using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PROG7312_POE.MVC.Model.Tree_Structures
{
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
            ///  Constructor
            /// </summary>
            /// >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>> Start of Method >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
            public ReportMinHeap()
            {
                heap = new List<ReportModel>();
            }
            //------------------------------------------------------------------------ End of Method ------------------------------------------------------------------------------------------

            /// <summary>
            /// Get the index of the left child
            /// </summary>
            /// <param name="index"></param>
            /// <returns></returns>
            private int LeftChild(int index) => 2 * index + 1;

            /// <summary>
            /// Get the index of the right child
            /// </summary>
            /// <param name="index"></param>
            /// <returns></returns>
            private int RightChild(int index) => 2 * index + 2;


            /// <summary>
            /// Get the index of the parent
            /// </summary>
            /// <param name="index"></param>
            /// <returns></returns>
            private int Parent(int index) => (index - 1) / 2;

            /// <summary>
            /// Insert a ReportModel into the heap
            /// </summary>
            /// <param name="report"></param>
            /// >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>> Start of Method >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
            public void Insert(ReportModel report)
            {
                heap.Add(report);
                HeapifyUp(heap.Count - 1);
            }
            //------------------------------------------------------------------------ End of Method ------------------------------------------------------------------------------------------

            /// <summary>
            /// Heapify the heap after insertion (moving the inserted element up)
            /// </summary>
            /// <param name="index"></param>
            /// >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>> Start of Method >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
            private void HeapifyUp(int index)
            {
                while (index > 0 && heap[Parent(index)].ID > heap[index].ID)
                {
                    Swap(index, Parent(index));
                    index = Parent(index);
                }
            }
            //------------------------------------------------------------------------ End of Method ------------------------------------------------------------------------------------------

            /// <summary>
            /// Extract the min element (root) from the heap
            /// </summary>
            /// <returns></returns>
            /// >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>> Start of Method >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
            public ReportModel ExtractMin()
            {
                if (heap.Count == 0) return null;

                ReportModel min = heap[0];
                heap[0] = heap[heap.Count - 1];
                heap.RemoveAt(heap.Count - 1);
                HeapifyDown(0);
                return min;
            }
            //------------------------------------------------------------------------ End of Method ------------------------------------------------------------------------------------------

            /// <summary>
            /// Retrieve all elements in the heap in sorted order.
            /// This will empty the heap in the process.
            /// </summary>
            /// <returns>A list of elements in sorted order.</returns>
            /// >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>> Start of Method >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
            public List<ReportModel> GetAllElementsInOrder()
            {
                List<ReportModel> sortedList = new List<ReportModel>();
                while (heap.Count > 0)
                {
                    sortedList.Add(ExtractMin());
                }
                return sortedList;
            }
            //------------------------------------------------------------------------ End of Method ------------------------------------------------------------------------------------------

            /// <summary>
            /// Heapify the heap after extraction (moving the root down)
            /// </summary>
            /// <param name="index"></param>
            /// >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>> Start of Method >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
            private void HeapifyDown(int index)
            {
                int left = LeftChild(index);
                int right = RightChild(index);
                int smallest = index;

                if (left < heap.Count && heap[left].ID < heap[smallest].ID)
                    smallest = left;

                if (right < heap.Count && heap[right].ID < heap[smallest].ID)
                    smallest = right;

                if (smallest != index)
                {
                    Swap(index, smallest);
                    HeapifyDown(smallest);
                }
            }
            //------------------------------------------------------------------------ End of Method ------------------------------------------------------------------------------------------

            /// <summary>
            /// Swap two elements in the heap
            /// </summary>
            /// <param name="index1"></param>
            /// <param name="index2"></param>
            /// >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>> Start of Method >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
            private void Swap(int index1, int index2)
            {
                var temp = heap[index1];
                heap[index1] = heap[index2];
                heap[index2] = temp;
            }
            //------------------------------------------------------------------------ End of Method ------------------------------------------------------------------------------------------

            /// <summary>
            /// Peek at the minimum element (root) without extracting it
            /// </summary>
            /// <returns></returns>
            /// >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>> Start of Method >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
            public ReportModel Peek()
            {
                return heap.Count > 0 ? heap[0] : null;
            }
            //------------------------------------------------------------------------ End of Method ------------------------------------------------------------------------------------------

            /// <summary>
            /// Return the count of elements in the heap
            /// </summary>
            public int Count => heap.Count;
        }
    }

}
//=============================================================================== End of File =============================================================================