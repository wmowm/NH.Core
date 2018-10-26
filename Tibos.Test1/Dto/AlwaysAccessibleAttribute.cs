using Microsoft.AspNetCore.Mvc.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tibos.Test1.Dto
{
    /// <summary>
    /// 允许匿名访问
    /// </summary>
    public class AlwaysAccessibleAttribute: Attribute
    {

        public string Name { get; set; }
    }
}
