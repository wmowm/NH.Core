using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Tibos.Common;
using Tibos.Service.Contract;

namespace Tibos.Api.Areas.User.Controllers
{
    [Produces("application/json")]
    [Route("api/User")]
    public class UserController : Controller
    {
        public UsersIService _UsersService { get; set; }

        public JsonResult GetUserList()
        {
            Common.Json json = new Common.Json();
            DateTime dt = Convert.ToDateTime("2018-4-13");
            return Json(dt);
        }

        [HttpGet]
        [HttpPost]
        public JsonResult GetToken(string user_name,string password)
        {
            //自定义返回json对象
            Json json = new Json();

            //1.开始参数验证
            if (string.IsNullOrEmpty(user_name))
            {
                json.status = -1;
                json.msg = "用户名不能为空!";
                return Json(json);
            }
            if (string.IsNullOrEmpty(password))
            {
                json.status = -1;
                json.msg = "密码不能为空!";
                return Json(json);
            }
            //自定义参数模板
            List<SearchTemplate> st = new List<SearchTemplate>();
            st.Add(new SearchTemplate() { key = "user_name", value = user_name, searchType = EnumBase.SearchType.Eq });
            st.Add(new SearchTemplate() { key = "password", value = password, searchType = EnumBase.SearchType.Eq });
            var list = _UsersService.GetList(st, null);
            if (list.Count > 0)
            {
                //登录成功,设置用户token

                var model = list[0];

                var claimsIdentity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);

                claimsIdentity.AddClaim(new Claim(ClaimTypes.Sid, "123"));
                if (model.group_id  == 1)
                {
                    claimsIdentity.AddClaim(new Claim(model.group_id.ToString(), "Admin"));
                }
                else
                {
                    claimsIdentity.AddClaim(new Claim(model.group_id.ToString(), "User"));
                }
                var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

                HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal, new AuthenticationProperties
                {
                    ExpiresUtc = DateTime.UtcNow.AddMinutes(20)
                });
                //不同的角色,进入不同的页面
            }
            else
            {
                json.status = -1;
                json.msg = "用户名或密码不正确!";
            }
            return Json(json);
        }

        [Authorize(Roles = "1")]
        [Area("Admin")]
        public JsonResult GetAdmin()
        {
            var userCode = HttpContext.User.FindFirst(ClaimTypes.Sid).Value;
            return Json(userCode);
        }

        [Authorize(Roles = "2")]
        [Area("User")]
        public JsonResult GetUser()
        {
            var userCode = HttpContext.User.FindFirst(ClaimTypes.Sid).Value;
            return Json(userCode);
        }
        [HttpGet("{a}/{b}")]
        public JsonResult Add(string a,string b)
        {
            //var userCode = HttpContext.User.FindFirst(ClaimTypes.Sid).Value;
            var res = a + b;
            return Json(res);
        }
    }
}