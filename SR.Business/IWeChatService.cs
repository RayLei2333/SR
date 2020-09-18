using SR.WeChat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SR.Business
{
    public interface IWeChatService
    {
        TokenResult GetToken();

        CodeResult CodeToUserId(string code);

        CorpUserResult GetUserInfo(string userid);
    }
}
