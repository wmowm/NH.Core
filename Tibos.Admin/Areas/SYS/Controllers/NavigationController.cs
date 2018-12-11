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
    public class NavigationController : Controller
    {
        public NavigationIService _NavigationIService { get; set; }
        public IActionResult Index()
        {
            return View();
        }




        public IActionResult Create(string Id)
        {
            var model = new Navigation();
            if (!string.IsNullOrEmpty(Id))
            {
               model = _NavigationIService.Get(Id);
            }
            return View(model);
        }
        public IActionResult Edit(string Id)
        {
            var model = new Navigation();
            if (!string.IsNullOrEmpty(Id))
            {
                model = _NavigationIService.Get(Id);
            }
            return View(model);
        }

        public IActionResult Detail()
        {
            return View();
        }


        public ActionResult GetFont()
        {
            return PartialView("~/Areas/SYS/Views/Navigation/_Font.cshtml");
        }


        [HttpPost]
        public JsonResult ListTree(NavigationRequest request)
        {
            request.sortKey = "Sort";
            request.sortType = 0;
            var list = _NavigationIService.GetList(request);
            var count = _NavigationIService.GetCount(request);
            List<zTree> list_ztree = new List<zTree>();
            zTree ztree = new zTree()
            {
                id = "0",
                pId = "#",
                name = "系统",
                noEditBtn = true,
                noRemoveBtn = true,
                open = true
            };
            list_ztree.Add(ztree);
            foreach (var item in list)
            {
                ztree = new zTree()
                {
                    id = item.Id,
                    pId = item.ParentId ?? "0",
                    name = item.Name,
                    open = true
                };
                if (item.IsSys == 1)
                {
                    ztree.noEditBtn = true;
                    ztree.noRemoveBtn = true;
                }
                list_ztree.Add(ztree);
            }

            Json reponse = new Json();
            reponse.code = 200;
            reponse.total = count;
            reponse.data = list_ztree;
            return Json(reponse);
        }



        [HttpPost]
        public JsonResult List(NavigationRequest request)
        {
            request.sortKey = "Sort";
            request.sortType = 0;
            request.Level = 1;
            var list = _NavigationIService.GetList(request);

            List<Navigation> nav_list = new List<Navigation>();

            foreach (var item in list)
            {
                nav_list.Add(item);
                request.Level = 2;
                request.ParentId = item.Id;
                var sub_list = _NavigationIService.GetList(request);
                nav_list.AddRange(sub_list);
            }


            Json reponse = new Json();
            reponse.code = 200;
            reponse.total = nav_list.Count;
            reponse.data = nav_list;
            return Json(reponse);
        }


        [HttpPost]
        public JsonResult Create(NavigationRequest request)
        {
            Navigation model = new Navigation()
            {
                Areas = request.Areas,
                ControllerName = request.ControllerName,
                Icon = request.Icon,
                Id = Guid.NewGuid().GuidTo16String(),
                IsSys = request.IsSys,
                Link = request.Link,
                Name = request.Name,
                ParentId = request.ParentId,
                Sort = request.Sort,
                Level = request.Level
            };
            var id = _NavigationIService.Save(model);


            zTree ztree = new zTree()
            {
                id = model.Id,
                pId = model.ParentId ?? "0",
                name = model.Name,
                open = true
            };
            if (model.IsSys == 1)
            {
                ztree.noEditBtn = true;
                ztree.noRemoveBtn = true;
            }

            Json reponse = new Json();
            reponse.code = 200;
            reponse.status = 0;
            reponse.data = ztree;
            return Json(reponse);
        }

        [HttpPost]
        public JsonResult Edit(NavigationRequest request)
        {
            Json reponse = new Json();
            Navigation model = new Navigation()
            {
                Areas = request.Areas,
                ControllerName = request.ControllerName,
                Icon = request.Icon,
                Id = request.Id,
                IsSys = request.IsSys,
                Link = request.Link,
                Name = request.Name,
                ParentId = request.ParentId,
                Sort = request.Sort,
                Level = request.Level
            };
            _NavigationIService.Update(model);

            zTree ztree = new zTree()
            {
                id = model.Id,
                pId = model.ParentId ?? "0",
                name = model.Name,
                open = true
            };
            if (model.IsSys == 1)
            {
                ztree.noEditBtn = true;
                ztree.noRemoveBtn = true;
            }


            reponse.code = 200;
            reponse.status = 0;
            reponse.data = ztree;
            return Json(reponse);
        }

        [HttpPost]
        public JsonResult Del(string Id)
        {
            _NavigationIService.Delete(Id);
            Json reponse = new Json();
            reponse.code = 200;
            return Json(reponse);
        }
    }
}