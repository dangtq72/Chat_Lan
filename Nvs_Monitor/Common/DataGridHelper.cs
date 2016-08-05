using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Threading.Tasks;

namespace Nvs_Monitor
{
    /// <summary>
    /// Extension methods for DataGrid
    /// </summary>
    public static class DataGridHelper
    {
        /// <summary>
        /// Gets the visual child of an element
        /// </summary>
        /// <typeparam name="T">Expected type</typeparam>
        /// <param name="parent">The parent of the expected element</param>
        /// <returns>A visual child</returns>
        public static T GetVisualChild<T>(Visual parent) where T : Visual
        {
            try
            {
                T child = default(T);
                int numVisuals = VisualTreeHelper.GetChildrenCount(parent);
                for (int i = 0; i < numVisuals; i++)
                {
                    Visual v = (Visual)VisualTreeHelper.GetChild(parent, i);
                    child = v as T;
                    if (child == null)
                    {
                        child = GetVisualChild<T>(v);
                    }
                    if (child != null && child.GetType().Name == "DataGridCellsPresenter")
                    {
                        break;
                    }
                }
                return child;
            }
            catch
            {
                return default(T);
            }
        }

        /// <summary>
        /// Gets the specified row of the DataGrid
        /// </summary>
        /// <param name="grid">The DataGrid instance</param>
        /// <param name="index">The index of the row</param>
        /// <returns>A row of the DataGrid</returns>
        public static DataGridRow GetRow(this DataGrid grid, int index)
        {
            DataGridRow row = (DataGridRow)grid.ItemContainerGenerator.ContainerFromIndex(index);
            
            try
            {
                if (row == null)
                {
                    // May be virtualized, bring into view and try again.
                    grid.UpdateLayout();
                    grid.ScrollIntoView(grid.Items[index]);
                    row = (DataGridRow)grid.ItemContainerGenerator.ContainerFromIndex(index);
                }
                return row;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Gets the specified cell of the DataGrid
        /// </summary>
        /// <param name="grid">The DataGrid instance</param>
        /// <param name="row">The row of the cell</param>
        /// <param name="column">The column index of the cell</param>
        /// <returns>A cell of the DataGrid</returns>
        public static DataGridCell GetCell(this DataGrid grid, DataGridRow row, int column)
        {
            try
            {
                if (row != null)
                {
                    DataGridCellsPresenter presenter = GetVisualChild<DataGridCellsPresenter>(row);
                    if (presenter == null)
                    {
                        grid.ScrollIntoView(row, grid.Columns[column]);
                        presenter = GetVisualChild<DataGridCellsPresenter>(row);
                    }

                    DataGridCell cell = (DataGridCell)presenter.ItemContainerGenerator.ContainerFromIndex(column);

                    return cell;
                }
                return null;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Gets the specified cell of the DataGrid
        /// </summary>
        /// <param name="grid">The DataGrid instance</param>
        /// <param name="row">The row index of the cell</param>
        /// <param name="column">The column index of the cell</param>
        /// <returns>A cell of the DataGrid</returns>
        public static DataGridCell GetCell(this DataGrid grid, int row, int column)
        {
            DataGridRow rowContainer = grid.GetRow(row);
            return grid.GetCell(rowContainer, column);
        }

        /// <summary>
        /// Gets the selected row of the DataGrid
        /// </summary>
        /// <param name="grid">The DataGrid instance</param>
        /// <returns></returns>
        public static DataGridRow GetSelectedRow(this DataGrid grid)
        {
            return (DataGridRow)grid.ItemContainerGenerator.ContainerFromItem(grid.SelectedItem);
        }

        /// <summary>
        /// forcus vào bản ghi (tên datagrid, dòng , cột)
        /// </summary>
        /// <param name="grid">tên grid</param>
        /// <param name="row">row muốn forcus</param>
        /// <param name="column">cột</param>
        public static void NVSFocus(this DataGrid grid, int row, int column)
        {
            try
            {
                DataGridCell _dgc = DataGridHelper.GetCell(grid, row, column);
                //grid.Items.Refresh();
                if (_dgc != null)
                {
                    _dgc.Focus();
                    grid.SelectedIndex = row;
                }
                else
                {
                    _dgc.Focus();
                    grid.SelectedIndex = row;
                }
            }
            catch { }
        }


        //private T FindFirstElementInVisualTree<T>(DependencyObject parentElement) where T : DependencyObject
        //{
        //    var count = VisualTreeHelper.GetChildrenCount(parentElement);
        //    if (count == 0)
        //        return null;

        //    for (int i = 0; i < count; i++)
        //    {
        //        var child = VisualTreeHelper.GetChild(parentElement, i);

        //        if (child != null && child is T)
        //        {
        //            return (T)child;
        //        }
        //        else
        //        {
        //            var result = FindFirstElementInVisualTree<T>(child);
        //            if (result != null)
        //                return result;

        //        }
        //    }
        //    return null;
        //}

    }
}