
using AutoMapper;
using CodingTool.Views.ViewModels.Templates;
using GalaSoft.MvvmLight.Messaging;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace CodingTool.Views.Pages;

public partial class TemplateIndex : UserControl
{
    public TemplateIndex( )
    {
        InitializeComponent();
        this.DataContext = new TemplateIndexViewModel();
        Messenger.Default.Send(new NotificationMessage("QueryTemplateList"));
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