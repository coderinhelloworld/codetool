using CodingTool.Services.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CodingTool.Services.Extensions
{
    public static partial class StringExtention
    {


        public static string ToSystemNamingCase(this string input)
        {
            return NamingService.GetNamingString(input);
        }
    }
}
