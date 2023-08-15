﻿using CodingTool.Base;
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

namespace CodingTool.ViewModels
{
    public class MainViewModel : ViewModelBase
    {

        public MainViewModel()
        {
            _generateCodeModel = new GenerateCodeModel();
            CodeGenerateCommand = new CommandBase(_ =>
            {
                CodeGenerate.Generate(_generateCodeModel);
            });
            Yktv2SqlGenerateCommand = new CommandBase(_ =>
            {
                OutputText= CodeGenerate.Yktv2SqlGenerateByClass(InputText);
            });
            SaveSettingCommand = new CommandBase(_ =>
            {
                SettingHelper.SaveSetting(_generateCodeModel.SerializeObject(),SettingType.代码生成);
            });
            ImgToIcoCommand = new CommandBase(_ =>
            {
                ImageFunctions.ImageToIco(InputText);
            });
            ChooseFileCommand = new CommandBase(_ =>
            {

                InputText= SelectFileWpf();
            });
            GenerateTestMethodsCommand=new CommandBase(_ =>
            {
                OutputText = CodeGenerate.GenerateTestMethods(InputText);
            });
        }
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

        /// <summary>
        ///生成代码命令及相关字段
        /// </summary>

        public CommandBase CodeGenerateCommand { get; }
        public CommandBase Yktv2SqlGenerateCommand { get; }
        public CommandBase ImgToIcoCommand { get; }
        public CommandBase ChooseFileCommand { get; }
        
        public CommandBase GenerateTestMethodsCommand { get; }




        private GenerateCodeModel _generateCodeModel;
    
        public GenerateCodeModel GenerateCodeModel
        {
            get => _generateCodeModel;
            set => SetProperty(ref _generateCodeModel, value);
        }


        ///保存配置
        ///
        public CommandBase SaveSettingCommand { get; }
        
        private ObservableCollection<ReceipViewModel> _ReceipItems = new ObservableCollection<ReceipViewModel>();
        public ObservableCollection<ReceipViewModel> ReceipItems
        {
            get
            {
                return _ReceipItems;
            }
            set
            {
                if (value == null || value.Count < 1)
                    return;
                //清空原先的列表
                // thisWindow.Dispatcher.BeginInvoke(DispatcherPriority.Normal, (ThreadStart)delegate ()
                // {
                //     _ReceipItems.Clear();
                //     //向列表中加入单词块
                //     foreach (ReceipViewModel poppingWord in value)
                //     {
                //         _ReceipItems.Add(poppingWord);
                //     }
                // });

            }
        }

    }
}