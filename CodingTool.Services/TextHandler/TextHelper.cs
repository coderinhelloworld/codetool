using CoodingTool.Data.models.dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CodingTool.Services.TextHandler
{
    public static class TextHelper
    {

        //替换sql中的参数
        public static string GetFullSql(string input)
        {

            var inputText = input+" ";
            //v3中参数替换
            if (inputText.Contains("[Parameters=["))
            {

                var parameterList = new List<SqlParameter>();
                var sqlParameterString = inputText.Substring(0, inputText.LastIndexOf(']') + 1);

                var sqlStr = inputText.Replace(sqlParameterString, "");

                sqlParameterString = sqlParameterString.Replace("[Parameters=[", "");
                var parameterOriginalList = sqlParameterString.Split(',');

                foreach (var item in parameterOriginalList)
                {
                    var str = item;
                    if (str.Contains("Command"))
                    {
                        continue;
                    }
                    if (str.Contains("("))
                    {
                        str = item.Substring(0, item.LastIndexOf("("));
                    }
                    str = str.TrimEnd('"');
                    str = str.Replace("]", "");

                    var keyAndValue = str.Split('=');


                    parameterList.Add(new SqlParameter
                    {
                        Key = keyAndValue[0],
                        Value = keyAndValue[1]
                    });

                }
                foreach (var item in parameterList)
                {
                    sqlStr = sqlStr.Replace(item.Key, item.Value);
                }
                string pattern = @"(?i)\bLIMIT\b(.*)";

                var matches = Regex.Matches(sqlStr, pattern);
                if (matches.Count > 0)
                {

                    foreach (var item in matches)
                    {

                        var handleStr = item.ToString().Replace("'", " ");

                        sqlStr = sqlStr.Replace(item.ToString(), handleStr);

                    }

                }
            }


            //Merit,V2参数替换
            //删除inputText中的所有的[符号,保留最后一个
            var inputTextList = inputText.Split(new string[] { "[" }, StringSplitOptions.RemoveEmptyEntries).ToList();
            var lastStr = "[" + inputTextList.Last();
            inputTextList.Remove(inputTextList.Last());
            inputText = string.Join("", inputTextList) + lastStr;
            //删除inputText中的所有的]符号,保留最后一个
            inputTextList = inputText.Split(new string[] { "]" }, StringSplitOptions.RemoveEmptyEntries).ToList();
            inputText = string.Join("", inputTextList) + "]";
            var sqlString = inputText.Split('[')[0];
            var paramList = inputText.Split('[')[1].Replace("]", "").Split('，');
            var count = 1;
            if (inputText.Contains("@p0"))
            {
                count = 0;
            }
            foreach (var item in paramList)
            {
                var replaceStr = "@p" + count + ",";
                var replaceStr2 = "@p" + count + "";
                var replaceStr3 = "@p" + count + ")";
                var afterReplaceStr = "'" + item + "'" + ",";
                var afterReplaceStr2 = "'" + item + "'" + " ";
                var afterReplaceStr3 = "'" + item + "'" + ")";
                if (count == paramList.Count())
                {
                    replaceStr = "@p" + count;
                    afterReplaceStr = "'" + item + "'";
                }
                sqlString = sqlString.Replace(replaceStr, afterReplaceStr);
                sqlString = sqlString.Replace(replaceStr3, afterReplaceStr3);
                sqlString = sqlString.Replace(replaceStr2, afterReplaceStr2);
          
                count++;
            }
             return sqlString;

        }
    }
}
