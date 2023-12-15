using CodingTool.Models.Entities.Template.dto;
using CoodingTool.Data.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingTool.Views.ViewModels.TemplateDtos
{
    public static  class TemplateDtoData
    {

        //自定义事件委托 

        //发送信息
        public delegate void PropertyChangedEventHandler(TemplateDto e);
        static public event PropertyChangedEventHandler propertyChangedHandler;

        //定义数据类型
        static private TemplateDto TemplateDto;

        static public TemplateDto TemplateData
        {
            get
            {
                return TemplateDto;
            }
            set
            {
                TemplateDto = value;
                propertyChangedHandler(TemplateDto);
            }
        }   

    }
}
