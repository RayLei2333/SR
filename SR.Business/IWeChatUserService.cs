using SR.DTO;
using SR.WeChat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SR.Business
{
    public interface IWeChatUserService
    {
        ResponseModel Login(string userName, string pwd);

        bool NotfoundOrAddUser(CorpUserResult wechatUser);

        ResponseModel GetUserLv();
    }
}
