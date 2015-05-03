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
            return View();
        }

    }
}