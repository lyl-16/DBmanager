using Microsoft.AspNetCore.Mvc;

namespace _12306TrainSystemProject.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult TryLogin(Models.UserViewModel model)
        {
            int L = ServerSqlTools.OracleSqlTools.Login(model);
            if(L != -1)
            {
                return Content("黄昊的检验说不对, L = " + L.ToString());
            }
            //重定向到火车页面
            Response.Redirect("../Train");
            return Content("");
        }
    }
}
