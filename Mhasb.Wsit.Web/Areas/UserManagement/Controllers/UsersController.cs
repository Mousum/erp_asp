using Mhasb.Domain.Users;
using Mhasb.Services.Users;
using Mhasb.Wsit.Web.AuthSecurity;
using Mhasb.Wsit.Web.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
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


        public ActionResult Registration()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Registration(User user)
        {
            if (uService.CheckUserExistence(user.Email))
                return Content("Already Exist");
            if (uService.AddUser(user) != false)
            {
                return View("RegistrationSuccess");
            }

            return Content("Registration Failed");
        }
        
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string email, string password)
        {
            if (CustomPrincipal.Login(email, password,false))
            {
                Session.Add("uEmail",email);
                return Redirect("Dashboard");
            }
            else
                return Redirect("~/Home/Index");
        }

        public ActionResult Logout()
        {
            //Session.Clear();
            CustomPrincipal.Logout();
            return Redirect("~/");

        }

        public ActionResult Dashboard()
        {

            if (HttpContext.User.Identity.IsAuthenticated)
                return View();
            else
                return Redirect("~/");
        }

        public ActionResult MyMashab()
        {

            if (Session["uEmail"] != null)
                return View();
            else
                return Redirect(Url.Content("~/"));
               // return RedirectToAction("Index", "Home", new { area = "Controllers" });
        }

        public bool sendMail(string to,string mailSubject, string mailBody)
        {
            var fromAddress = new MailAddress("sumon20@gmail.com", "From Name");
            var toAddress = new MailAddress(to, "To Name");
             string fromPassword = "638848";
             string subject = mailSubject;
             string body = mailBody;

            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
            };
            using (var message = new MailMessage(fromAddress, toAddress)
            {
                Subject = subject,
                Body = body
            })
            {
                smtp.Send(message);
            }

            return true;
        }


    }
}