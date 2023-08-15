using CodingTool.Entity;
using CodingTool.ViewModels.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodingTool.Enums;

namespace CodingTool.Helpers
{
    public static class SettingHelper
    {
        public static List<Settings> GetLocalSettings(SettingType type)
        {
            var pathPrefix = Directory.GetCurrentDirectory();
            var settingsStr = File.ReadAllText(pathPrefix + "/Setting/Settings.txt");
            var res = JsonConvert.DeserializeObject<List<Settings>>(settingsStr);
            if (res == null)
            {
                return new List<Settings>()
                {
                };
            }
            return res.Where(x => x.Type == type.ToString()).ToList();
        }


        public static void SaveSetting(string  jsonContent,SettingType  type)
        {
            var pathPrefix = Directory.GetCurrentDirectory();
            var directory = pathPrefix + "/Setting";
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }
            var settingFilePath= pathPrefix + "/Setting/Settings.txt";
            var settingsStr = "";
            if (File.Exists(settingFilePath))
            {
                settingsStr = File.ReadAllText(pathPrefix + "/Setting/Settings.txt");
            }
            else
            {
                settingsStr= "[]";
            }
            
     
            var res = JsonConvert.DeserializeObject<List<Settings>>(settingsStr);
            if (res == null)
            {
                res = new List<Settings>();
            }
            var setting = res.FirstOrDefault(x => x.Type == type.ToString());
            if (setting == null)
            {
                res.Add(new Settings()
                {
                    Type = type.ToString(),
                    JsonString = jsonContent
                });
            }
            else
            {
                setting.JsonString = jsonContent;
            }
            File.WriteAllText(pathPrefix + "/Setting/Settings.txt", JsonConvert.SerializeObject(res));
        }

    }
}
