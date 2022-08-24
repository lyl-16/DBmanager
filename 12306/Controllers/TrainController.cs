using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _12306.Controllers
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
