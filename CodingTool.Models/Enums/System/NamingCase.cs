using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CodingTool.Models.Enums.System
{



    public enum NamingCase
    {
        [Description("TitleCase")]
        TitleCase = 0,

        [Description("camelCase")]
        CamelCase = 1,

        [Description("lower_snake_case")]
        LowerSnakeCase = 2,

        [Description("UPPER_SNAKE_CASE")]
        UpperSnakeCase = 3,

        [Description("lower-keba-case")]
        LowerKebabCase = 4,

        [Description("UPPER-KEBA-CASE")]
        UpperKebabCase = 5
    }

}
