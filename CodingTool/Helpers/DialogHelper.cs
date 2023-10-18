using CodingTool.Global;
using CodingTool.Views.Controls;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CodingTool.Helpers
{
    public class DialogHelper
    {
        /// <summary>
        /// 加载Loading
        /// </summary>
        /// <param name="name"></param>
        public async void LoadingDialog(string name)
        {
            var loading = new Loading();
            Globals.Loading = loading;
            await MaterialDesignThemes.Wpf.DialogHost.Show(loading, name);
        }

        /// <summary>
        /// 关闭loading
        /// </summary>
        public void CloseDialog()
        {
            App.Current.Dispatcher.Invoke((Action)(() =>
            {
                DialogHost.CloseDialogCommand.Execute(null, null);
            }));
        }
        /// <summary>
        /// 展示Dialog信息
        /// </summary>
        /// <param name="message"></param>
        /// <param name="dialogName"></param>
        public async void MessageTips(string message, string dialogName="main")
        {
            CloseDialog();
            var infoDialog = new InfoDialog
            {
                Message = { Text = message }
            };
            await DialogHost.Show(infoDialog, dialogName);
        }
    }
}
