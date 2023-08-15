using CodingTool.Extentions;
using CodingTool.ViewModels.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CodingTool.Functions
{
    public static class CodeGenerate
    {
        public static void Generate(GenerateCodeModel generateCodeModel)
        {
            var currentPath = Directory.GetCurrentDirectory();
            var projectRootPath = ExtractPathBeforeBin(currentPath);
            string yktBoTemplateContent =
                File.ReadAllText($@"{projectRootPath}\Templates\Yktv2Layers\YKT_V2_BO.cshtml");
            var yktV2BoContent = new RazorEngineCore.RazorEngine().Compile(yktBoTemplateContent).Run(generateCodeModel);


            string yktDaoTemplateContent =
                File.ReadAllText($@"{projectRootPath}\Templates\Yktv2Layers\YKT_V2_DAO.cshtml");
            var yktV2DaoContent =
                new RazorEngineCore.RazorEngine().Compile(yktDaoTemplateContent).Run(generateCodeModel);

            string yktHandlerTemplateContent =
                File.ReadAllText($@"{projectRootPath}\Templates\Yktv2Layers\YKT_V2_Handler.cshtml");
            var yktV2HandlerContent = new RazorEngineCore.RazorEngine().Compile(yktHandlerTemplateContent)
                .Run(generateCodeModel);
            var path = currentPath + @"\CodeOutput";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            File.WriteAllText(path + "/yktV2BoContent.cs", yktV2BoContent);
            File.WriteAllText(path + "/yktV2DaoContent.cs", yktV2DaoContent);
            File.WriteAllText(path + "/yktV2HandlerContent.cs", yktV2HandlerContent);
            //打开路径
            System.Diagnostics.Process.Start("explorer.exe", path);
        }

        static string ExtractPathBeforeBin(string inputPath)
        {
            int binIndex = inputPath.IndexOf("\\bin", StringComparison.OrdinalIgnoreCase);
            if (binIndex != -1)
            {
                return inputPath.Substring(0, binIndex);
            }
            else
            {
                return inputPath;
            }
        }


        public static string Generate(GenerateCodeModel generateCodeModel, string inputText)
        {
            var tableNameList = generateCodeModel.TableName.Split(' ');
            var tableExplain = "";
            if (tableNameList.Count() > 1)
            {
                tableExplain = tableNameList.Where(x => !x.IsNullOrEmpty()).Last();
            }

            var tableName = tableNameList[0].ToPascal();


            var col = 1;
            var sb = new StringBuilder();
            var textList = inputText.Split(new string[] { "\r\n" }, StringSplitOptions.None).ToList();
            if (textList.Count() == 1)
            {
                textList = inputText.Split(new string[] { "\n" }, StringSplitOptions.None).ToList();
            }

            sb.AppendLine($@"[Table(""{tableName.ToUnderlineNaming().ToUpper()}"", ""{tableExplain}"")]
[Serializable]
public class {tableName}Eo : FrameEo {{ ");
            foreach (var text in textList)
            {
                var items = text.Split('\t').ToList();
                if (items.Count == 1)
                {
                    items = text.Split(' ').ToList();
                }

                items = items.Where(x => x != "").ToList();
                if (items.Count > 0)
                {
                    sb.AppendLine("/// <summary>");
                    sb.AppendLine($"/// {items[col]}");
                    sb.AppendLine("/// </summary>");
                    sb.AppendLine(
                        $"[Column(\"{items[0].ToPascal().ToUnderlineNaming().ToUpper()}\", \"{items[col]}\")]");
                    sb.AppendLine($" public string {items[0].ToPascal()} {{get;set;}}");
                    sb.AppendLine();
                }
            }

            sb.AppendLine("}");

            var entityClassStr = sb.ToString();
            GenerateEntityClass(generateCodeModel, entityClassStr);
            return sb.ToString();
        }


        private static void GenerateEntityClass(GenerateCodeModel generateCodeModel, string classStr)
        {
            //根据generateCodeModel.EntityPath生成txt文件

            if (!Directory.Exists(generateCodeModel.EntityPath))
            {
                Directory.CreateDirectory(generateCodeModel.EntityPath);
            }

            File.WriteAllText(generateCodeModel.EntityPath + "/" + generateCodeModel.TableName + ".cs", classStr);
        }


        /// <summary>
        /// 生成sql语句
        /// </summary>
        /// <param name="inputText"></param>
        /// <returns></returns>
        public static string Yktv2SqlGenerateByClass(string inputText)
        {
            return GenerateCreateTableSqlFromString(inputText);
        }

        private static string GenerateCreateTableSqlFromString(string classString)
        {
            string pattern = @"\[Table\(""(.*?)""";
            Match match = Regex.Match(classString, pattern);
            if (!match.Success)
            {
                throw new ArgumentException("Table attribute not found in the input string.");
            }

            string tableName = match.Groups[1].Value;

            StringBuilder sqlBuilder = new StringBuilder();
            sqlBuilder.AppendFormat("CREATE TABLE {0} (", tableName);

            pattern = @"\[Column\(""(.*?)""";
            MatchCollection columnMatches = Regex.Matches(classString, pattern);
            foreach (Match columnMatch in columnMatches)
            {
                string columnName = columnMatch.Groups[1].Value;
                //正则表达式获取"public"+任意内容+columnName
                string pattern2 = @"public.*?" + columnName.ToPascal();
                Match match2 = Regex.Match(classString, pattern2);
                if (!match2.Success)
                {
                    throw new ArgumentException("Column attribute not found in the input string.");
                }

                var typeStr = match2.Value;

                string dataType = "NVARCHAR(255)"; // Default data type

                if (typeStr.ToUpper().Contains("DATETIME"))
                    dataType = "DATETIME";
                else if (typeStr.ToUpper().Contains("INT"))
                    dataType = "INT";
                else if (typeStr.ToUpper().Contains("STRING"))
                {
                    dataType = "NVARCHAR(255)";
                }
                else
                {
                    dataType = "INT";
                }

                sqlBuilder.AppendFormat("\n    {0} {1},", columnName, dataType);
            }

            sqlBuilder.Length--; // Remove the last comma
            sqlBuilder.Append("\n);");

            return sqlBuilder.ToString();
        }

        /// <summary>
        /// 生成测试方法
        /// </summary>
        /// <param name="inputText"></param>
        /// <returns></returns>
        public static string GenerateTestMethods(string inputText)
        {
            string input = inputText;
            var sb = new StringBuilder();
            string pattern = @".*public.*";
            MatchCollection matches = Regex.Matches(input, pattern, RegexOptions.Multiline);
            foreach (Match match in matches)
            {
                var fullMethodName = match.Value;
                if (fullMethodName.Contains("FrameDao"))
                {
                    continue;
                }

                // Define regular expressions for method extraction
                string methodPattern = @"(public.*)";
                string methodNamePattern = @"(\w+)\s*\(";
                string methodParamsPattern = @"\((.*?)\)";

                MatchCollection methodMatches = Regex.Matches(fullMethodName, methodPattern, RegexOptions.Singleline);
                foreach (Match methodMatch in methodMatches)
                {
                    string methodDeclaration = methodMatch.Groups[1].Value;
                    if (methodDeclaration.Contains("pdi)"))
                    {
                         continue;
                    }

                    Match methodNameMatch = Regex.Match(methodDeclaration, methodNamePattern);
                    string methodName = methodNameMatch.Groups[1].Value;

                    Match methodParamsMatch = Regex.Match(methodDeclaration, methodParamsPattern);
                    string methodParams = methodParamsMatch.Groups[1].Value;

                    // Split method parameters into individual parameters
                    string[] paramArray = methodParams.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                    List<string> paramTypes = new List<string>();
                    List<string> paramNames = new List<string>();
                    var paramStr = "";
                    sb.AppendLine("[TestMethod]");
                    sb.AppendLine($"public void {methodName}Test()");
                    sb.AppendLine(" {");

                    if (methodDeclaration.Contains("void"))
                    {
                        sb.Append($" _dao.{methodName}(");
                    }
                    else
                    {
                        sb.Append($"   var res = _dao.{methodName}(");
                    }

                    foreach (string param in paramArray)
                    {
                        string[] paramParts = param.Trim().Split(' ');
                        if (paramParts.Length == 2)
                        {
                            paramTypes.Add(paramParts[0]);
                            paramNames.Add(paramParts[1]);
                            if (paramParts[0].ToUpper().Contains("STRING"))
                            {
                                sb.Append("\"test\",");
                            }
                            else if (paramParts[0].ToUpper().Contains("INT"))
                            {
                                sb.Append("0,");
                            }else if(paramParts[0].ToUpper().Contains("DATETIME"))
                            {
                                sb.Append("DateTime.Now,");
                            }
                            else
                            {
                                sb.Append($"{paramParts[0]},");
                            }
                        }
                    }
                    //去除最后的,
                    var tempStr=sb.ToString().Substring(0, sb.ToString().Length - 1);
                    sb =new StringBuilder().Append(tempStr);
                    
                    sb.AppendLine($");");
                    if (methodDeclaration.Contains("DataTable"))
                    {
                        sb.AppendLine("   var count = res.Rows.Count;");
                    }

                    if (methodDeclaration.Contains("List<"))
                    {
                        sb.AppendLine("var count = res.Count;");
                    }

             
                    sb.AppendLine("  }");
                    sb.AppendLine("");
                                 


                    Console.WriteLine("Method Name: " + methodName);
                    Console.WriteLine("Method Parameters: " + string.Join(", ", paramNames));
                    Console.WriteLine("Parameter Types: " + string.Join(", ", paramTypes));
                    Console.WriteLine();
                }
            }

            return sb.ToString();
        }
    }
}