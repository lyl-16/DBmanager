using Microsoft.AspNetCore.Mvc;

namespace _12306TrainSystemProject.Controllers
{
    public class RegisterController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Agreement()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(Models.UserViewModel model)
        {
            model.UserID = model.UserPID;
            model.UserType = 0;
            model.UserGender = "1";

            int r = ServerSqlTools.OracleSqlTools.Register(model);
            if (r != -1)
            {
                //此处不知道应该用谁写的检验，建议统一一下
                return Content("黄昊的检验说不对, r = " + r.ToString());
            }
            //重定向到登录页面
            Response.Redirect("../Login");
            return Content("");
        }
    }
}
