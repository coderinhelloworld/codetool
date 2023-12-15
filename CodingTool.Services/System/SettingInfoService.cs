using CodingCommon.Extentions;
using CodingTool.Models.Enums.System;
using CoodingTool.Data;
using CoodingTool.Data.models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingTool.Services.System
{
    public class SettingInfoService
    {
        public Settings GetSettings()
        {
            using (var context = new SqlLiteDbContext())
            {
                var info = context.SettingInfo.FirstOrDefault();
                if (info == null)
                {
                    return new Settings
                    {
                        NamingCase = NamingCase.CamelCase
                    };
                }
                else
                {
                    return info.Parameters.DeserializeObject<Settings>();
                }
            };
        }



        public SettingInfo GetSettingInfo()
        {
            using (var context = new SqlLiteDbContext())
            {
                return context.SettingInfo.FirstOrDefault();
            };
        }

        public void SaveSettingInfo(SettingInfo settingInfo)
        {
            using (var context = new SqlLiteDbContext())
            {
                if(settingInfo.Id == null)
                {
                    settingInfo.Id = Guid.NewGuid().ToString();
                    context.Add(settingInfo);
                }
                else
                {
                    context.Update(settingInfo);
                }          
                context.SaveChanges();
            };

        }
  
    }
}
