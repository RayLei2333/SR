using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SR.App_Start
{
    /// <summary>
    /// 全局拦截器
    /// </summary>
    public class FilterConfig
    {
        /// <summary>
        /// 注拦截器
        /// </summary>
        /// <param name="filters">GlobalFilters.Filters 由Gloabl提供</param>
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            //添加全局拦截器
            //原理AOP
            filters.Add(new ErrorFilter());
        }

    }
}