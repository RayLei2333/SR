using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SR.Infrastructure
{
    public static class JsonHelper
    {
        public static string ToJson(object data,string timeFormat = "yyyy-MM-dd HH:mm:ss")
        {
            IsoDateTimeConverter timeConverter = new IsoDateTimeConverter();
            //这里使用自定义日期格式，如果不使用的话，默认是ISO8601格式     
            timeConverter.DateTimeFormat = timeFormat;
            JsonSerializerSettings settings = new JsonSerializerSettings
            {
                Formatting = Formatting.Indented,
                DateFormatString = timeFormat
            };
            return JsonConvert.SerializeObject(data, Formatting.Indented, timeConverter);
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
