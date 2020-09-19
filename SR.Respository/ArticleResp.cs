using Dapper;
using SR.DTO.Resp;
using SR.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SR.Respository
{
    public class ArticleResp : BaseRespository
    {
        public int GetCount(string keyword, DateTime? beginTime, DateTime? endTime, string area)
        {
            string sql = @"SELECT COUNT(1) FROM T_Article a
                            LEFT JOIN T_Article_Detail b on b.ArticleId = a.Id
                            WHERE a.IsDelete = 0";
            DynamicParameters sqlPara = new DynamicParameters();
            if (!string.IsNullOrEmpty(keyword))
                sql += $" AND a.Title LIKE '%{keyword}%' ";
            if (beginTime != null && endTime != null && beginTime.HasValue && endTime.HasValue)
            {
                sql += " AND CONVERT(CHAR(10),b.BeginTime,120) >= @begin AND CONVERT(CHAR(10),b.BeginTime,120) <= @end ";
                sqlPara.Add("@begin", beginTime.Value);
                sqlPara.Add("@end", endTime.Value);
            }
            if (string.IsNullOrEmpty(area))
            {
                sql += " AND b.Region=@area";
                sqlPara.Add("@area", area);
            }
            int count = base.Get<int>(sql, sqlPara);
            return count;
        }

        public T_Article GetArticleById(string id)
        {
            string sql = "SELECT * FROM T_Article WHERE Id=@id";
            T_Article result = base.Get<T_Article>(sql, new { id });
            return result;
        }

        public IEnumerable<T_Article> GetArticleList(int pageIndex, int pageSize, string keyword, DateTime? beginTime, DateTime? endTime, string area, string model)
        {
            string orderbyCondition = null;
            if (model.ToLower() == "pc")
                orderbyCondition = "a.CreateTime";
            else if (model.ToLower() == "moblie")
                orderbyCondition = "b.BeginTime";

            string sql = @"SELECT TOP (@size) * FROM (SELECT ROW_NUMBER()OVER(ORDER BY {0} desc)rownumber, a.* 
                             FROM T_Article a 
                             LEFT JOIN T_Article_Detail b ON b.ArticleId = a.Id
                             WHERE a.IsDelete = 0 AND b.IsDelete = 0 {1}) c
                            WHERE rownumber>@number";
            DynamicParameters sqlPara = new DynamicParameters();
            //sqlPara.Add("");
            string condtion = "";
            if (!string.IsNullOrEmpty(keyword))
                condtion += $" AND a.Title LIKE '%{keyword}%' ";
            if (beginTime != null && endTime != null && beginTime.HasValue && endTime.HasValue)
            {
                condtion += " AND CONVERT(CHAR(10),b.BeginTime,120) >= @begin AND CONVERT(CHAR(10),b.BeginTime,120) <= @end ";
                sqlPara.Add("@begin", beginTime.Value);
                sqlPara.Add("@end", endTime.Value);
            }
            if (string.IsNullOrEmpty(area))
            {
                condtion += " AND b.Region=@area";
                sqlPara.Add("@area", area);
            }
            sql = string.Format(sql, orderbyCondition, condtion);
            sqlPara.Add("@size", pageSize);
            sqlPara.Add("@number", (pageIndex - 1) * pageSize);
            IEnumerable<T_Article> result = base.Query<T_Article>(sql, sqlPara);
            return result;
        }

        public IEnumerable<T_Article_Detail> GetArticleDetailByArticle(string articleId)
        {
            string sql = "SELCET * FROM T_Article_Detail WHERE ArticleId=@articleId AND IsDelete=0";
            IEnumerable<T_Article_Detail> result = base.Query<T_Article_Detail>(sql, new { articleId });
            return result;

        }

        public IEnumerable<T_Article_Detail> GetArticleDetail(IEnumerable<string> articleId)
        {
            string sql = "SELECT * FROM T_Article_Detail WHERE IsDelete=0 AND (";
            List<string> sqlAppend = new List<string>();
            foreach (var item in articleId)
            {
                sqlAppend.Add($"ArticleId='{item}'");
            }
            sql += string.Join(" OR ", sqlAppend.ToArray());
            sql += ")";
            IEnumerable<T_Article_Detail> result = base.Query<T_Article_Detail>(sql);
            return result;
        }

        public int DeleteArticle(string id)
        {
            string sql = @"UPDATE T_Article SET IsDelete=1,UpdateDate=GETDATE() WHERE Id=@id AND IsDelete=0";
            int result = base.Execute(sql, new { id });
            return result;
        }

        public int DeleteDetailByArticle(string articleId)
        {
            string sql = @"UPDATE T_Article_Detail SET IsDelete=1,UpdateDate=GETDATE() WHERE ArticleId=@articleId AND IsDelete=0";
            int result = base.Execute(sql, new { articleId });
            return result;
        }

        public int CancelSign(string userId, string articleId, string detailId)
        {
            string sql = @"UPDATE T_Article_Sign SET IsDelete=1,UpdateTime=GETDATE() WHERE UserId=@userId AND ArticleId=@articleId AND DetailId=@detailId";
            int result = base.Execute(sql, new
            {
                userId,
                articleId,
                detailId
            });
            return result;

        }

        public int GetUserIsSigned(string userId, string articleId, string detailId)
        {
            string sql = "SELECT COUNT(1) FROM T_Article_Signed WHERE IsDelete=0 AND UserId=@userId AND ArticleId=@articleId AND DetailId=@detailId";
            int result = base.Get<int>(sql, new { userId, articleId, detailId });
            return result;
        }

        public IEnumerable<UserSignQuery> GetUserIsSign(string userId, string articleId)
        {
            string sql = @"SELECT b.ArticleId,b.Id 'DetailId',COUNT(a.UserId) 'count' FROM T_Article_Sign a
                            RIGHT JOIN T_Article_Detail b on a.DetailId = b.Id
                            WHERE a.ArticleId=@articleId AND b.ArticleId=@articleId AND a.UserId=@userId AND a.IsDelete=0
                            GROUP BY b.ArticleId,b.Id";
            IEnumerable<UserSignQuery> result = base.Query<UserSignQuery>(sql, new { articleId, userId });
            return result;
        }

        public IEnumerable<UserSignQuery> GetUserIsSigned(string userId, string articleId)
        {
            string sql = @"SELECT b.ArticleId,b.Id 'DetailId',COUNT(a.UserId) 'count' FROM T_Article_Signed a
                            RIGHT JOIN T_Article_Detail b on a.DetailId = b.Id
                            WHERE a.ArticleId=@articleId AND b.ArticleId=@articleId AND a.UserId=@userId AND a.IsDelete=0
                            GROUP BY b.ArticleId,b.Id";
            IEnumerable<UserSignQuery> result = base.Query<UserSignQuery>(sql, new { articleId, userId });
            return result;
        }

        public IEnumerable<T_Article_Sign> GetNotSignDetail(IEnumerable<string> userList, string articleId)
        {
            string sql = @"SELECT * FROM T_Article_Sign WHERE UserId NOT IN(select userid from T_Article_Signed where ArticleId=@articleId)
                            AND ArticleId=@articleId AND IsDelete =0";
            DynamicParameters sqlPara = new DynamicParameters();
            if (userList.Count() > 0)
            {
                sql += " AND (";
                List<string> sqlAppend = new List<string>();
                int index = 1;
                foreach (var item in userList)
                {
                    sqlAppend.Add($"UserId=@userId{index}");
                    sqlPara.Add($"@userId{index}", item);
                    index++;
                }
                sql += string.Join(" OR ", sqlAppend.ToArray());
                sql += ")";
            }

            sqlPara.Add("@articleId", articleId);
            IEnumerable<T_Article_Sign> result = base.Query<T_Article_Sign>(sql, sqlPara);
            return result;
        }

        public IEnumerable<T_Article_Comment> GetCommentList(int pageIndex, int pageSize, string articleId)
        {
            string sql = @"SELECT TOP (@size) * FROM (SELECT ROW_NUMBER()OVER(ORDER BY a.CreateTime desc)rownumber, a.* FROM T_Article_Comment a  WHERE ArticleId=@articleId) c 
                            WHERE rownumber>@number";
            IEnumerable<T_Article_Comment> result = base.Query<T_Article_Comment>(sql, new
            {
                articleId,
                size = pageSize,
                number = (pageIndex - 1) * pageSize
            });

            return result;
        }

        public int GetCommentCount(string articleId)
        {
            string sql = @"SELECT COUNT(1) FROM T_Article_Comment WHERE ArticleId=@articleId";
            int result = base.Get<int>(sql, new { articleId });
            return result;
        }

        public IEnumerable<UserSignQuery> GetUserSignArticleList(int pageIndex, int pageSize, string userId)
        {
            string sql = @"SELECT TOP (@size) * FROM (SELECT ROW_NUMBER()OVER(ORDER BY b.BeginTime desc)rownumber, a.Id,a.HeaderImg,a.Contents,b.BeginTime
                            FROM T_Article a
                            LEFT JOIN T_Article_Detail b ON b.ArticleId = a.Id
                            LEFT JOIN T_Article_Sign c ON c.DetailId = b.Id
                            WHERE c.UserId=@userId AND c.IsDelete = 0 AND a.IsDelete = 0 AND b.IsDelete = 0) d
                            WHERE rownumber>@number";
            IEnumerable<UserSignQuery> result = base.Query<UserSignQuery>(sql, new
            {
                userId,
                size = pageSize,
                number = (pageIndex - 1) * pageSize
            });
            return result;
        }

        public int GetUserSignCount(string userId)
        {
            string sql = @"SELECT COUNT(1) FROM T_Article a
                    LEFT JOIN T_Article_Detail b ON b.ArticleId = a.Id
                    LEFT JOIN T_Article_Sign c ON c.DetailId = b.Id
                    WHERE c.UserId=@userId AND c.IsDelete = 0 AND a.IsDelete = 0 AND b.IsDelete = 0";
            int result = base.Get<int>(sql, new { userId });
            return result;
        }

        public IEnumerable<UserSignQuery> GetUserSignedArticleList(int pageIndex, int pageSize, string userId)
        {
            string sql = @"SELECT TOP (@size) * FROM (SELECT ROW_NUMBER()OVER(ORDER BY b.BeginTime desc)rownumber, a.Id,a.HeaderImg,a.Contents,b.BeginTime
                            FROM T_Article a
                            LEFT JOIN T_Article_Detail b ON b.ArticleId = a.Id
                            LEFT JOIN T_Article_Signed c ON c.DetailId = b.Id
                            WHERE c.UserId=@userId AND c.IsDelete = 0 AND a.IsDelete = 0 AND b.IsDelete = 0) d
                            WHERE rownumber>@number";
            IEnumerable<UserSignQuery> result = base.Query<UserSignQuery>(sql, new
            {
                userId,
                size = pageSize,
                number = (pageIndex - 1) * pageSize
            });
            return result;
        }

        public int GetUserSignedCount(string userId)
        {
            string sql = @"SELECT COUNT(1) FROM T_Article a
                    LEFT JOIN T_Article_Detail b ON b.ArticleId = a.Id
                    LEFT JOIN T_Article_Signed c ON c.DetailId = b.Id
                    WHERE c.UserId=@userId AND c.IsDelete = 0 AND a.IsDelete = 0 AND b.IsDelete = 0";
            int result = base.Get<int>(sql, new { userId });
            return result;
        }
    }
}
