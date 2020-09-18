using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SR.WeChat.NetWork
{
    internal class WXApi
    {
        public static string Request(string url, object data, Method method, Format format = Format.JSON)
        {
            string result = null;
            if (method == Method.POST)
                result = Post.SendJson(url, data, format);
            else
                result = Get.Send(url);
            return result;
        }

        public static T Request<T>(string url, object data, Method method, Format format = Format.JSON) where T : class, new()
        {
            string result = Request(url, data, method, format);
            return SerializerHelper.ToObject<T>(result);
        }
    }

    internal enum Method
    {
        GET = 1,
        POST = 2,
    }

    internal enum Format
    {
        JSON = 1,
        XML = 2
    }
}
