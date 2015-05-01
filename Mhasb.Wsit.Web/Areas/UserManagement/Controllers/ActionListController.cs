using Mhasb.Domain.Users;
using Mhasb.Services.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Mhasb.Wsit.Web.Areas.UserManagement.Controllers
{
    public class ActionListController :Controller
    {
       private IActionListService actionListService = new ActionListService();

       public ActionResult Index() 
       {
           var model = actionListService.GetAllActionList();
           return View(model);
       }

       public ActionResult Create() {
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