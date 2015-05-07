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
    public class UserInRolesController :Controller
    {
        private IUserInRoleService userInRoleService = new UserInRoleService();

        public ActionResult Index() 
        {
            return View();
        }

        public ActionResult Create() 
        {
            IUserService uService = new UserService();

            ViewBag.UserList = new SelectList(uService.GetAllUsers(),"Id","FirstName");
            return View();
        }
        [HttpPost]
        public PartialViewResult GetUserInRole(string Id)
        {

            var model = userInRoleService.GetRoleListByUser(Convert.ToInt32(Id));
            return PartialView("GetUserInRole", model);
        }
    }
}