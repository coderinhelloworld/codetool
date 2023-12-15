using CodingTool.Base;
using CodingTool.Functions;
using CodingTool.Helpers;
using CodingTool.ViewModels.Models;
using System;
using System.IO;
using System.Windows;
using System.Windows.Threading;
using System.Windows.Input;
using CodingTool.Global;
using CoodingTool.Data.models;
using System.Collections.ObjectModel;
using System.Threading;
using System.Collections.Generic;
using CodingTool.Models;
using CodingTool.Models.Enums.System;
using CodingCommon.Extensions;
using System.Reflection.Metadata;
using CoodingTool.Data;
using System.Linq;
using System.Threading.Tasks;
using CodingCommon.Extentions;
using CodingTool.Services.System;

namespace CodingTool.Views.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        /// <summary>
        ///生成代码命令及相关字段
        /// </summary>


        private ObservableCollection<RadioItem> _RadioButtons = new ObservableCollection<RadioItem>();
        public ObservableCollection<RadioItem> RadioButtons
        {
            get
            {
                return _RadioButtons;
            }
            set
            {
                if (value == null || value.Count < 1)
                    return;
                //清空原先的列表
                //GlobalData.MainW.Dispatcher.BeginInvoke(DispatcherPriority.Normal, (ThreadStart)delegate ()
                //{
                //    _RadioButtons.Clear();
                //    foreach (RadioItem poppingWord in value)
                //    {
                //        RadioButtons.Add(poppingWord);
                //    }
                //});

            }
        }
        public ICommand CodeGenerateCommand { get; }
        public ICommand Yktv2SqlGenerateCommand { get; }
        public ICommand ImgToIcoCommand { get; }
        public ICommand ChooseFileCommand { get; }

        public ICommand GenerateTestMethodsCommand { get; }

        public ICommand ShowSettingCommand { get; set; }
        public ICommand CloseWindowCommand { get; set; }
        public ICommand MiniWindowCommand { get; set; }
        public ICommand MaximizedWindowCommand { get; set; }
        public ICommand GenetateClassByProertyCommand { get; set; }
        public ICommand GenetateClassByJsonCommand { get; set; }
        public ICommand JsonFormateCommand { get; set; }

        /// <summary>
        /// 选择
        /// </summary>
        public ICommand RadioCheckCommand { get; }
        /// <summary>
        /// 生成一卡通实体类
        /// </summary>
        public ICommand GenetateYktEoClassByProertyCommand { get; set; }

        /// <summary>
        /// 生成sql建表语句
        /// </summary>
        public ICommand GetSqlCreateTableCommand { get; set; }



        public MainViewModel()
        {

            SettingInfoService settingInfoService = new SettingInfoService();
            var settingInfo = settingInfoService.GetSettingInfo();
            var nameingCase = NamingCase.TitleCase;
            if (settingInfo != null)
            {
                if (!settingInfo.Parameters.IsNullOrEmpty())
                {
                    var settings = settingInfo.Parameters.DeserializeObject<Models.Enums.System.Settings>();
                    nameingCase = settings.NamingCase;
                }         
            }
            //获取枚举NamingCase的所有名
            foreach (var item in NamingCase.TitleCase.GetEnumElements())
            {
                var radioItem = new RadioItem()
                {
                    Content = item.Description.Replace("_", "__"),//默认情况下，文本内容会自动去除下划线（_）作为快捷键标记。这是为了支持访问键和快捷键，因为下划线通常用于指示访问键。如果您希望保留下划线，可以在 XAML 中使用两个下划线（__）来转义
                    IsCheck = (NamingCase)item.Enum == nameingCase,
                    NamingCase = (NamingCase)item.Enum
                };
                _RadioButtons.Add(radioItem);
            }








            //获取主程序所在的目录
            LogoPath = Path.Combine("assets", "images", "logos", "1.png");

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

            GenetateYktEoClassByProertyCommand = new CommandBase(_ =>
            {
                OutputText = CodeGenerate.GenerateYktEoClass(InputText);
            });
            GetSqlCreateTableCommand = new CommandBase(_ =>
            {
                OutputText = CodeGenerate.GetSqlCreateTable(InputText);
            });

            RadioCheckCommand = new CommandBase(_ =>
            {
                var chooseRadio = RadioButtons.FirstOrDefault(x => x.IsCheck == true);
                if (chooseRadio != null)
                {
                    var settingInfo = settingInfoService.GetSettingInfo();
                    if (settingInfo == null)
                    {
                        settingInfo = new SettingInfo
                        {
                            Parameters =new Settings()
                            {
                                NamingCase=chooseRadio.NamingCase
                            }.SerializeObject()
                        };
                        settingInfoService.SaveSettingInfo(settingInfo);
                    }
                    else
                    {
                        var settings = settingInfo.Parameters.DeserializeObject<Models.Enums.System.Settings>();
                        if (settings == null)
                        {
                            settings = new Settings
                            {
                                NamingCase = chooseRadio.NamingCase
                            };
                        }
                        settings.NamingCase= chooseRadio.NamingCase;
                        settingInfo.Parameters = settings.SerializeObject();
                        settingInfoService.SaveSettingInfo(settingInfo);
                    }
                }

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
            LogoPath = Path.Combine("assets", "images", "logos", logoCount.ToString() + ".png");
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

        private int _namingCase;
        public int NamingCaseData
        {
            get => _namingCase;
            set
            {
                SetProperty(ref _namingCase, value);
                GlobalData.SettingInfo.NamingCase = (NamingCase)value;
            }

        }


    }
}
