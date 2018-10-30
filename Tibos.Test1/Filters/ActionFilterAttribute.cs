using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Tibos.Test1.Dto;

namespace Tibos.Test1.Filters
{
    public class ActionFilterAttribute : Attribute, IActionFilter
    {

        private readonly ILogger<ActionFilterAttribute> logger;
        private readonly IMemoryCache _Cache;

        public ActionFilterAttribute(ILoggerFactory loggerFactory, IMemoryCache memoryCache)
        {
            logger = loggerFactory.CreateLogger<ActionFilterAttribute>();
            _Cache = memoryCache;
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {

            var authoritybtnAttribute = ((Microsoft.AspNetCore.Mvc.Controllers.ControllerActionDescriptor)context.ActionDescriptor).MethodInfo.GetCustomAttributes(typeof(AlwaysAccessibleAttribute), true);



            #region 根据注解允许匿名访问

            var actionDescriptor = context.ActionDescriptor as ControllerActionDescriptor;
            var controllerAttributes = actionDescriptor.MethodInfo.GetCustomAttributes(typeof(AlwaysAccessibleAttribute), false);
            if (controllerAttributes != null && controllerAttributes.Length > 0)
            {
                return;
            }
            #endregion



        }
        public void OnActionExecuted(ActionExecutedContext context)
        {

        }
    }
}
