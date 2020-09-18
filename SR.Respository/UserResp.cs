using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SR.Respository
{
    /// <summary>
    /// 手机端用户操作
    /// </summary>
    public class UserResp : BaseRespository
    {
        /// <summary>
        /// 查询用户是否存在
        /// </summary>
        /// <param name="openUserId">微信企业号全局id</param>
        /// <returns></returns>
        public int GetUserCount(string openUserId)
        {
            string sql = "SELECT COUNT(1) FROM T_WeChat_User WHERE OpenUserId=@userid";
            int result = base.Get<int>(sql, new
            {
                userid = openUserId
            });
            return result;
        }
    }
}
