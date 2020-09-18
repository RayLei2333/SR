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
    public class MvcApplication : System.Web.HttpApplication
    {
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
