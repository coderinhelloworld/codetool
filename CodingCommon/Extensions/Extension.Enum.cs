using CodingTool.Models;
using CodingTool.Models.Enums.System;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingCommon.Extensions
{
    public static class EnumExtension
    {


        public static List<EnumItem> GetEnumElements(this Enum enumType)
        {
            List<EnumItem> list = new List<EnumItem>();
            foreach (Enum item in Enum.GetValues(enumType.GetType()))
            {               
                list.Add( new EnumItem()
                {
                    Description = GetDescriptionAttribute(item).ToString(),
                    Title=item.ToString(),
                    Enum= (NamingCase)Enum.Parse(typeof(NamingCase), item.ToString()),
                    Value = Convert.ToInt32(item)
                });
            }
            return list;
        }

        private static string GetDescriptionAttribute(Enum value)
        {
            var field = value.GetType().GetField(value.ToString());
            var attributes = (DescriptionAttribute[])field.GetCustomAttributes(typeof(DescriptionAttribute), false);

            return attributes.Length > 0 ? attributes[0].Description : value.ToString();
        }
    }
}
