using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatrixCalc.Controls
{
    using System.Windows;
    using System.Windows.Controls.Primitives;
    public class BindableUniformGrid : UniformGrid
    {


        public int RowsCount
        {
            get { return (int)GetValue(RowsCountProperty); }
            set { SetValue(RowsCountProperty, value); }
        }

        public static readonly DependencyProperty RowsCountProperty =
            DependencyProperty.Register("RowsCount", typeof(int), typeof(BindableUniformGrid), new PropertyMetadata(0, OnRowsCountChange));

        private static void OnRowsCountChange(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        {
            var grid = dependencyObject as BindableUniformGrid;
            var newCount = dependencyPropertyChangedEventArgs.NewValue as int?;
            if (newCount.HasValue && grid != null)
            {
                grid.Rows = newCount.Value;
            }
        }

        public int ColumnsCount
        {
            get { return (int)GetValue(ColumnsCountProperty); }
            set { SetValue(ColumnsCountProperty, value); }
        }

        public static readonly DependencyProperty ColumnsCountProperty =
            DependencyProperty.Register("ColumnsCount", typeof(int), typeof(BindableUniformGrid), new PropertyMetadata(0, OnColumnsCountChange));

        private static void OnColumnsCountChange(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        {
            var grid = dependencyObject as BindableUniformGrid;
            var newCount = dependencyPropertyChangedEventArgs.NewValue as int?;
            if (newCount.HasValue && grid != null)
            {
                grid.Columns = newCount.Value;
            }
        }
    }
}
