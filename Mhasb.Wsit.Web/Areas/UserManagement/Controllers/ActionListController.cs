using Mhasb.Domain.Users;
using Mhasb.Services.Users;
using Mhasb.Wsit.Web.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PagedList;
using PagedList.Mvc;

namespace Mhasb.Wsit.Web.Areas.UserManagement.Controllers
{
    public class ActionListController : BaseController
    {
        private IActionListService actionListService = new ActionListService();

        public ActionResult Index()
        {
           // var model = actionListService.GetAllActionList();
            return View();
        }
        
        
        //method for paging and search option
        public ActionResult List(string currentFilter, string searchString, int? page)
        {
            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            ViewBag.CurrentFilter = searchString;
            List<ActionList> Al = actionListService.GetAllActionList();
            if (!String.IsNullOrEmpty(searchString))
            {
                Al = Al.Where(s => s.ActionName.Contains(searchString)
                                   || s.ControllerName.Contains(searchString)
                                   || s.ModuleName.Contains(searchString)).ToList();
            }

            int pageSize = 5;
            int pageNumber = (page ?? 1);
            return PartialView(Al.ToPagedList(pageNumber, pageSize));

        }


        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ActionList actionList)
        {
            actionListService.AddActionList(actionList);
            return RedirectToAction("Index");
        }
        public ActionResult Edit(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var model = actionListService.GetSingleActionList(id);
            if (model == null)
            {
                return HttpNotFound();
            }
            return View(model);
        }

        [HttpPost]

        public ActionResult Edit(ActionList actionList)
        {
            actionListService.UpdateActionList(actionList);
            //return View();
            return RedirectToAction("Index");
        }

    }

}