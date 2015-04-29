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
            if (uService.AddUser(user)!= false)
            {
                return View();
            }

            return Content("Failed");
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string email, string password)
        {
            if (uService.UserLogin(email, password) != false)
            {
                return Redirect("User/Dashboard");
            }
            else
                Session.Add("uEmail",email);

            return Redirect("Home/Index");
        }

        public ActionResult Logout()
        {
            Session.Clear();
            return Redirect("Home/Index");
        }

        public ActionResult Dashboard()
        {
            //Session.Add("uEmail", "sdf");
            if (Session["uEmail"] != null)
                return View();
            else
                return Redirect("Home/Index");
        }

        public ActionResult MyMashab()
        {
            if (Session["uEmail"] != null)
                return View();
            else
                return Redirect("Home/Index");
        }



    }
}