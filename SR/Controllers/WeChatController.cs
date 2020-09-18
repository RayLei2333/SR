using SR.Business;
using SR.Infrastructure;
using SR.Infrastructure.IOC;
using SR.WeChat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SR.Controllers
{
    public class WeChatController : Controller
    {

        /// <summary>
        /// 微信企业号网页授权
        /// </summary>
        /// <returns></returns>
        public ActionResult WebAuth()
        {
            string code = Request["code"];
            string redirectUrl = Request["redirect"];
            IWeChatService service = ObjectFactory.GetObject<IWeChatService>();
            CodeResult codeResult = service.CodeToUserId(code);
            if (codeResult.errcode != 0)
                return View();  //获取code失败
            if (string.IsNullOrEmpty(codeResult.UserId))
                return View();  //非企业号员工
            CorpUserResult userInfo = service.GetUserInfo(codeResult.UserId);
            if (userInfo.errcode != 0)
                return View();  //获取用户信息失败
            CacheHelper.SetSession("mobileUser", userInfo);
            return Redirect(redirectUrl);
        }


    }
}