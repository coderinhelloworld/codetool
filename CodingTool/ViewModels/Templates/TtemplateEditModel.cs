using CodingTool.Base;
using CodingTool.Global;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

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
        }



    }
}
