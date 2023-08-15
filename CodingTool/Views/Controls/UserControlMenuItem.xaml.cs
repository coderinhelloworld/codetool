using CodingTool.ViewModels.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CodingTool.Views.Controls
{
    /// <summary>
    /// UserControlMenuItem.xaml 的交互逻辑
    /// </summary>
    public partial class UserControlMenuItem : UserControl
    {
        MainWindow _context;
        private ItemMenu _itemMenu;

        public UserControlMenuItem(ItemMenu itemMenu, MainWindow context)
        {
            InitializeComponent();

            _context = context;

            ExpanderMenu.Visibility = itemMenu.SubItems == null ? Visibility.Collapsed : Visibility.Visible;

            ListViewItemMenu.Visibility = itemMenu.SubItems == null ? Visibility.Visible : Visibility.Collapsed;

            this.DataContext = itemMenu;
        }

        private void ListViewMenu_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (((SubItem)((ListView)sender).SelectedItem) != null)
            {
                _context.SwitchScreen(((SubItem)((ListView)sender).SelectedItem).Screen);
                var listView = sender as ListView;
                if (listView?.Items != null)
                    foreach (var item in listView?.Items)
                    {
                        ListViewItem? lvi = listView.ItemContainerGenerator.ContainerFromItem(item) as ListViewItem;
                        if (lvi != null)
                        {
                            lvi.IsSelected = false; // change isSelected
                        }
                    }
            }

        }
    }
}