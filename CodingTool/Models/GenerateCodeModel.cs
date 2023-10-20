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
