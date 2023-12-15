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
using CodingTool.Views.ViewModels.TemplateDtos;
using CodingTool.Models.Entities.Template.dto;

namespace CodingTool.Views.ViewModels.Templates
{
    public class TtemplateEditModel : ViewModelBase
    {
        public ICommand ConfirmAddCommand { get; }

        public TtemplateEditModel()
        {
            ConfirmAddCommand = new CommandBase(Confirm);
            TemplateDtoData.propertyChangedHandler += (val) =>
            {
                _title=val.Title;
                _content = val.Content;
                _group = val.Group;
                _id = val.Id;
            };
        }

        private void Confirm(object? obj)
        {
            GlobalData.DialogHelper.MessageTips("成功");
            using (var context = new SqlLiteDbContext())
            {
                if (Id > 0)
                {

                    var template = context.Template.FirstOrDefault(x => x.Id == Id);
                    if(template==null) return;
                    template.Title = Title;
                    template.Content = Content;
                    template.Group = Group;
                    template.UpdateTime = DateTime.Now;
                    context.Template.Update(template);
                }
                else
                {
                    var current = context.Template.OrderByDescending(x => x.Id).FirstOrDefault();

                    Template template = new Template
                    {
                        Id = (current == null ? 1 : current.Id) + 1,
                        Title = Title,
                        Group = Group,
                        Content = Content,
                        CreateTime = DateTime.Now,
                        UpdateTime = DateTime.Now
                    };
                    context.Template.Add(template);
                }

                context.SaveChanges();
            }
            // 发送消息
            Messenger.Default.Send(new NotificationMessage("QueryTemplateList"));

        }

        /// <summary>
        /// ID
        /// </summary>
        private int _id;
        public int Id
        {
            get => _id;
            set => SetProperty(ref _id, value);
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
