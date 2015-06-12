using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Mhasb.Services.Accounts;
using Mhasb.Services.Users;
using Mhasb.Wsit.Web.Areas.UserManagement.Models;


namespace Mhasb.Wsit.Web.Controllers
{
    public class HomeController : CommonBaseController
    {
        private IRoleVsActionService arService = new RoleVsActionService();
        private IUserService uService = new UserService();
        private ISettingsService setService = new SettingsService();
        public ActionResult Index()
        {
            if (HttpContext.User.Identity.IsAuthenticated)
            {

                var user = uService.GetSingleUserByEmail(HttpContext.User.Identity.Name);
                var userSetting = setService.GetAllByUserId(user.Id);

                if (userSetting !=null && userSetting.lglast)
                {
                    string absUrl;
                    if (!checkCompanyFlow(out absUrl))
                    {
                        return Redirect(absUrl);
                    }
                    return RedirectToAction("Dashboard", "Users", new { Area = "UserManagement" });

                }
                else
                    return RedirectToAction("MyMhasb", "Users", new { Area = "UserManagement" });
            }
                
            var model = new Login();
           // return View(model);
            //return View("Index",model);
            return View();
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Contact()
        {
            return View();
        }

        public ActionResult Clients()
        {
            return View();
        }
        public ActionResult AccessDenied() {
            var tt = HttpContext.User.Identity.Name;
            ViewData["message"] = "Permission Denied!!!!! You do not have permission to access this page";
            ViewData["Session"]=tt;
            return View();
        }
       
    }
}