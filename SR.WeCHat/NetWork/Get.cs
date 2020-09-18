using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SR.WeChat.NetWork
{
    internal static class Get
    {
        public static string Send(string url)
        {
            using (HttpClient client = new HttpClient())
            {
                string result = client.GetStringAsync(url).Result;
                return result;
            }
        }
    }
}
