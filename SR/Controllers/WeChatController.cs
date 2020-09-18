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
    /**
     * 业务
     * 1、处理微信网页授权
     * 2、处理微信JSSDK分享
     * **/
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
            if (userInfo.status == 2 || userInfo.status == 5)
                return View();  //已禁用状态 和  退出企业状态无法访问
            IWeChatUserService userService = ObjectFactory.GetObject<IWeChatUserService>();
            bool checkUserResult = userService.NotfoundOrAddUser(userInfo);
            if (!checkUserResult)
                return View();  //增添用户信息出错
            CacheHelper.SetSession("mobileUser", userInfo);
            return Redirect(redirectUrl);
        }


    }
}