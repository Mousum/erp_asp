using Mhasb.Domain.Organizations;
using Mhasb.Domain.Users;
using Mhasb.Services.Commons;
using Mhasb.Services.Organizations;
using Mhasb.Services.Users;
using Mhasb.Wsit.Web.Areas.UserManagement.Models;
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

        private ISettingsService setService = new SettingsService();
        private readonly IAreaTimeService iTimeZone = new AreaTimeService();
        private readonly ICompanyService cService = new CompanyService();

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

        private readonly ICompanyService iCompany = new CompanyService();



        [AllowAnonymous]
        public ActionResult Registration()
        {
            //return View();
            return View("Registration_new");
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult Registration(User user)
        {
            var tnc = Request.Params.Get("tnc");
            if (tnc != null && tnc == "on")
            {
                if (uService.GetSingleUserByEmail(user.Email) == null)
                {
                    if (uService.AddUser(user))
                    {
                        //return View("RegistrationSuccess");

                        CustomPrincipal.Login(user.Email, user.Password, false);
                        return Redirect("MyMhasb");
                    }
                }
                else
                {
                    ModelState.AddModelError("Msg", "Already Register");
                    // return Content("Already Register");
                    return View();
                }

            }


            return Content("You Must Agree with our terms and Condition");
        }

        public ActionResult Login()
        {
            if (HttpContext.User.Identity.IsAuthenticated)
                return RedirectToAction("MyMhasb", "Users", new { Area = "UserManagement" });
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Login(string email, string password)
        {
            if (CustomPrincipal.Login(email, password, false) != false)
            {

                return Redirect("MyMhasb");
            }
            else
            {
                Session.Add("msg", "Invelid Email Or PassWord");
                return Redirect(Url.Content("~/"));
            }

        }

        [AllowAnonymous]
        public ActionResult Logout()
        {
            //Session.Clear();
            CustomPrincipal.Logout();
            return RedirectToAction("Index", "Home", new { area = "" });

        }

        [AllowAnonymous]
        public ActionResult Dashboard()
        {
            var tt = HttpContext.User.Identity.Name;
            //if (Session["uEmail"] != null)
            //    return View();
            //else
            //return Redirect("Home/Index");
            return View("Deshboard_new");
        }

        //[AllowAnonymous]
        public ActionResult MyMhasb()
        {
            User user = uService.GetSingleUserByEmail(HttpContext.User.Identity.Name);
            List<Company> myCompanyList = iCompany.GetAllCompanies()
                                                   .Where(c => c.Users.Id == user.Id).ToList();


            //User user = uService.GetSingleUserByEmail("zahedwsit@dfg.com");
            ViewBag.userName = user.FirstName + " " + user.LastName;
            ViewBag.lastLoginCompany = "UniCorn";
            ViewBag.lastLoginTime = DateTime.Now;
            return View("MyMhasb_new", myCompanyList);

        }

        public ActionResult AccountSettings()
        {

            var accsetting = new AccountSetting();
            var email = HttpContext.User.Identity.Name;
            var users = uService.GetSingleUserByEmail(email);
            try
            {
                accsetting.Users = users;
                accsetting.AccSettings = new Settings();
                ViewBag.TimeZoneList = new SelectList(iTimeZone.GetAllAreaTimes(), "Id", "ZoneName");
                ViewBag.CompanyList = new SelectList(cService.GetAllCompaniesByUserId(users.Id), "Id", "DisplayName");
            }
            catch (Exception ex)
            {
                
                var rr = ex.Message;
            }
          

            return View("AccountSettings_new", accsetting);

        }
        [HttpPost]
        public ActionResult UpdateEmail(string Email)
        {

            if (String.IsNullOrEmpty(Email) )
            {
                return Json(new { msg = "False" });
            }
            else
            {
                var exitsEmal = uService.GetSingleUserByEmail(Email);
                if (exitsEmal == null)
                {
                    var email = HttpContext.User.Identity.Name;
                    var users = uService.GetSingleUserByEmail(email);
                    users.Email = Email;
                    users.Password = users.Password;
                    users.ConfirmPassword = users.Password;
                    var msg = uService.UpdateUser(users);
                    if (msg)
                    {
                        CustomPrincipal.Login(Email, users.Password, false);
                        return Json(new { success = "True", msg = Email, msgpass = users.Password });
                    }
                    else
                    {

                        return Json(new { success = "False" });
                    }

                }
                else
                {
                    return Json(new { success = "False" });
                }

            }
        }
        [HttpPost]
        public ActionResult UpdateUserPassword(string password)
        {
            var email = HttpContext.User.Identity.Name;
            var User = uService.GetSingleUserByEmail(email);
            User.Password = password;
            User.ConfirmPassword = password;
            if (uService.UpdateUser(User))
            {
                CustomPrincipal.Login(email, User.Password, false);
                return Json(new { success = "True", msg = email, msgpass = User.Password });
            }
            else 
            {
                return Json(new { success = "False" });
            }
          
        }

        [HttpPost]
        public ActionResult UpdateSettings(bool lgcompany, bool lgdash, bool lglast, int TimezoneId, int ComanyId)
        //public ActionResult UpdateSettings(Settings setting)
        {

            var email = HttpContext.User.Identity.Name;
            var users = uService.GetSingleUserByEmail(email);
            var setObj = setService.GetAllByUserId(users.Id);

            if (setObj != null)
            {
                setObj.userId = users.Id;
                setObj.lgcompany = lgcompany;
                setObj.lgdash = lgdash;
                setObj.lglast = lglast;
                setObj.TimezoneId = TimezoneId;
                setObj.CompanyId = ComanyId;
                setService.UpdateSettings(setObj);
                return Json(new { success = "True" });
            }
            else
            {
                Settings newObj = new Settings();
                newObj.userId = users.Id;
                newObj.lgcompany = lgcompany;
                newObj.lgdash = lgdash;
                newObj.lglast = lglast;
                newObj.TimezoneId = TimezoneId;
                newObj.CompanyId = ComanyId;
                setService.AddSettings(newObj);

                return Json(new { success = "True" });
            }


        }

        public JsonResult GetSettingsByUserId()
        {
            var setObj = new Settings();
            try
            {
                var email = HttpContext.User.Identity.Name;
                var users = uService.GetSingleUserByEmail(email);
                setObj = setService.GetAllByUserId(users.Id);
            }
            catch (Exception ex)
            {
                
                var rr =ex.Message;
            }
           

            return Json(setObj, JsonRequestBehavior.AllowGet);
        }
        [AllowAnonymous]
        public ActionResult Ledgersettings() {
           
            return View();
        }



    }
}