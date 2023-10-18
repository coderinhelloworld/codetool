using CodingTool.Global;
using CodingTool.ViewModels;
using System.Windows.Controls;

namespace CodingTool.Views.Pages;

public partial class TemplateIndex : UserControl
{
    public TemplateIndex()
    {
        InitializeComponent();
        this.DataContext = new TemplateIndexViewModel();

    }
}