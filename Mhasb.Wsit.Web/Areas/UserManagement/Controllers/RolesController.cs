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

        public ActionResult Create() {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Role role) 
        {
            rService.AddRole(role);
            return View();
        }

        public ActionResult Edit()
        {
            return View();
        }

        [HttpPost]

        public ActionResult Edit(Role  role)
        {
            rService.EditRole(role);
            return View();
        }


    }
}