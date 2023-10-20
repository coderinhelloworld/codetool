using CodingTool.Base;
using CodingTool.Functions;
using CodingTool.Helpers;
using CodingTool.ViewModels.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using CodingTool.Enums;
using CodingTool.Extentions;
using CodingTool.Global;
using CoodingTool.Data;
using CoodingTool.Data.models;
using System.Windows.Input;

namespace CodingTool.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        /// <summary>
        ///生成代码命令及相关字段
        /// </summary>

        public ICommand CodeGenerateCommand { get; }
        public ICommand Yktv2SqlGenerateCommand { get; }
        public ICommand ImgToIcoCommand { get; }
        public ICommand ChooseFileCommand { get; }

        public ICommand GenerateTestMethodsCommand { get; }

        public ICommand CloseWindowCommand { get; set; }
        public ICommand MiniWindowCommand { get; set; }
        public ICommand MaximizedWindowCommand { get; set; }
        public ICommand GenetateClassByProertyCommand { get; set; }
        public ICommand GenetateClassByJsonCommand { get; set; }
        public ICommand JsonFormateCommand { get; set; }

        /// <summary>
        /// 生成一卡通实体类
        /// </summary>
        public ICommand GenetateYktEoClassByProertyCommand { get; set; }

        /// <summary>
        /// 生成sql建表语句
        /// </summary>
        public ICommand GetSqlCreateTableCommand { get; set; }

        /// <summary>
        /// 查找读取函数中的读块包含更改的
        /// </summary>
        public ICommand FindChangeSqlInReadFunctionCommand { get; set; }

        public MainViewModel()
        {
            //获取主程序所在的目录
            LogoPath = System.IO.Path.Combine("assets", "images", "logos", "1.png");

            _generateCodeModel = new GenerateCodeModel();
            CodeGenerateCommand = new CommandBase(_ =>
            {
                CodeGenerate.Generate(_generateCodeModel);
            });
            Yktv2SqlGenerateCommand = new CommandBase(_ =>
            {
                OutputText = CodeGenerate.Yktv2SqlGenerateByClass(InputText);
            });
            ImgToIcoCommand = new CommandBase(_ =>
            {
                ImageFunctions.ImageToIco(InputText);
            });
            ChooseFileCommand = new CommandBase(_ =>
            {

                InputText = SelectFileWpf();
            });
            GenerateTestMethodsCommand = new CommandBase(_ =>
            {
                OutputText = CodeGenerate.GenerateTestMethods(InputText);
            });

            GenetateYktEoClassByProertyCommand=new CommandBase(_ =>
            {
                OutputText = CodeGenerate.GenerateYktEoClass(InputText);
            });
            GetSqlCreateTableCommand = new CommandBase(_ =>
            {
                OutputText = CodeGenerate.GetSqlCreateTable(InputText);
            });
            FindChangeSqlInReadFunctionCommand=new CommandBase(_ =>
            {
                //异步执行
                Task task = Task.Run(() =>
                {
                    TxtHandler.FindChangeSqlInFuntions(InputText);
                });
  
            });


            #region 界面类

            CloseWindowCommand = new CommandBase(_ =>
            {
                Application.Current.Shutdown();
            });
            MiniWindowCommand = new CommandBase(_ =>
            {
                Application.Current.MainWindow.WindowState = WindowState.Minimized;
            });
            MaximizedWindowCommand = new CommandBase(_ =>
            {
                if (Application.Current.MainWindow.WindowState == WindowState.Maximized)
                {
                    Application.Current.MainWindow.WindowState = WindowState.Normal;
                }
                else
                {
                    Application.Current.MainWindow.WindowState = WindowState.Maximized;
                }
            });
            #endregion
            GenetateClassByProertyCommand = new CommandBase(_ =>
            {

                OutputText = CodeGenerate.GenerateClassByProerty(InputText);
            });
            GenetateClassByJsonCommand = new CommandBase(_ =>
            {

                OutputText = CodeGenerate.GenerateClassByJson(InputText);
            });

            JsonFormateCommand = new CommandBase(_ =>
            {
                OutputText = JsonHelper.ConvertJsonString(InputText);


            });


            //定时切换logo
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(0.12);
            timer.Tick += Timer_Tick;
            timer.Start();

        }


        private void Timer_Tick(object sender, EventArgs e)
        {
            if (logoCount == 14)
            {
                logoCount = 1;
            }
            LogoPath = System.IO.Path.Combine("assets", "images", "logos", logoCount.ToString()+".png");
            logoCount++;
        }
        private int logoCount = 1;
        public string SelectFileWpf()
        {
            var openFileDialog = new Microsoft.Win32.OpenFileDialog()
            {
                Filter = "All files (*.*)|*.*"
            };
            var result = openFileDialog.ShowDialog();
            if (result == true)
            {
                return openFileDialog.FileName;
            }
            else
            {
                return null;
            }
        }
        private string _logoPath;
        public string LogoPath
        {
            get => _logoPath;
            set => SetProperty(ref _logoPath, value);
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




        private GenerateCodeModel _generateCodeModel;

        public GenerateCodeModel GenerateCodeModel
        {
            get => _generateCodeModel;
            set => SetProperty(ref _generateCodeModel, value);
        }

    }
}
