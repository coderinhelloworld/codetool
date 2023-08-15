using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace CodingTool.ViewModels.Models
{
    public class ItemMenu
    {
        public ItemMenu(string header, List<SubItem> subItems, PackIconKind icon,string iconColor = "White")
        {
            Header = header;
            SubItems = subItems;
            Icon = icon;
            IconColor= iconColor;
        }

        public string Header { get; private set; }
        public PackIconKind Icon { get; private set; }
        public string IconColor { get;  set; }
        public List<SubItem> SubItems { get; private set; }
        public UserControl Screen { get; private set; }
    }
}
