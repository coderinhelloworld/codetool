using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public static class ObjectExtension
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
