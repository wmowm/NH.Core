using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Landlord.Models;
using Microsoft.AspNetCore.Mvc;

namespace Landlord.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            Rule rule = new Rule();
            var list = rule.CreateCard();

            list = rule.Shuffle(list);

            var mm = rule.Licensing(list);
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View("日了狗子,出错了");
        }
    }
}
