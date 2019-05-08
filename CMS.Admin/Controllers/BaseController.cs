using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;

namespace CMS.Admin.Controllers
{
    public class BaseController : Controller
    {
        /// <summary>
        /// Called before the action method is invoked.
        /// </summary>
        /// <param name="context"></param>
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            // 从session中获取当前登录用户信息
            var userStr = context.HttpContext.Session.GetString("CurrentUser");
            if (string.IsNullOrEmpty(userStr))
            {
                context.Result = new RedirectResult("/Login/Index");
                return;
            }

            base.OnActionExecuting(context);
        }


        /// <summary>
        /// 获取服务端验证的第一条错误信息
        /// </summary>
        /// <returns></returns>
        public string GetModelStateError()
        {
            foreach (var item in ModelState.Values)
            {
                if (item.Errors.Count > 0)
                {
                    return item.Errors[0].ErrorMessage;
                }
            }
            return "";
        }
    }
}