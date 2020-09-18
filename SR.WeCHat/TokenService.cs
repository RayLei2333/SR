using SR.WeChat.NetWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SR.WeChat
{
    public class TokenService
    {
        /// <summary>
        /// 获取微信企业号Token
        /// </summary>
        /// <param name="corpid">企业ID</param>
        /// <param name="corpSecret">应用的凭证密钥</param>
        /// <returns></returns>
        public TokenResult GetToken(string corpid, string corpSecret)
        {
            string url = $"https://qyapi.weixin.qq.com/cgi-bin/gettoken?corpid={corpid}&corpsecret={corpSecret}";
            TokenResult result  = WXApi.Request<TokenResult>(url, null, Method.GET);
            return result;
        }

        public CodeResult CodeToUser(string accessToken,string code)
        {
            string url = $"https://qyapi.weixin.qq.com/cgi-bin/user/getuserinfo?access_token={accessToken}&code={code}";

            CodeResult result = WXApi.Request<CodeResult>(url,null,Method.GET);
            return result;
        }
    }
}
