using SR.Business;
using SR.DTO;
using SR.DTO.Req;
using SR.DTO.Resp;
using SR.Infrastructure;
using SR.Infrastructure.IOC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace SR.Controllers
{
    /**
     * 业务
     * 
     * 分页查询活动并附带 关键词、开始时间、结束时间、地域
     * 查询单个活动详情
     * 新增&修改活动
     * 发布活动
     * 删除活动
     * 报名活动
     * 取消报名活动
     * 签到活动、批量签到活动(补签到)
     * 查询当前授权用户对某一个活动是否报名
     * 查询当前授权用户对某一个活动是否签到
     * 分页查询活动评论
     * 新增活动评论     
     * 分页查询用户已报名所有活动
     * 分页查询用户已签到所有活动
     * 查询用户已签到活动总数判断等级
     * 
     **/
    //[Route("api/article/{action}")]
    public class ArticleAPIController : Controller
    {

        //分页查询活动并附带 关键词、开始时间、结束时间、地域
        public ActionResult GetArticleList(ArticleQuery model)
        {
            IArticleService service = ObjectFactory.GetObject<IArticleService>();
            var result = service.GetArticleList(model);
            return Json(result);
        }

        //查询单个活动详情
        public ActionResult GetArticle(string articleId)
        {
            IArticleService service = ObjectFactory.GetObject<IArticleService>();
            var result = service.GetArticle(articleId);
            return Json(result);
        }


        //新增&修改活动
        public ActionResult AddOrEditArticle(ArticleDTO model)
        {
            IArticleService service = ObjectFactory.GetObject<IArticleService>();
            var result = service.AddOrUpdate(model);
            return Json(result);
        }

        //发布活动  微信推送
        public ActionResult ReleaseArticle(string articleId)
        {
            IArticleService service = ObjectFactory.GetObject<IArticleService>();
            var result = service.ReleaseArticle(articleId);
            return Json(result);
        }

        //删除活动
        public ActionResult DeleteArticle(string articleId)
        {
            IArticleService service = ObjectFactory.GetObject<IArticleService>();
            var result = service.DeleteArticle(articleId);
            return Json(result);
        }

        //报名、取消报名活动 微信推送
        public ActionResult SignArticle(string articleId, string detailId, bool sign)
        {
            IArticleService service = ObjectFactory.GetObject<IArticleService>();
            object result = null;
            if (sign)
                result = service.SignArticle(articleId, detailId);
            else
                result = service.CancelSignArticle(articleId, detailId);
            return Json(result);
        }

        //签到活动 微信推送
        public ActionResult SignedArticel(string articleId, string detailId)
        {
            IArticleService service = ObjectFactory.GetObject<IArticleService>();
            var result = service.SignedArticle(articleId, detailId);
            return Json(result);
        }

        //批量签到活动 微信推送
        public ActionResult BatchSignedArticel(List<string> userList, string articleId)
        {
            IArticleService service = ObjectFactory.GetObject<IArticleService>();
            var result = service.BatchSignedArticle(userList, articleId);
            return Json(result);
        }

        //查询当前授权用户对某一个活动是否报名
        //查询当前授权用户对某一个活动是否签到
        public ActionResult GetUserArticleStatus(string articleId)
        {
            IArticleService service = ObjectFactory.GetObject<IArticleService>();
            var result = service.GetUserArticleStatus(articleId);
            return Json(result);
        }

        //新增活动评论
        public ActionResult Comment(ArticleCommentDTO model)
        {
            //保存Media 中的图片转为链接
            IArticleService service = ObjectFactory.GetObject<IArticleService>();
            //var result = service.AddComment(model,);
            //return Json(result);
            return null;
        }

        //分页查询活动评论
        public ActionResult GetCommentList(int pageIndex, int pageSize, string articleId)
        {
            IArticleService service = ObjectFactory.GetObject<IArticleService>();
            var result = service.GetCommentList(pageIndex, pageSize, articleId);
            return Json(result);
        }

        //查询用户已签到活动总数判断等级
        public ActionResult GetUserLv()
        {
            IWeChatUserService service = ObjectFactory.GetObject<IWeChatUserService>();
            var result = service.GetUserLv();
            return Json(result);
        }

        //分页查询用户已报名所有活动
        public ActionResult GetUserSignArticleList(int pageIndex,int pageSize)
        {
            IArticleService service = ObjectFactory.GetObject<IArticleService>();
            var result = service.GetUserSignArticle(pageIndex, pageSize);
            return Json(result);
        }

        //分页查询用户已签到所有活动
        public ActionResult GetUserSignedArticleList(int pageIndex, int pageSize)
        {
            IArticleService service = ObjectFactory.GetObject<IArticleService>();
            var result = service.GetUserSignedArticle(pageIndex, pageSize);
            return Json(result);
        }

        // GET: Article
        public ActionResult Index()
        {

            return Content("you are right");
        }


        protected ActionResult Json(object data, int code = 0, string msg = "", string timeFormat = "yyyy-MM-dd HH:mm:ss")
        {
            ResponseModel model = new ResponseModel
            {
                Data = data,
                ErrCode = code,
                ErrMsg = msg
            };

            return Content(JsonHelper.ToJson(model), "application/json", Encoding.UTF8);
        }

        protected ActionResult Json(ResponseModel model)
        {
            return Content(JsonHelper.ToJson(model), "application/json", Encoding.UTF8);
        }
    }
}