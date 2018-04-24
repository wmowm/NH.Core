using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Web.Models;
using Tibos.Service;
using Tibos.Common;
using Microsoft.AspNetCore.Http;
using System.Security.Cryptography;
using System.Text;

namespace Web.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            UsersService b_user = new UsersService();
            var res =  b_user.Get(1);
            ViewBag.res = res.user_name;
            return View();
        }
        public IActionResult Login()
        {
            return View();
        }

        [HttpGet]
        public ActionResult GetAuthCode()
        {
            string pic_code = "";
            byte[] b = new VerifyCode().GetVerifyCode(ref pic_code);
            HttpContext.Session.SetString("pic_code", pic_code);//存入session
            return File(b, @"image/Gif");
        }

        [HttpPost]
        public JsonResult Login(string user_name, string password, string code, string returnUrl)
        {
            Json json = new Tibos.Common.Json();
            string pic_code = HttpContext.Session.GetString("pic_code");
            using (var md5 = MD5.Create())
            {
                var res = md5.ComputeHash(Encoding.UTF8.GetBytes(code.ToLower()));
                code = BitConverter.ToString(res);
            }
            if (pic_code != code)
            {
                json.status = -1;
                json.msg = "验证码不正确";
            }

            if (!String.IsNullOrEmpty(returnUrl))
            {
                json.returnUrl = returnUrl;
            }
            else
            {
                json.returnUrl = "/home/index";
            }
            return Json(json);
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
