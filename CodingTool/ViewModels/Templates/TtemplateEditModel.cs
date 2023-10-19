using CodingTool.Base;
using CodingTool.Global;
using CoodingTool.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using CoodingTool.Data.models;
using CodingTool.Services.TemplateSr;
using GalaSoft.MvvmLight.Messaging;

namespace CodingTool.ViewModels.Templates
{
    public class TtemplateEditModel : ViewModelBase
    {
        public ICommand AddTemplateCommand { get; }

        public TtemplateEditModel()
        {
            AddTemplateCommand = new CommandBase(ExecuteAddTemplate);
        }

        private void ExecuteAddTemplate(object? obj)
        {
            Globals.DialogHelper.MessageTips("成功");
            using (var context = new SqlLiteDbContext())
            {
                var current = context.Template.OrderByDescending(x=>x.Id).FirstOrDefault();

                Template template = new Template
                {
                    Id = (current==null?1:current.Id)+1,
                    Title = Title,
                    Group = Group,
                    Content = Content,
                    CreateTime = DateTime.Now,
                    UpdateTime = DateTime.Now
                };

                context.Template.Add(template);
                context.SaveChanges();
            }
            // 发送消息
            Messenger.Default.Send(new NotificationMessage("QueryTemplateList"));

        }


        
        /// <summary>
        /// 标题
        /// </summary>
        private string _title;
        public string Title
        {
            get => _title;
            set => SetProperty(ref _title, value);
        }


        /// <summary>
        /// 内容
        /// </summary>
        private string _content;
        public string Content
        {
            get => _content;
            set => SetProperty(ref _content, value);
        }


        /// <summary>
        /// 组别
        /// </summary>
        private string _group;
        public string Group
        {
            get => _group;
            set => SetProperty(ref _group, value);
        }

    }
}
