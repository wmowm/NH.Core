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
using Microsoft.AspNetCore.Hosting;
using System.IO;
using System.Collections;

namespace Web.Controllers
{
    public class HomeController : Controller
    {
        private IHostingEnvironment hostingEnv;
        public HomeController(IHostingEnvironment env)
        {
            hostingEnv = env;
        }
        public IActionResult Index()
        {
            ViewData["GUID"] = Guid.NewGuid();
            return View();
        }
        public IActionResult Login()
        {
            return View();
        }

        public IActionResult About()
        {
            return View();
        }

        public IActionResult Home()
        {
            return View();
        }
        public IActionResult Api()
        {
            return View();
        }
        public IActionResult Test()
        {
            return View();
        }

        public IActionResult UpImg()
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





        public async Task<IActionResult> UeditorUpload()
        {
            var files = Request.Form.Files;
            string callback = Request.Query["callback"];
            string editorId = Request.Query["editorid"];
            if (files != null && files.Count > 0)
            {
                var file = files[0];
                string contentPath = hostingEnv.WebRootPath;
                string fileDir = Path.Combine(contentPath, "upload");
                if (!Directory.Exists(fileDir))
                {
                    Directory.CreateDirectory(fileDir);
                }
                string fileExt = Path.GetExtension(file.FileName);
                string newFileName = DateTime.Now.ToString("yyyyMMddHHmmssfff") + fileExt;
                string filePath = Path.Combine(fileDir, newFileName);
                using (FileStream fs = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(fs);
                }
                var fileInfo = getUploadInfo("../../../upload/" + newFileName, file.FileName,
                    Path.GetFileName(filePath), file.Length, fileExt);
                string json = BuildJson(fileInfo);

                Response.ContentType = "text/html";
                if (callback != null)
                {
                    await Response.WriteAsync(String.Format("<script>{0}(JSON.parse(\"{1}\"));</script>", callback, json));
                }
                else
                {
                    await Response.WriteAsync(json);
                }
                return View();
            }
            return NoContent();
        }
        private string BuildJson(Hashtable info)
        {
            List<string> fields = new List<string>();
            string[] keys = new string[] { "originalName", "name", "url", "size", "state", "type" };
            for (int i = 0; i < keys.Length; i++)
            {
                fields.Add(String.Format("\"{0}\": \"{1}\"", keys[i], info[keys[i]]));
            }
            return "{" + String.Join(",", fields) + "}";
        }
        /**
       * 获取上传信息
       * @return Hashtable
       */
        private Hashtable getUploadInfo(string URL, string originalName, string name, long size, string type, string state = "SUCCESS")
        {
            Hashtable infoList = new Hashtable();

            infoList.Add("state", state);
            infoList.Add("url", URL);
            infoList.Add("originalName", originalName);
            infoList.Add("name", Path.GetFileName(URL));
            infoList.Add("size", size);
            infoList.Add("type", Path.GetExtension(originalName));

            return infoList;
        }

    }
}
