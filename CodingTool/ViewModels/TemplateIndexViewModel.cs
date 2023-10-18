using CodingTool.Base;
using CodingTool.Global;
using CodingTool.Views.Pages;
using CoodingTool.Data.models;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace CodingTool.ViewModels
{
    public class TemplateIndexViewModel : ViewModelBase
    {
        public CommandBase AddTemplateCommand { get; }

        public CommandBase RunDialogCommand { get; }

        public TemplateIndexViewModel()
        {

            var list = new ObservableCollection<Template>();
            list.Add(new Template
            {
                Name = "测试",
                Id = 1,
                CreateTime = DateTime.Now
            });
            TemplateItems = list;

            RunDialogCommand = new CommandBase(ExecuteRunDialog);
            AddTemplateCommand = new CommandBase(_ =>
            {
                Globals.DialogHelper.MessageTips("成功");
            });
        }

        private void ExecuteAddTemplate(object? obj)
        {
       
        }

        private async void ExecuteRunDialog(object? _)
        {
            //let's set up a little MVVM, cos that's what the cool kids are doing:
            var view = new TtemplateEdit
            {
                //DataContext = new TtemplateEdit()
            };

            //show the dialog
            //var result = await DialogHost.Show(view, "RootDialog", null, ClosingEventHandler, ClosedEventHandler);
            var result = await DialogHost.Show(view, "main", null, null, null);

            //check the result...
            Debug.WriteLine("Dialog was closed, the CommandParameter used to close it was: " + (result ?? "NULL"));
        }

        private ObservableCollection<Template> _Template = new ObservableCollection<Template>();
        public ObservableCollection<Template> TemplateItems
        {
            get
            {
                return _Template;
            }
            set
            {
                if (value == null || value.Count < 1)
                    return;
                //清空原先的列表
                Globals.MainW.Dispatcher.BeginInvoke(DispatcherPriority.Normal, (ThreadStart)delegate ()
                {
                    _Template.Clear();
                    foreach (Template poppingWord in value)
                    {
                        TemplateItems.Add(poppingWord);
                    }
                });

            }
        }
    }
}
