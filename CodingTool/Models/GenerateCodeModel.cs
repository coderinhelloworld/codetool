using CodingTool.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingTool.ViewModels.Models
{
    public class GenerateCodeModel : ViewModelBase
    {
        //private string _entityPath;
        //public string EntityPath
        //{
        //    get => _entityPath;
        //    set => SetProperty(ref _entityPath, value);
        //}

        //private string _controllerPath;
        //public string ControllerPath
        //{
        //    get => _controllerPath;
        //    set => SetProperty(ref _controllerPath, value);
        //}


        //private string _serverPath;
        //public string ServicePath
        //{
        //    get => _serverPath;
        //    set => SetProperty(ref _serverPath, value);
        //}



        ///// <summary>
        ///// 模块名称,用于生成文件夹
        ///// </summary>
        //private string _mouduleName;
        //public string MouduleName
        //{
        //    get => _mouduleName;
        //    set => SetProperty(ref _mouduleName, value);
        //}


        ///// <summary>
        ///// 项目名称
        ///// </summary>
        //private string _projectName;
        //public string ProjectName
        //{
        //    get => _projectName;
        //    set => SetProperty(ref _projectName, value);
        //}


        private string _tableName;
        public string TableName
        {
            get => _tableName;
            set => SetProperty(ref _tableName, value);
        }


        /// <summary>
        /// 字段
        /// </summary>
        private ObservableCollection<string> _Fields = new ObservableCollection<string>();
        public ObservableCollection<string> Fields
        {
            get => _Fields;
            set => SetProperty(ref _Fields, value);
        }
    }
}
