using CodingTool.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CodingTool.Functions
{
    public static  class TxtHandler
    {
        public static void FindChangeSqlInFuntions(string filePath )
        {
            if(string.IsNullOrEmpty(filePath) && !Directory.Exists(filePath))
            {
                throw new Exception("路径不存在");
            }
            var filePreList = new List<string>();
            filePreList.Add(".cs");
            var filePathList = FileHelper.GetDirAllFiles(filePath);
            foreach (var path in filePathList)
            {
                //获取path的后缀
                var filePre = Path.GetExtension(path);
                if (filePreList.Contains(filePre))
                {
                    var content = File.ReadAllText(path, FileHelper.GetType(path));

                    //逐行读取
                    var contentList = content.Split('\n');
                    var function =new StringBuilder();
                    var count = 1;
                    foreach (var txt in contentList)
                    {
                      
                        if (txt.Contains("public") || txt.Contains("private ") || contentList.Length==count)
                        {
                            var functionStr= function.ToString();

                            if(functionStr.Contains(".UsingRWS()") && (functionStr.ToLower().Contains("delete ") || functionStr.ToLower().Contains("update ")))
                            {
                                MsgHelper.AppendMsg(path.ToString());
                                MsgHelper.AppendMsg(function.ToString());
                                MsgHelper.AppendMsg("------------------------------------");
                            }
                            function = new StringBuilder();
                            function.AppendLine(txt);
                        }
                        else
                        {
                            function.AppendLine(txt);
                        }                        
                        count++;
                    }


                    
                }

            }

        }
    }
}
