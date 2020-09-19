using SR.Model;
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
        public T_Sys_User GetSystemUser(string userName,string pwd)
        {
            string sql = @"SELECT * FROM T_Sys_User WHERE UserName=@userName AND Pwd=@pwd";
            T_Sys_User user= base.Get<T_Sys_User>(sql,new { userName,pwd });
            return user;
        }


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


        public IEnumerable<T_WeChat_User> GetNotSignedUser(string articleId)
        {
            string sql = @"SELECT a.* FROM T_WeChat_User a
                            LEFT JOIN T_Article_Sign b on a.OpenUserId = b.UserId
                            WHERE b.UserId NOT IN (select userid from T_Article_Signed where ArticleId=@articleId)
                            AND b.IsDelete = 0 AND B.ArticleId=@articleId";
            IEnumerable<T_WeChat_User> result = base.Query<T_WeChat_User>(sql,new { articleId });
            return result;
        }

        public int GetUserSignedNumber(string userId)
        {
            string sql = @"SELECT COUNT(1) FROM T_Article_Signed WHERE IsDelete=0 AND UserId=@userId";
            int result = base.Get<int>(sql);
            return result;
        }
    }
}
