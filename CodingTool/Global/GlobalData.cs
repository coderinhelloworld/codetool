using System.Collections.Generic;
using System.Windows.Controls;
using AutoMapper;
using CodingTool.Helpers;
using CodingTool.Models.Enums.System;
using CodingTool.Views.Controls;

namespace CodingTool.Global;

public static class GlobalData
{
    public static List<UserControlMenuItem> MenuListViews { get; set; } = new List<UserControlMenuItem>();
    public static Loading Loading { get; set; }

    public static DialogHelper DialogHelper = new DialogHelper();

    public static MainWindow MainW { get; set; }

    public static Settings SettingInfo { get; set; }
    public static IMapper Mapper { get; set; }
}

