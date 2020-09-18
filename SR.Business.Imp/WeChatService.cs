using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SR.Infrastructure;
using SR.WeChat;

namespace SR.Business.Imp
{
    public class WeChatService : IWeChatService
    {
        private static string TokenCacheKey { get { return "CorpToken"; } }



        public TokenResult GetToken()
        {
            TokenResult token = CacheHelper.GetApplication<TokenResult>(TokenCacheKey);
            if (token == null)
            {
                //reget
                token = this.GetNewToken();
            }
            else
            {
                int lastMinutes = Convert.ToInt32((token.ExpiresTime - DateTime.Now).TotalMinutes);
                if (lastMinutes < 20)
                {
                    //reget
                    token = this.GetNewToken();
                }
            }
            return token;
        }

        private TokenResult GetNewToken()
        {
            TokenService service = new TokenService();
            TokenResult result = service.GetToken(AppConfig.CorpId, AppConfig.CorpSecret);
            if (result.errcode != 0)
                return result;
            CacheHelper.SetAppliation(TokenCacheKey, result);
            return result;
        }


        public CodeResult CodeToUserId(string code)
        {
            TokenService service = new TokenService();
            TokenResult token = this.GetToken();
            CodeResult result = service.CodeToUser(token.access_token, code);
            return result;
        }

        public CorpUserResult GetUserInfo(string userid)
        {
            TokenResult token = this.GetToken();
            CorpUserService userService = new CorpUserService();
            CorpUserResult userResult = userService.GetUser(token.access_token, userid);
            return userResult;
            //TokenService tokenService = new TokenService();
            //TokenResult token = this.GetToken();
            //CodeResult codeResult = tokenService.CodeToUser(token.access_token, code);
            //CorpUserService userService = new CorpUserService();
            //CorpUserResult userResult = userService.GetUser(codeResult.UserId);
            //throw new NotImplementedException();
        }
    }
}
