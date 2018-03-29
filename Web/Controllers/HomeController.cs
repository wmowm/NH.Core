using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Web.Models;
using BLL;
namespace Web.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            B_Users b_user = new B_Users();
            var res =  b_user.Get(1);
            ViewBag.res = res.user_name;
            return View();
        }

       

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
