using SR.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SR.App_Start
{
    /// <summary>
    /// 手机端授权拦截器
    /// </summary>
    public class MobileAuthAttribute : AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            if(CacheHelper.GetSession(CacheKey.MobileUserSession) == null)
                return false;
            return true;
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            string url1 = filterContext.RequestContext.RouteData.ToString();
            Uri url = filterContext.HttpContext.Request.Url;
            //构造完整的企业微信授权路径
            string authUrl = $"{url.Scheme}://{url.Authority}/WeChat/WebAuth?redirect={HttpUtility.UrlEncode(url.ToString())}";
            authUrl = HttpUtility.UrlEncode(authUrl);
            string corpUrl = $"https://open.weixin.qq.com/connect/oauth2/authorize?appid={AppConfig.CorpId}&redirect_uri={authUrl}&response_type=code&scope=snsapi_base&state=STATE#wechat_redirect";
            //跳转至授权页面
            filterContext.HttpContext.Response.Redirect(corpUrl);
            //if()
            base.HandleUnauthorizedRequest(filterContext);
        }
    }
}