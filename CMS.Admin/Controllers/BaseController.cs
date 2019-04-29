using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

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
            byte[] result;
            // 从session中获取当前登录用户信息
            context.HttpContext.Session.TryGetValue("CurrentUser", out result);
            if (result == null) {
                context.Result = new RedirectResult("/Login/Index");
                return;
            }

            base.OnActionExecuting(context);
        }
    }
}