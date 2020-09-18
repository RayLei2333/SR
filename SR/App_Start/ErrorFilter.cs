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
    /// 异常拦截器
    /// </summary>
    public class ErrorFilter : HandleErrorAttribute
    {
        public override void OnException(ExceptionContext filterContext)
        {
            //base.OnException(filterContext);
            if (!filterContext.ExceptionHandled)
            {
                string controller = filterContext.RouteData.Values["controller"].ToString();
                string action = filterContext.RouteData.Values["action"].ToString();

                LogService.Error($"{controller}/{action} Message:{filterContext.Exception.Message}  Source:{filterContext.Exception.Source}");
                filterContext.ExceptionHandled = true;
                ResponseModel resp = new ResponseModel()
                {
                    ErrCode = -1,
                    ErrMsg = filterContext.Exception.Message
                };
                ContentResult result = new ContentResult()
                {
                    Content = JsonHelper.ToJson(resp),
                    ContentEncoding = Encoding.UTF8,
                    ContentType = "application/json"
                };
                filterContext.Result = result;
            }
            

        }
    }
}