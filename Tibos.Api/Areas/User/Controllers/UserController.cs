using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Tibos.Common;
using Tibos.ConfingModel;
using Tibos.ConfingModel.model;
using Tibos.Domain;
using Tibos.Service.Contract;

namespace Tibos.Api.Areas.User.Controllers
{
    [Produces("application/json")]
    [Route("api/User/")]
    [EnableCors("any")]
    public class UserController : Controller
    {


        private IMemoryCache _Cache;

        //属性注入
        public UsersIService _UsersService { get; set; }

        //构造函数注入
        public UserController(IMemoryCache memoryCache)
        {
            _Cache = memoryCache;
        }


        [HttpGet]
        public async Task<JsonResult> Get(int id)
        {
            return await Task.Run<JsonResult>(() =>
            {
                Common.Json json = new Common.Json();
                var model = _UsersService.Get(id);
                return Json(model);
            });
        }

        [HttpGet("{token}")]
        public async Task<JsonResult> Get(string user_name, string password)
        {
            return await Task.Run<JsonResult>(() =>
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
                    //获取token
                    var tokenconfig = JsonConfigurationHelper.GetAppSettings<TokenConfig>("TokenConfig.json", "TokenConfig");
                    Token tokenHelper = new Token();
                    var token = tokenHelper.CreateToken(user_name, password, tokenconfig.Sign);

                    //登录成功,设置用户token
                    _Cache.GetOrCreate(token, entry =>
                    {
                        entry.SetSlidingExpiration(TimeSpan.FromSeconds(15 * 60)); //15分钟
                        return (list[0].id);
                    });
                    //var result = _Cache.Get(token);
                    json.msg = "登录成功!";
                    json.data = token;
                }
                else
                {
                    json.status = -1;
                    json.msg = "用户名或密码不正确!";
                }
                return Json(json);
            });
        }

        [Route("list"),HttpPost]
        public async Task<JsonResult> List([FromBody]DataParameter parameter)
        {
            return await Task.Run<JsonResult>(() =>
            {

                return Json(parameter);
            });
        }

        [HttpPost]
        public async Task<JsonResult> Add(Users user)
        {
            return await Task.Run<JsonResult>(() =>
            {

                return Json("add");
            });
        }

        [HttpPut("{edit}")]
        public async Task<JsonResult> Edit(Users user)
        {
            return await Task.Run<JsonResult>(() =>
            {

                return Json(user);
            });
        }

        [HttpDelete("{delete}")]
        public async Task<JsonResult> Delete(int id)
        {
            return await Task.Run<JsonResult>(() =>
            {

                return Json("成功删除了一条数据");
            });
        }
    }

    [Serializable]
    public class DataParameter
    {
        public DataParameter()
        {
            this.DataItems = new List<DataItemParameter>();
        }

        public int Id { get; set; }

        public List<DataItemParameter> DataItems { get; set; }
    }

    [Serializable]
    public class DataItemParameter
    {
        public DataItemParameter() { }
        public DataItemParameter(int id, string name)
        {
            this.Id = id;
            this.Name = name;
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public List<DataItemProperty> Properties { get; set; }
    }

    [Serializable]
    public class DataItemProperty
    {
        public string Name { get; set; }
    }
}