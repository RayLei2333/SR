using SR.DTO;
using SR.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace SR.App_Start
{

    /// <summary>
    /// 异常拦截器，注意继承类
    /// </summary>
    public class ErrorFilter : HandleErrorAttribute
    {
        /// <summary>
        /// 当发生异常后如何处理
        /// </summary>
        /// <param name="filterContext"></param>
        public override void OnException(ExceptionContext filterContext)
        {
            //base.OnException(filterContext);
            //如果这个异常未被处理
            if (!filterContext.ExceptionHandled)
            {
                //获取路由
                string controller = filterContext.RouteData.Values["controller"].ToString();
                string action = filterContext.RouteData.Values["action"].ToString();

                //日志记录
                LogService.Error($"{controller}/{action} Message:{filterContext.Exception.Message}  Source:{filterContext.Exception.Source}");
                //标识该请求为已处理状态
                filterContext.ExceptionHandled = true;
                //响应数据
                ResponseModel resp = new ResponseModel()
                {
                    ErrCode = -1,
                    ErrMsg = filterContext.Exception.Message
                };
                //响应模型，注意继承
                ContentResult result = new ContentResult()
                {
                    Content = JsonHelper.ToJson(resp),
                    ContentEncoding = Encoding.UTF8,
                    ContentType = "application/json"
                };
                //设置响应到浏览器
                filterContext.Result = result;
            }
            

        }
    }
}