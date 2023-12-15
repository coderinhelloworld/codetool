using CodingCommon.Extentions;
using CodingTool.Models.Enums.System;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingTool.Services.System
{
    public static class NamingService
    {
        public static string GetNamingString(string input)
        {
            var setting = new SettingInfoService().GetSettings();
            switch (setting.NamingCase)
            {

                case NamingCase.TitleCase:
                    return input.ToNamingTitleCase();
                case NamingCase.CamelCase:
                    return input.ToNamingCamelCase();
                case NamingCase.LowerKebabCase:
                    return input.ToNamingLowerKebabCase();
                case NamingCase.LowerSnakeCase:
                    return input.ToNamingLowerSnakeCase();
                case NamingCase.UpperKebabCase:
                    return input.ToNamingUpperKebabCase();
                case NamingCase.UpperSnakeCase:
                    return input.ToNamingUpperSnakeCase();
                default: return input;
            }

        }

    }
}
