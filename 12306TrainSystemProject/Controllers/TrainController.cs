using Microsoft.AspNetCore.Mvc;

namespace _12306TrainSystemProject.Controllers
{
    public class TrainController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Buy()
        {
            return View();
        }

        public IActionResult Pay()
        {
            return View();
        }
    }
}
