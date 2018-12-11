using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Tibos.Common;
using Tibos.Domain;
using Tibos.Service.Contract;

namespace Tibos.Admin.Areas.SYS.Controllers
{
    [Area("SYS")]
    public class RoleController : Controller
    {
        public RoleIService _RoleIService { get; set; }
        public IActionResult Index()
        {
            return View();
        }




        public IActionResult Create(string Id)
        {
            var model = new Role();
            if (!string.IsNullOrEmpty(Id))
            {
                model = _RoleIService.Get(Id);
            }
            return View(model);
        }
        public IActionResult Edit(string Id)
        {
            var model = new Role();
            if (!string.IsNullOrEmpty(Id))
            {
                model = _RoleIService.Get(Id);
            }
            return View(model);
        }

        public IActionResult Detail()
        {
            return View();
        }


        public ActionResult GetFont()
        {
            return PartialView("~/Areas/SYS/Views/Role/_Font.cshtml");
        }





        [HttpPost]
        public JsonResult List(RoleRequest request)
        {

            var list = _RoleIService.GetList(request);

            List<Role> nav_list = new List<Role>();

        


            Json reponse = new Json();
            reponse.code = 200;
            reponse.total = nav_list.Count;
            reponse.data = nav_list;
            return Json(reponse);
        }


        [HttpPost]
        public JsonResult Create(RoleRequest request)
        {
            Role model = new Role()
            {

            };
            var id = _RoleIService.Save(model);


         
            Json reponse = new Json();
            reponse.code = 200;
            reponse.status = 0;
            return Json(reponse);
        }

        [HttpPost]
        public JsonResult Edit(RoleRequest request)
        {
            Json reponse = new Json();
            Role model = new Role()
            {

            };
            _RoleIService.Update(model);
            return Json(reponse);
        }

        [HttpPost]
        public JsonResult Del(string Id)
        {
            _RoleIService.Delete(Id);
            Json reponse = new Json();
            reponse.code = 200;
            return Json(reponse);
        }
    }
}