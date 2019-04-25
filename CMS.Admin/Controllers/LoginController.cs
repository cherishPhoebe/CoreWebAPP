using Application.UserApp;
using Microsoft.AspNetCore.Mvc;

namespace CMS.Admin.Controllers
{
    public class LoginController : Controller
    {
        private IUserAppService _userAppService;

        public LoginController(IUserAppService userAppService)
        {
            _userAppService = userAppService;
        }

        public IActionResult Index()
        {
            var user = _userAppService.CheckUser("admin", "123456");


            return View();
        }
    }
}