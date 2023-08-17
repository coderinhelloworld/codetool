using CodingTool.ViewModels.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
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
using CodingTool.Global;

namespace CodingTool.Views.Controls
{
    /// <summary>
    /// UserControlMenuItem.xaml 的交互逻辑
    /// </summary>
    public partial class UserControlMenuItem : UserControl
    {
        MainWindow _context;
        public ItemMenu _itemMenu;

        public UserControlMenuItem(ItemMenu itemMenu, MainWindow context)
        {
            InitializeComponent();

            _context = context;


            ExpanderMenu.Visibility = itemMenu.SubItems == null ? Visibility.Collapsed : Visibility.Visible;

            ListViewItemMenu.Visibility = itemMenu.SubItems == null ? Visibility.Visible : Visibility.Collapsed;

            this.DataContext = itemMenu;
            _itemMenu= itemMenu;
            //_itemMenu.IconColor = "120, 120, 120";
        }

        public void SetIconColor(string color)
        {
            _itemMenu.IconColor= color;
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
                            //如果不是当前选中项，则将其背景色设置为白色
                            if (item != listView.SelectedItem)
                            {
                                lvi.Background = new SolidColorBrush(Colors.Transparent);
                            }
                            else
                            {
                                lvi.Background = new SolidColorBrush(Color.FromRgb(165, 165, 165));
                            }
                            lvi.IsSelected = false; 
                        }
                    }
            
            }

            //更改其他listView的背景色
            var listViewSelected = sender as ListView;
            foreach (var item in Globals.MenuListViews)
            {
                item.SetIconColor("Blue");
                if (item is UserControlMenuItem)
                {
                    var listView = ((UserControlMenuItem)item).ListViewMenu;
                    if (listView?.Items != null && listViewSelected != listView)
                        foreach (var item2 in listView?.Items)
                        {
                            ListViewItem? lvi = listView.ItemContainerGenerator.ContainerFromItem(item2) as ListViewItem;
                            var subItem = item2 as ItemMenu;
                            if (lvi != null)
                            {
                                //如果不是当前选中项，则将其背景色设置为白色
                                lvi.Background = new SolidColorBrush(Colors.Transparent);
                            }
                        }
                }
            }
        }
        
    }
}