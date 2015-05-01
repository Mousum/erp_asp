using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Mhasb.Wsit.Web.Areas.UserManagement.Models;
using Mhasb.Services.Users;


namespace Mhasb.Wsit.Web.Controllers
{
    public class HomeController : Controller
    {
        private IRoleVsActionService arService = new RoleVsActionService();
        public ActionResult Index()
        {
            
            var model = new Login();
            return View(model);
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