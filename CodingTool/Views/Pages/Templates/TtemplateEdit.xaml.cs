﻿using CodingTool.ViewModels;
using CodingTool.Views.ViewModels.Templates;
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

namespace CodingTool.Views.Pages
{
    /// <summary>
    /// TtemplateEdit.xaml 的交互逻辑
    /// </summary>
    public partial class TtemplateEdit : UserControl
    {
        public TtemplateEdit()
        {            
            InitializeComponent();
            this.DataContext=new TtemplateEditModel();
        }
    }
}
