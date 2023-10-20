using CodingTool.Base;
using CodingTool.Global;
using CodingTool.Services.TemplateSr;
using CodingTool.Views.Pages;
using CoodingTool.Data.models;
using GalaSoft.MvvmLight.Messaging;
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

        public ICommand AddTemplateCommand { get; }
        public ICommand QueryListCommand { get; }
        public ICommand EditTemplateCommand { get; }

        public  TemplateIndexViewModel()
        {
            AddTemplateCommand = new CommandBase(AddTemplateAction);
            QueryListCommand = new CommandBase(QueryTemplateList);
            EditTemplateCommand=new CommandBase(ExecuteEditTemplate);
            // 注册消息接收器
            Messenger.Default.Register<NotificationMessage>(this, HandleMessage);
        }

        private void ExecuteEditTemplate(object? obj)
        {
            throw new NotImplementedException();
        }

        private void HandleMessage(NotificationMessage message)
        {            
            if (message.Notification == "QueryTemplateList")
            {
                // 这里处理收到的消息
                QueryTemplateList(null);
            }
        }

        private async void QueryTemplateList(object? _)
        {
            TemplateService  templateService = new TemplateService();
            var res= await templateService.GetListAsync();
            var list = new ObservableCollection<Template>();
            foreach (var item in res)
            {
                list.Add(item);
            }
            TemplateItems = list;
        }
        private async void AddTemplateAction(object? _)
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
