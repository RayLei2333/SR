using SR.App_Start;
using SR.Infrastructure.IOC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace SR
{
    /// <summary>
    /// 程序入口
    /// </summary>
    public class MvcApplication : System.Web.HttpApplication
    {
        /// <summary>
        /// 当程序被启动时云心这个方法，
        /// 根据IIS站点启动运行，全局只运行一次
        /// </summary>
        protected void Application_Start()
        {
            //注册area
            AreaRegistration.RegisterAllAreas();
            //注册路由
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            //注册拦截器
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            //注册ioc
            Bootstrapper.Register();
        }
    }
}
