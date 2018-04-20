using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Tibos.Common;
using Tibos.Service.Contract;

namespace Tibos.Api.Filters
{
    public class ActionFilterAttribute : Attribute, IActionFilter
    {
        
        private readonly ILogger<ActionFilterAttribute> logger;
        private readonly IMemoryCache _Cache;
        private readonly UsersIService _UsersService;
        public ActionFilterAttribute(ILoggerFactory loggerFactory, IMemoryCache memoryCache, UsersIService userService)
        {
            logger = loggerFactory.CreateLogger<ActionFilterAttribute>();
            _Cache = memoryCache;
            _UsersService = userService;
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            #region 记录日志(所有的请求)
            MonitorLog MonLog = new MonitorLog();
            MonLog.ExecuteStartTime = Convert.ToDateTime(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.ffff", DateTimeFormatInfo.InvariantInfo));
            MonLog.ControllerName = context.RouteData.Values["controller"] as string;
            MonLog.ActionName = context.RouteData.Values["action"] as string;
            MonLog.QueryCollections = context.HttpContext.Request.QueryString;//Url 参数
            if (string.IsNullOrEmpty(MonLog.QueryCollections.ToString()) && context.ActionArguments.Count != 0)
            {
                try
                {
                    MonLog.BodyCollections = JsonConvert.SerializeObject(context.ActionArguments["dic"]);
                }
                catch
                {

                }
            }
            logger.LogInformation(MonLog.GetLoginfo());
            #endregion
            #region 权限验证
            //1.忽略权限验证的部分(如果要忽略的部分过多,可以提取成方法)
            if (MonLog.ControllerName.ToLower() == "user" && MonLog.ActionName.ToLower() == "gettoken") return;
            //2.根据token获取用户实体对象
            //3.用户->职位->角色->是否具备操作权限

            var token = context.HttpContext.Request.Headers["token"].ToString();
            Json json = CheckToken(token);
            if (json.status != 0)
            {
                context.Result = new JsonResult(json);
                return;
            }
            #endregion
        }
        public void OnActionExecuted(ActionExecutedContext context)
        {

        }


        /// <summary>
        /// 验证Token
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public Json CheckToken(string token)
        {
            Json json = new Json();
            if (string.IsNullOrEmpty(token))
            {
                json.msg = "token不能为空!";
                json.status = -1;
                return json;
            }
            var id = _Cache.Get(token);
            if (id == null)
            {
                json.msg = "token已失效!";
                json.status = -1;
                return json;
            }
            var model = _UsersService.Get(Convert.ToInt32(id));
            return json;
        }
    }
}
