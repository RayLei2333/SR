using SR.DTO;
using SR.DTO.Req;
using SR.DTO.Resp;
using SR.Infrastructure;
using SR.Model;
using SR.Respository;
using SR.WeChat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SR.Business.Imp
{
    public class ArticleService : IArticleService
    {
        public ResponseModel GetArticle(string id)
        {
            using (ArticleResp resp = new ArticleResp())
            {
                T_Article article = resp.GetArticleById(id);
                IEnumerable<T_Article_Detail> details = resp.GetArticleDetailByArticle(id);
                ArticleDTO model = new ArticleDTO()
                {
                    Article = article,
                    Detail = details.ToList()
                };

                return new ResponseModel(model);
            }
        }

        public ResponseModel GetArticleList(ArticleQuery model)
        {
            using (ArticleResp resp = new ArticleResp())
            {
                int count = resp.GetCount(model.Keyword, model.BeginTime, model.EndTime, model.Area);
                IEnumerable<T_Article> articles = resp.GetArticleList(model.PageIndex, model.PageSize, model.Keyword, model.BeginTime, model.EndTime, model.Area, model.ReqSource);
                if (articles.Count() <= 0)
                    return new ResponseModel();
                IEnumerable<string> articleIdList = articles.Select(t => t.Id).AsEnumerable();
                IEnumerable<T_Article_Detail> details = resp.GetArticleDetail(articleIdList);

                List<ArticleDTO> results = new List<ArticleDTO>();
                foreach (var item in articles)
                {
                    ArticleDTO respModel = new ArticleDTO()
                    {
                        Article = item,
                        Detail = details.Where(t => t.ArticleId == item.Id).OrderByDescending(t => t.CreateTime).ToList()
                    };
                    results.Add(respModel);
                }

                return new ResponseModel(new { count = count, list = results });
            }
        }

        #region 新增&修改活动
        public ResponseModel AddOrUpdate(ArticleDTO model)
        {
            if (string.IsNullOrEmpty(model.Article.Id))
            {
                return this.AddArticle(model);
            }
            else
            {
                return this.UpdateArticle(model);
            }
        }

        private ResponseModel AddArticle(ArticleDTO model)
        {
            T_Article article = model.Article;
            article.Id = Guid.NewGuid().ToString();
            article.IsSend = false;
            article.IsDelete = false;
            article.CreateTime = DateTime.Now;
            article.UpdateTime = DateTime.Now;
            List<T_Article_Detail> details = model.Detail;
            foreach (var item in details)
            {
                item.Id = Guid.NewGuid().ToString();
                item.ArticleId = article.Id;
                item.IsDelete = false;
                item.CreateTime = DateTime.Now;
                item.UpdateTime = DateTime.Now;
            }

            using (ArticleResp resp = new ArticleResp())
            {
                try
                {
                    resp.BeginTransaction();
                    resp.Insert(article);
                    //最简单做法  效率损耗
                    foreach (var item in details)
                        resp.Insert(item);
                    resp.Commit();
                    return new ResponseModel();
                }
                catch (Exception ex)
                {
                    resp.Rollback();
                    LogService.Error(ex);
                    return new ResponseModel(10010, ex.Message);
                }
            }
        }

        private ResponseModel UpdateArticle(ArticleDTO model)
        {
            using (ArticleResp resp = new ArticleResp())
            {
                try
                {
                    resp.BeginTransaction();
                    T_Article oldArticle = resp.GetArticleById(model.Article.Id);
                    IEnumerable<T_Article_Detail> oldDetails = resp.GetArticleDetailByArticle(model.Article.Id);

                    oldArticle.Title = model.Article.Title;
                    oldArticle.Author = model.Article.Author;
                    oldArticle.Contents = model.Article.Contents;
                    oldArticle.GroupMsg = model.Article.GroupMsg;
                    oldArticle.HeaderImg = model.Article.HeaderImg;
                    oldArticle.UpdateTime = DateTime.Now;

                    resp.Update(oldArticle);

                    foreach (var item in model.Detail)
                    {
                        if (string.IsNullOrEmpty(item.Id))
                        {
                            item.Id = Guid.NewGuid().ToString();
                            item.ArticleId = oldArticle.Id;
                            item.IsDelete = false;
                            item.CreateTime = DateTime.Now;
                            item.UpdateTime = DateTime.Now;
                            resp.Insert(item);
                        }
                        else
                        {
                            T_Article_Detail oldDetail = oldDetails.Where(t => t.Id == item.Id).First();
                            oldDetail.Place = item.Place;
                            oldDetail.Longitude = item.Longitude;
                            oldDetail.Region = item.Region;
                            oldDetail.Number = item.Number;
                            oldDetail.IsLBS = item.IsLBS;
                            oldDetail.BeginTime = item.BeginTime;
                            oldDetail.EndTime = item.EndTime;
                            oldDetail.UpdateTime = item.UpdateTime;
                            resp.Update(oldDetail);
                        }
                    }
                    resp.Commit();
                    return new ResponseModel();
                }
                catch (Exception ex)
                {
                    resp.Rollback();
                    LogService.Error(ex);
                    return new ResponseModel(10011, ex.Message);
                }

            }
        }
        #endregion

        public ResponseModel ReleaseArticle(string articleId)
        {
            using (ArticleResp resp = new ArticleResp())
            {
                try
                {
                    T_Article article = resp.GetArticleById(articleId);
                    if (article.IsSend)
                        return new ResponseModel(10022, "该活动已经发布过了");
                    article.IsSend = true;
                    article.UpdateTime = DateTime.Now;
                    resp.Update(article);

                    if (article.GroupMsg)
                    {
                        //微信推送
                    }
                    return new ResponseModel();
                }
                catch (Exception ex)
                {
                    LogService.Error(ex);
                    return new ResponseModel(10023, ex.Message);
                }

            }
        }

        public ResponseModel DeleteArticle(string articleId)
        {
            using (ArticleResp resp = new ArticleResp())
            {
                try
                {
                    resp.BeginTransaction();
                    resp.DeleteArticle(articleId);
                    resp.DeleteDetailByArticle(articleId);
                    resp.Commit();
                    return new ResponseModel();
                }
                catch (Exception ex)
                {
                    resp.Rollback();
                    LogService.Error(ex);
                    return new ResponseModel(10033, "活动删除失败");
                }
            }
        }


        public ResponseModel SignArticle(string articleId, string detailId)
        {
            using (ArticleResp resp = new ArticleResp())
            {
                try
                {
                    T_Article_Sign sign = new T_Article_Sign()
                    {
                        Id = Guid.NewGuid().ToString(),
                        ArticleId = articleId,
                        DetailId = detailId,
                        UserId = CacheHelper.GetSession<CorpUserResult>(CacheKey.MobileUserSession).open_userid,
                        IsDelete = false,
                        CreatTime = DateTime.Now,
                        UpdateTime = DateTime.Now
                    };

                    resp.Insert(sign);
                    return new ResponseModel();
                }
                catch (Exception ex)
                {
                    LogService.Error(ex);
                    return new ResponseModel(10041, ex.Message);
                }
            }
        }

        public ResponseModel CancelSignArticle(string articleId, string detailId)
        {
            using (ArticleResp resp = new ArticleResp())
            {
                try
                {
                    CorpUserResult userCache = CacheHelper.GetSession<CorpUserResult>(CacheKey.MobileUserSession);
                    resp.CancelSign(userCache.open_userid, articleId, detailId);
                    return new ResponseModel();
                }
                catch (Exception ex)
                {
                    LogService.Error(ex);
                    return new ResponseModel(10051, ex.Message);
                }
            }
        }

        public ResponseModel SignedArticle(string articleId, string detailId)
        {
            using (ArticleResp resp = new ArticleResp())
            {
                try
                {
                    CorpUserResult user = CacheHelper.GetSession<CorpUserResult>(CacheKey.MobileUserSession);
                    int count = resp.GetUserIsSigned(user.open_userid, articleId, detailId);
                    if (count > 0)
                        return new ResponseModel(100062, "已报名活动");
                    T_Article_Signed signed = new T_Article_Signed()
                    {
                        Id = Guid.NewGuid().ToString(),
                        ArticleId = articleId,
                        DetailId = detailId,
                        UserId = user.open_userid,
                        IsAdminSigned = false,
                        IsDelete = false,
                        CreateTime = DateTime.Now,
                        UpdateTime = DateTime.Now
                    };
                    resp.Insert(signed);
                    return new ResponseModel();
                }
                catch (Exception ex)
                {
                    LogService.Error(ex);
                    return new ResponseModel(100061, ex.Message);
                }
            }
        }

        public ResponseModel<List<T_Article_Signed>> BatchSignedArticle(List<string> userList, string articleId)
        {
            using (ArticleResp resp = new ArticleResp())
            {
                ///先获取这些人中已报名并没有签到的详情id
                //报名了 没有签到的人
                try
                {
                    resp.BeginTransaction();
                    IEnumerable<T_Article_Sign> notSigned = resp.GetNotSignDetail(userList, articleId);
                    if (notSigned.Count() <= 0)
                        return new ResponseModel<List<T_Article_Signed>>();
                    List<T_Article_Signed> adminSigned = new List<T_Article_Signed>();
                    foreach (var item in notSigned)
                    {
                        T_Article_Signed signed = new T_Article_Signed()
                        {
                            Id = Guid.NewGuid().ToString(),
                            ArticleId = item.ArticleId,
                            IsAdminSigned = true,
                            IsDelete = false,
                            UserId = item.UserId,
                            DetailId = item.DetailId,
                            CreateTime = DateTime.Now,
                            UpdateTime = DateTime.Now
                        };
                        resp.Insert(signed);
                        adminSigned.Add(signed);
                    }
                    resp.Commit();
                    return new ResponseModel<List<T_Article_Signed>>(adminSigned);
                }
                catch (Exception ex)
                {
                    resp.Rollback();
                    LogService.Error(ex);
                    return new ResponseModel<List<T_Article_Signed>>(10071, ex.Message);
                }
            }
        }

        public ResponseModel GetUserArticleStatus(string articleId)
        {
            //获取到活动的所有明细

            //查询授权用户是否报名
            //查询授权用户是否签到
            CorpUserResult user = CacheHelper.GetSession<CorpUserResult>(CacheKey.MobileUserSession);
            using (ArticleResp resp = new ArticleResp())
            {
                //IEnumerable<T_Article_Detail> details = resp.GetArticleDetailByArticle(articleId);
                IEnumerable<UserSignQuery> signList = resp.GetUserIsSign(user.open_userid, articleId);
                IEnumerable<UserSignQuery> signedList = resp.GetUserIsSigned(user.open_userid, articleId);

                List<UserSignDTO> list = new List<UserSignDTO>();
                foreach (var item in signList)
                {
                    UserSignDTO dto = new UserSignDTO()
                    {
                        ArticleId = articleId,
                        DetailId = item.DetailId,
                        Sign = item.Count,
                        Signed = signedList.Where(t => t.DetailId == item.DetailId).FirstOrDefault().Count
                    };
                    list.Add(dto);
                }

                return new ResponseModel(list);

            }




        }

        public ResponseModel AddComment(ArticleCommentDTO model, List<string> mediaUrl)
        {
            using (ArticleResp resp = new ArticleResp())
            {
                try
                {
                    T_Article_Comment comment = new T_Article_Comment()
                    {
                        Id = Guid.NewGuid().ToString(),
                        UserId = CacheHelper.GetSession<CorpUserResult>(CacheKey.MobileUserSession).open_userid,
                        ArticleId = model.ArticleId,
                        Contents = model.Content,
                        Media = string.Join(",", mediaUrl.ToArray()),
                        CreateTime = DateTime.Now,
                        UpdateTime = DateTime.Now,
                    };
                    resp.Insert(comment);
                    return new ResponseModel();
                }
                catch (Exception ex)
                {
                    LogService.Error(ex);
                    return new ResponseModel(10081, ex.Message);
                }
            }
        }


        public ResponseModel GetCommentList(int pageIndex, int pageSize, string articleId)
        {
            using (ArticleResp resp = new ArticleResp())
            {
                int count = resp.GetCommentCount(articleId);
                IEnumerable<T_Article_Comment> comments = resp.GetCommentList(pageIndex, pageSize, articleId);


                return new ResponseModel(new { count = count, list = comments });
            }
        }

        public ResponseModel GetUserSignArticle(int pageIndex, int pageSize)
        {
            using (ArticleResp resp = new ArticleResp())
            {
                CorpUserResult user = CacheHelper.GetSession<CorpUserResult>(CacheKey.MobileUserSession);
                IEnumerable<UserSignQuery> result = resp.GetUserSignArticleList(pageIndex, pageSize, user.open_userid);
                int count = resp.GetUserSignCount(user.open_userid);
                return new ResponseModel(new { count = count, list = result });
            }
        }

        public ResponseModel GetUserSignedArticle(int pageIndex, int pageSize)
        {
            using (ArticleResp resp = new ArticleResp())
            {
                CorpUserResult user = CacheHelper.GetSession<CorpUserResult>(CacheKey.MobileUserSession);
                IEnumerable<UserSignQuery> result = resp.GetUserSignedArticleList(pageIndex, pageSize, user.open_userid);
                int count = resp.GetUserSignedCount(user.open_userid);
                return new ResponseModel(new { count = count, list = result });
            }

        }
    }
}
