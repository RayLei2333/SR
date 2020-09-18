using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SR.Infrastructure;
using SR.Model;
using SR.Respository;
using SR.WeChat;

namespace SR.Business.Imp
{
    public class WeChatUserService : IWeChatUserService
    {

        public bool NotfoundOrAddUser(CorpUserResult wechatUser)
        {
            using (UserResp resp = new UserResp())
            {
                int count = resp.GetUserCount(wechatUser.open_userid);
                if (count <= 0)
                {
                    //add 
                    T_WeChat_User user = this.CorpUserToWechatUser(wechatUser);
                    int result = (int)resp.Insert(user);
                    return result > 0;
                }
                return true;
            }
        }

        private T_WeChat_User CorpUserToWechatUser(CorpUserResult corpUser)
        {
            T_WeChat_User user = new T_WeChat_User()
            {
                Id = Guid.NewGuid().ToString(),
                UserId = corpUser.userid,
                OpenUserId = corpUser.open_userid,
                Name = corpUser.name,
                Position = corpUser.position,
                Mobile = corpUser.mobile,
                Gender = corpUser.gender,
                Email = corpUser.email,
                //Department = corpUser.de
                Alias = corpUser.alias,
                EnglishName = corpUser.english_name,
                Status = corpUser.status,
                CreateTime = DateTime.Now,
                UpdateTime = DateTime.Now
            };
            return user;
        }
    }
}
