
using CodingTool.ViewModels.Templates;
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