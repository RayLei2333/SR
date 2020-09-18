using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace SR.WeChat.NetWork
{
    internal static class Post
    {
        public static string SendJson(string url, string data, Format format)
        {
            using (HttpContent content = new StringContent(data))
            {
                if (format == Format.JSON)
                    content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                if (format == Format.XML)
                    content.Headers.ContentType = new MediaTypeHeaderValue("application/xml");
                using (HttpClient client = new HttpClient())
                {
                    return client.PostAsync(url, content).Result.Content.ReadAsStringAsync().Result;
                }
            }
        }

        public static string SendJson(string url, object data, Format format)
        {
            return SendJson(url, SerializerHelper.ToJson(data), format);
        }
    }
}
