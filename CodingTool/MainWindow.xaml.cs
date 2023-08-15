using CodingTool.ViewModels;
using CodingTool.ViewModels.Models;
using CodingTool.Views.Controls;
using CodingTool.Views.Pages;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
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
using CodingTool.Global;

namespace CodingTool
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = new MainViewModel();

            var menuCode = new List<SubItem>();
            menuCode.Add(new SubItem("Yktv2 分层代码生成", new Yktv2AllLayersGenerate()));
            menuCode.Add(new SubItem("通过类生成代码", new GenerateSqlByYktv2Class()));
            menuCode.Add(new SubItem("测试页面", new TestPage()));
            var menu1 = new ItemMenu("代码生成器", menuCode, PackIconKind.SharkFin);
            Globals.MenuListViews.Add(new UserControlMenuItem(menu1,this));

            var menuImage = new List<SubItem>();
            menuImage.Add(new SubItem("图片转换成Icon", new ImageToIco()));
            var menu2 = new ItemMenu("图片相关", menuImage, PackIconKind.Image);
            Globals.MenuListViews.Add(new UserControlMenuItem(menu2,this));

            var menuStrings = new List<SubItem>();
            menuStrings.Add(new SubItem("多字符串替换", new StringsReplace()));
            var menu3 = new ItemMenu("字符串处理", menuStrings, PackIconKind.StringLights);
            Globals.MenuListViews.Add(new UserControlMenuItem(menu3,this));
            
            
            foreach (var menuItem in     Globals.MenuListViews)
            {
                Menu.Children.Add(menuItem);
            }
            // Menu.Children.Add(new UserControlMenuItem(menu1, this));
            // Menu.Children.Add(new UserControlMenuItem(menu2, this));
            // Menu.Children.Add(new UserControlMenuItem(menu3, this));
        }
        internal void SwitchScreen(object sender)
        {
            var screen = ((UserControl)sender);

          
                StackPanelMain.Children.Clear();
                StackPanelMain.Children.Add(screen);
     

        }
    }

}
