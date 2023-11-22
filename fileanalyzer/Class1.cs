using System;
using System.Collections;
using System.Windows.Forms;

public class ListViewColumnSorter : IComparer
{
    private int columnIndex;
    private SortOrder sortOrder;

    public ListViewColumnSorter()
    {
        columnIndex = 0; // Default column index to sort
        sortOrder = SortOrder.None; // Default sort order
    }

    public int Compare(object x, object y)
    {
        if (!(x is ListViewItem) || !(y is ListViewItem))
        {
            throw new ArgumentException("Objects must be of type ListViewItem.");
        }

        ListViewItem itemX = (ListViewItem)x;
        ListViewItem itemY = (ListViewItem)y;

        // Compare subitems based on the column index and sort order
        string textX = itemX.SubItems[columnIndex].Text;
        string textY = itemY.SubItems[columnIndex].Text;

        int result = string.Compare(textX, textY);

        if (sortOrder == SortOrder.Ascending)
        {
            return result;
        }
        else if (sortOrder == SortOrder.Descending)
        {
            return -result;
        }
        else
        {
            return 0;
        }
    }

    public int SortColumn
    {
        get { return columnIndex; }  // Add the get accessor to retrieve the columnIndex
        set
        {
            if (columnIndex == value)
            {
                sortOrder = sortOrder == SortOrder.Ascending ? SortOrder.Descending : SortOrder.Ascending;
            }
            else
            {
                columnIndex = value;
                sortOrder = SortOrder.Ascending;
            }
        }
    }
}
