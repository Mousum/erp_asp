using Mhasb.Domain.Users;
using Mhasb.Services.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Mhasb.Wsit.Web.Areas.UserManagement.Controllers
{
    public class UsersController : Controller
    {
        private IUserService uService = new UserService();
        //
        // GET: /UserManagement/Users/
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(User user)
        {
            user.ConfirmPassword = user.Password;
            user.CreatedTime = DateTime.Now;
            uService.AddUser(user);

            return View();
        }


        public ActionResult Registration()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Registration(User user)
        {
            uService.AddUser(user);

            return View();
        }

        public ActionResult Login()
        {
            return View();
        }
    }
}