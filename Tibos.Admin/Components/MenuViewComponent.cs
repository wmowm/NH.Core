using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tibos.Service.Contract;

namespace Tibos.Admin.Components
{
    public class MenuViewComponent:ViewComponent
    {
        private NavigationIService _navigationIService;

        public MenuViewComponent(NavigationIService navigationIService)
        {
            this._navigationIService = navigationIService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var list = _navigationIService.GetList();
            return View(list);
        }
    }
}
