using Application.UserApp;
using CMS.Admin.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Utility.Convert;

namespace CMS.Admin.Controllers
{
    public class LoginController : Controller
    {
        private IUserAppService _userAppService;

        public LoginController(IUserAppService userAppService)
        {
            _userAppService = userAppService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(LoginModel model) {
            if (ModelState.IsValid) {
                // 检查用户信息
                var user = _userAppService.CheckUser(model.UserName, model.Password);
                if (user != null) {
                    // 记录Session
                    HttpContext.Session.Set("CurrentUser", ByteConvertHelper.Object2Bytes(user));
                    // 跳转到系统首页
                    return RedirectToAction("Index", "Home");
                }
                ViewBag.ErrorInfo = "用户名或密码错误.";
                return View();
            }

            ViewBag.ErrorInfo = ModelState.Values.First().Errors[0].ErrorMessage;

            return View(model);
        }
    }
}               