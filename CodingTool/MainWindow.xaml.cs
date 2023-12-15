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
using System.Threading;
using System.Windows.Threading;
using CoodingTool.Data;
using System.Windows.Forms;
using CodingTool.Views.ViewModels;
using AutoMapper;
using CodingTool.Services.AutoMapper;

namespace CodingTool
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            GlobalData.MainW = this;
            InitializeComponent();
            //初始化数据库
            using (var context =new SqlLiteDbContext())
            {
                context.Database.EnsureCreated();
            }
            this.DataContext = new MainViewModel();
 



            var menuCode = new List<SubItem>();
            menuCode.Add(new SubItem("Yktv2 分层代码生成", new Yktv2AllLayersGenerate()));
            menuCode.Add(new SubItem("通过类生成代码", new GenerateSqlByYktv2Class()));
            menuCode.Add(new SubItem("通过字段生成代码", new GenerateClassByProperty()));
            menuCode.Add(new SubItem("通过Json生成代码", new GenerateByJson()));
            menuCode.Add(new SubItem("文本处理", new DicTxtHandler()));
            var menu1 = new ItemMenu("代码生成器", menuCode, PackIconKind.Code);
            GlobalData.MenuListViews.Add(new UserControlMenuItem(menu1, this));

            var menuImage = new List<SubItem>();
            menuImage.Add(new SubItem("图片转换成Icon", new ImageToIco()));
            var menu2 = new ItemMenu("图片相关", menuImage, PackIconKind.Image);
            GlobalData.MenuListViews.Add(new UserControlMenuItem(menu2, this));

            var menuStrings = new List<SubItem>();
            menuStrings.Add(new SubItem("代码模板", new TemplateIndex()));
            var menu3 = new ItemMenu("模板管理", menuStrings, PackIconKind.CodeBlockHtml);
            GlobalData.MenuListViews.Add(new UserControlMenuItem(menu3, this));
            foreach (var menuItem in GlobalData.MenuListViews)
            {
                Menu.Children.Add(menuItem);
            }


        }




        internal void SwitchScreen(object sender)
        {
            var screen = ((System.Windows.Controls.UserControl)sender);


            StackPanelMain.Children.Clear();
            StackPanelMain.Children.Add(screen);


        }


        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                this.DragMove();
            }
        }


        private void ScrollViewer_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            if (sender is UIElement element)
            {
                e.Handled = true;

                var eventArg = new MouseWheelEventArgs(e.MouseDevice, e.Timestamp, e.Delta)
                {
                    RoutedEvent = UIElement.MouseWheelEvent,
                    Source = sender
                };

                element.RaiseEvent(eventArg);
            }
        }
    }

}
