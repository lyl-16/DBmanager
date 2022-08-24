using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Containers;
using ServerSqlTools;

namespace _12306.Controllers
{
    public class RegisterController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult agreement()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Index(string user_ID, string user_password, string user_phone_number, string user_email, string user_real_name,string user_gender,string user_address,string user_person_ID)
        {
            User U = new User();
            U.UserID = user_ID;
            U.UserPWD = user_password;
            U.UserPhone = user_phone_number;
            U.UserEmail = user_email;
            U.UserRName = user_real_name;
            U.UserGender = user_gender;
            U.UserPID = user_person_ID;
            U.UserAddr = user_address;
            Console.WriteLine(U);
            int t;
            //ServerSqlTools.OracleSqlTools.resetUser();
            t = ServerSqlTools.OracleSqlTools.Register(U);
            if(t==-1)
            {
                ViewBag.notice = "register success";
                Console.WriteLine("register success");
                return RedirectToAction("Index","Login");
            }
            else
            {
                Console.WriteLine("register failed");
                Console.WriteLine(t);
                return Json(U);
            }
        }
    }
}
