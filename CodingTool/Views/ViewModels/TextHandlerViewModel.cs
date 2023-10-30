using CodingTool.Base;
using CodingTool.Services.TextHandler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CodingTool.Views.ViewModels
{
    public class TextHandlerViewModel : ViewModelBase
    {

        public ICommand BuildFullSqlCommand { get; set; }

        public TextHandlerViewModel()
        {
            BuildFullSqlCommand = new CommandBase(BuildFullSql);
        }

        private void BuildFullSql(object? obj)
        {
            OutputText = TextHelper.GetFullSql(InputText);
        }

        /// <summary>
        /// 输入内容
        /// </summary>
        private string _inputText;
        public string InputText
        {
            get => _inputText;
            set => SetProperty(ref _inputText, value);
        }
        /// <summary>
        /// 输出内容
        /// </summary>

        private string _outputText;
        public string OutputText
        {
            get => _outputText;
            set => SetProperty(ref _outputText, value);
        }

    }
}
