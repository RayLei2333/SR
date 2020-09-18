using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SR.Controllers
{
    /**
     * 业务
     * 
     * 分页查询活动并附带 关键词、开始时间、结束时间、地域
     * 新增&修改活动
     * 发布活动
     * 删除活动
     * 查询活动详情
     * 查询当前授权用户对某一个活动是否报名
     * 查询当前授权用户对某一个活动是否签到
     * 报名活动
     * 取消报名活动
     * 签到活动、批量签到活动(补签到)
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
        // GET: Article
        public ActionResult Index()
        {
            return Content("you are right");
        }
    }
}