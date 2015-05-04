﻿using Mhasb.Domain.Organizations;
using Mhasb.Domain.Users;
using Mhasb.Services.Organizations;
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
        private readonly ICompanyService iCompany = new CompanyService();
        

        [AllowAnonymous]
        public ActionResult Registration()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult Registration(User user)
        {
            var tnc= Request.Params.Get("tnc");
            if (tnc != null && tnc == "on")
            {
                if (uService.AddUser(user))
                {
                    //return View("RegistrationSuccess");

                    CustomPrincipal.Login(user.Email, user.Password, false);
                    return Redirect("MyMhasb");
                }
                return Content("Failed");
            }
            

            return Content("You Must Agree with our terms and Condition");
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

                return Redirect("MyMhasb");
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
            return RedirectToAction("Index", "Home", new {area="" });

        }

    [AllowAnonymous]
        public ActionResult Dashboard()
        {
             var tt = HttpContext.User.Identity.Name;
            //if (Session["uEmail"] != null)
            //    return View();
            //else
            //return Redirect("Home/Index");
            return View();
        }

        //[AllowAnonymous]
        public ActionResult MyMhasb()
        {
            User user = uService.GetSingleUserByEmail(HttpContext.User.Identity.Name);
            List<Company> myCompanyList = iCompany.GetAllCompanies()
                                                   .Where(c => c.Users.Id == user.Id).ToList();
            

            //User user = uService.GetSingleUserByEmail("zahedwsit@dfg.com");
            ViewBag.userName = user.FirstName + " " + user.LastName ;
            ViewBag.lastLoginCompany = "UniCorn";
            ViewBag.lastLoginTime = DateTime.Now;
            return View("MyMhasb",myCompanyList);

        }



    }
}