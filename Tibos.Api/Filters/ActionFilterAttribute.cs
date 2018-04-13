using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tibos.Api.Filters
{
    public class ActionFilterAttribute : Attribute, IActionFilter
    {
        private readonly ILogger<ActionFilterAttribute> logger;
 
         public ActionFilterAttribute(ILoggerFactory loggerFactory)
         {
             logger = loggerFactory.CreateLogger<ActionFilterAttribute>();
         }
 
         public void OnActionExecuted(ActionExecutedContext context)
         {
             logger.LogInformation("ActionFilter Executed!");
         }
 
         public void OnActionExecuting(ActionExecutingContext context)
         {
             logger.LogInformation("ActionFilter Executing!");
         }
    }
}
