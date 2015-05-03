using Mhasb.Domain.Users;
using Mhasb.Services.Users;
using Mhasb.Wsit.Web.AuthSecurity;
using Mhasb.Wsit.Web.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Mhasb.Wsit.Web.Areas.UserManagement.Controllers
{
    public class UsersController : BaseController
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

        [AllowAnonymous]
        public ActionResult Registration()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult Registration(User user)
        {


            if (uService.AddUser(user))
            {
                return View("RegistrationSuccess");
            }

            return Content("Failed");
        }
        
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Login(string email, string password)
        {
            if (CustomPrincipal.Login(email, password,false) != false)
            {
                return Redirect("Dashboard");
            }
            else
                Session.Add("uEmail", email);
            return Redirect(Url.Content("~/"));
        }

        [AllowAnonymous]
        public ActionResult Logout()
        {
            //Session.Clear();
            CustomPrincipal.Logout();
            return Redirect("~/");

        }


        public ActionResult Dashboard()
        {
             var tt = HttpContext.User.Identity.Name;
            //if (Session["uEmail"] != null)
            //    return View();
            //else
            //return Redirect("Home/Index");
            return View();
        }

        public ActionResult MyMashab()
        {

            if (Session["uEmail"] != null)
                return View();
            else
                return Redirect(Url.Content("~/"));
               // return RedirectToAction("Index", "Home", new { area = "Controllers" });
        }



    }
}