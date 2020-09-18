using SR.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SR.App_Start
{
    /// <summary>
    /// PC端登录拦截, 注意继承类
    /// 手机端同理
    /// </summary>
    public class AdminAuthAttribute : AuthorizeAttribute
    {
        /// <summary>
        /// 检查授权情况
        /// 判断有没有登录
        /// 如果登陆返回true，未登录返回false
        /// </summary>
        /// <param name="httpContext"></param>
        /// <returns></returns>
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            //同理session[""]
            if (CacheHelper.GetSession("pcUser") == null)
                return false;
            return true;
        }

        /// <summary>
        /// 如果未登录，则让用户登陆
        /// </summary>
        /// <param name="filterContext"></param>
        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            filterContext.HttpContext.Response.Redirect("/Login/Login");
            base.HandleUnauthorizedRequest(filterContext);
        }
    }
}