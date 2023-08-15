using Newtonsoft.Json;

namespace CodingTool.Extentions;

public static class ExtensionObject
{
    /// <summary>
    /// 将对象转化为JSON格式的字符串
    /// </summary>
    /// <param name="obj"></param>
    /// <returns>JSON格式的字符串</returns>
    public static string SerializeObject(this object obj)
    {
        return JsonConvert.SerializeObject(obj);
    }
}