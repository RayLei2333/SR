using SR.WeChat.NetWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SR.WeChat
{
    public class CorpUserService
    {
        public CorpUserResult GetUser(string accessToken, string userId)
        {
            string url = $"https://qyapi.weixin.qq.com/cgi-bin/user/get?access_token={accessToken}&userid={userId}";

            CorpUserResult result = WXApi.Request<CorpUserResult>(url, null, Method.GET);
            return result;
        }
    }
}
