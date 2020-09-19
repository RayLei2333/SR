using SR.Business;
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
     * 后台登陆
     * **/
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Login()
        {
            return View();
        }


        [HttpPost]
        public ActionResult CheckLogin(string userName, string pwd)
        {
            IWeChatUserService service = ObjectFactory.GetObject<IWeChatUserService>();
            var result = service.Login(userName, pwd);
            return Content(JsonHelper.ToJson(result), "application/json", Encoding.UTF8);
        }
    }
}