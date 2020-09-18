using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace SR
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");  //忽略这种请求

            //设置自定义路由
            //路由约精确，代码顺序越要排在前面
            routes.MapRoute(
                name: "ArticleAPI",
                url: "api/{controller=ArticleAPI}/{action}/{id}",
                defaults: new { controller = "Article", action = "Index", id = UrlParameter.Optional }
            );

            //MVC自定义路由
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
