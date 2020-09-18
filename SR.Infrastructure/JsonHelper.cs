using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SR.Infrastructure
{
    /// <summary>
    /// Json数据化
    /// </summary>
    public static class JsonHelper
    {
        /// <summary>
        /// 对象转为json
        /// </summary>
        /// <param name="data">数据对象</param>
        /// <param name="timeFormat">日期格式化</param>
        /// <param name="camelCase">是否启用驼峰命名</param>
        /// <returns></returns>
        public static string ToJson(object data, string timeFormat = "yyyy-MM-dd HH:mm:ss", bool camelCase = true)
        {
            //IsoDateTimeConverter timeConverter = new IsoDateTimeConverter();
            ////这里使用自定义日期格式，如果不使用的话，默认是ISO8601格式     
            //timeConverter.DateTimeFormat = timeFormat;
            JsonSerializerSettings settings = new JsonSerializerSettings
            {
                Formatting = Formatting.Indented,
                DateFormatString = timeFormat
            };
            if (camelCase)
                settings.ContractResolver = new Newtonsoft.Json.Serialization.CamelCasePropertyNamesContractResolver();
            return JsonConvert.SerializeObject(data,settings);
        }

        public static T ToObject<T>(string json) where T : class
        {
            T t = JsonConvert.DeserializeObject<T>(json);
            return t;
        }

        public static IEnumerable<T> ToObjectList<T>(string json) where T : class
        {
            List<T> list = JsonConvert.DeserializeObject<List<T>>(json);
            return list;
        }
    }
}
