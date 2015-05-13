using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Mhasb.Services.Users;
using Mhasb.Wsit.Web.Areas.UserManagement.Models;


namespace Mhasb.Wsit.Web.Controllers
{
    public class HomeController : Controller
    {
        private IRoleVsActionService arService = new RoleVsActionService();
        public ActionResult Index()
        {
            if (HttpContext.User.Identity.IsAuthenticated)
                return RedirectToAction("MyMhasb", "Users", new { Area = "UserManagement" });
            var model = new Login();
           // return View(model);
            return View("index_new",model);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
       
    }
}