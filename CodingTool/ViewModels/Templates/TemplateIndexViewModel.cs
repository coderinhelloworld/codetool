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
using System.Windows.Input;
using System.Windows.Threading;

namespace CodingTool.ViewModels.Templates
{
    public class TemplateIndexViewModel : ViewModelBase
    {

        public ICommand RunDialogCommand { get; }

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
  
        }


        private async void ExecuteRunDialog(object? _)
        {
            var view = new TtemplateEdit();
            var result = await DialogHost.Show(view, "main", null, null, null);
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
