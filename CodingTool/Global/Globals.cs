using System.Collections.Generic;
using System.Windows.Controls;
using CodingTool.Helpers;
using CodingTool.Views.Controls;

namespace CodingTool.Global;

public static class Globals
{
    public static List<UserControlMenuItem> MenuListViews { get; set; } = new List<UserControlMenuItem>();
    public static Loading Loading { get; set; }

    public static DialogHelper DialogHelper = new DialogHelper();

    public static MainWindow MainW { get; set; }
}