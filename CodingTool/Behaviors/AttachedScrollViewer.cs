using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace CodingTool.Behaviors
{
    public  static class AttachedScrollViewer
    {
        public static bool GetIsMouseWheelScrollingEnabled(DataGrid dataGrid)
        {
            return (bool)dataGrid.GetValue(IsMouseWheelScrollingEnabledProperty);
        }

        public static void SetIsMouseWheelScrollingEnabled(DataGrid dataGrid, bool value)
        {
            dataGrid.SetValue(IsMouseWheelScrollingEnabledProperty, value);
        }

        public static readonly DependencyProperty IsMouseWheelScrollingEnabledProperty =
            DependencyProperty.RegisterAttached(
                "IsMouseWheelScrollingEnabled",
                typeof(bool),
                typeof(AttachedScrollViewer),
                new UIPropertyMetadata(false, OnIsMouseWheelScrollingEnabledChanged)
            );

        private static void OnIsMouseWheelScrollingEnabledChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is DataGrid dataGrid)
            {
                if ((bool)e.NewValue)
                {
                    dataGrid.PreviewMouseWheel += DataGrid_PreviewMouseWheel;
                }
                else
                {
                    dataGrid.PreviewMouseWheel -= DataGrid_PreviewMouseWheel;
                }
            }
        }

        private static void DataGrid_PreviewMouseWheel(object sender, System.Windows.Input.MouseWheelEventArgs e)
        {
            if (sender is DataGrid dataGrid)
            {
                ScrollViewer scrollViewer = GetScrollViewer(dataGrid);
                if (scrollViewer != null)
                {
                    if (e.Delta > 0)
                    {
                        scrollViewer.LineUp();
                    }
                    else if (e.Delta < 0)
                    {
                        scrollViewer.LineDown();
                    }
                    e.Handled = true;
                }
            }
        }

        private static ScrollViewer GetScrollViewer(Visual visual)
        {
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(visual); i++)
            {
                Visual child = VisualTreeHelper.GetChild(visual, i) as Visual;
                if (child != null)
                {
                    if (child is ScrollViewer)
                    {
                        return (ScrollViewer)child;
                    }
                    ScrollViewer childOfChild = GetScrollViewer(child);
                    if (childOfChild != null)
                    {
                        return childOfChild;
                    }
                }
            }
            return null;
        }
    }
}
