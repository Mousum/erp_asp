﻿using Mhasb.Domain.Users;
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

            user.CreatedTime = DateTime.Now;
            uService.AddUser(user);
            return null;
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
                return Content("Sucessfull");
            }

            return Redirect("Home/Index");
        }



    }
}