using Mhasb.Domain.Users;
using Mhasb.Services.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Mhasb.Wsit.Web.Areas.UserManagement.Controllers
{
    public class RolesController :Controller
    {
        private IRoleService rService = new RoleService();

        public ActionResult Index() {
            var model = rService.GetAllRoles();
            return View("RoleIndex",model);
        }

        public ActionResult CreateRole() {
            return View();
        }
        [HttpPost]
        public ActionResult CreateRole(Role role) 
        {
            rService.AddRole(role);
            return View();
        }

        public ActionResult EditRole()
        {
            return View();
        }

        [HttpPost]

        public ActionResult EditRole(Role  role)
        {
            rService.EditRole(role);
            return View();
        }


    }
}