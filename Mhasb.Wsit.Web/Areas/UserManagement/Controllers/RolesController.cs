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
    public class RolesController :Controller
    {
        private IRoleService rService = new RoleService();

        public ActionResult Index() {
            var model = rService.GetAllRoles();
            return View(model);
            //return View();
        }

        public ActionResult CreateRole() {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateRole(Role role) 
        {
            rService.AddRole(role);
            return RedirectToAction("Index");
        }

        public ActionResult EditRole(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var model = rService.GetSingleRole(id);
            if (model == null)
            {
                return HttpNotFound();
            }
            return View(model);
        }

        [HttpPost]

        public ActionResult EditRole(Role  role)
        {
            rService.EditRole(role);
            //return View();
            return RedirectToAction("Index");
        }


    }
}