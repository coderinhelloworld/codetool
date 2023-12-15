using CodingTool.Global;
using CodingTool.Views.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingTool.Helpers
{
    public static class MsgHelper
    {
        public static void ShowMsg(string msg)
        {
            GlobalData.MainW.Dispatcher.Invoke(new Action(delegate
            {
                ((MainViewModel)GlobalData.MainW.DataContext).OutputText = msg;
                //要做的事
            }));
        }
        public static void AppendMsg(string msg)
        {
            GlobalData.MainW.Dispatcher.Invoke(new Action(delegate
            {
                ((MainViewModel)GlobalData.MainW.DataContext).OutputText +="\r\n"+ msg;
            }));
        }
    }
}
