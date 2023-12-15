using AutoMapper;
using CodingTool.Base;
using CodingTool.Global;
using CodingTool.Models.Entities.Template.dto;
using CodingTool.Services.TemplateSr;
using CodingTool.Views.Pages;
using CodingTool.Views.ViewModels.TemplateDtos;
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
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;

namespace CodingTool.Views.ViewModels.Templates
{
    public class TemplateIndexViewModel : ViewModelBase
    {
        private readonly IMapper _mapper;

        public ICommand AddTemplateCommand { get; }
        public ICommand QueryListCommand { get; }
        public ICommand EditTemplateCommand { get; }
        public ICommand DeleteCommand { get; }

        public TemplateIndexViewModel()
        {
            _mapper = GlobalData.Mapper;
            AddTemplateCommand = new CommandBase(AddTemplateAction);
            QueryListCommand = new CommandBase(QueryTemplateList);
            EditTemplateCommand = new CommandBase(ExecuteEditTemplate);
            DeleteCommand = new CommandBase(ExecuteDeleteTemplate);
            // 注册消息接收器
            Messenger.Default.Register<NotificationMessage>(this, HandleMessage);
        }



        private void ExecuteDeleteTemplate(object? obj)
        {
            var template = obj as TemplateDto;
            if (template != null)
            {
                new TemplateService().DeleteById(template.Id);
                QueryTemplateList(null);

            }        
        }

        /// <summary>
        /// 添加模板 
        /// </summary>
        /// <param name="_"></param>
        private async void AddTemplateAction(object? _)
        {
            var view = new TtemplateEdit();
            var result = await DialogHost.Show(view, "main", null, null, null);
        }

        /// <summary>
        /// 编辑模板
        /// </summary>
        /// <param name="obj"></param>
        private async void ExecuteEditTemplate(object? obj)
        {
     
            var selectedTemplates = _Templates.Where(x=>x.IsSeleted==true).ToList();
            if(selectedTemplates.Count>1)
            {
                GlobalData.DialogHelper.MessageTips("只能选择一个模板进行编辑");
                return;
            }
            if (selectedTemplates.Count ==0)
            {
                GlobalData.DialogHelper.MessageTips("请选择一个模板进行编辑");
                return;
            }

            var selectedTemplate= _Templates.Where(x => x.IsSeleted == true).FirstOrDefault();

            var view = new TtemplateEdit();
            TemplateDtoData.TemplateData = selectedTemplate;
            var result = await DialogHost.Show(view, "main", null, null, null);

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
            TemplateService templateService = new TemplateService();
            _Templates.Clear();
            var res = await templateService.GetListAsync();
            var list = new ObservableCollection<TemplateDto>();
            foreach (var item in res)
            {
                var dto= _mapper.Map<TemplateDto>(item);
                list.Add(dto);
            }
            TemplateItems = list;
        }
    

        private ObservableCollection<TemplateDto> _Templates = new ObservableCollection<TemplateDto>();
        public ObservableCollection<TemplateDto> TemplateItems
        {
            get
            {
                return _Templates;
            }
            set
            {
                if (value == null || value.Count < 1)
                    return;
                //清空原先的列表
                GlobalData.MainW.Dispatcher.BeginInvoke(DispatcherPriority.Normal, (ThreadStart)delegate ()
                {
                    _Templates.Clear();
                    foreach (TemplateDto item in value)
                    {
                        TemplateItems.Add(item);
                    }
                });

            }
        }
    }
}
