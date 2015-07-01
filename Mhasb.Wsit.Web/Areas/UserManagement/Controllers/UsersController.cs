using Mhasb.Domain.Organizations;
using Mhasb.Domain.Users;
using Mhasb.Services.Commons;
using Mhasb.Services.Loggers;
using Mhasb.Services.Organizations;
using Mhasb.Services.OrgSettings;
using Mhasb.Services.Users;
using Mhasb.Wsit.CustomModel.Organizations;
using Mhasb.Wsit.Web.Areas.UserManagement.Models;
using Mhasb.Wsit.Web.AuthSecurity;
using Mhasb.Wsit.Web.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Mhasb.Wsit.Web.Utilities;
using Mhasb.Domain.Loggers;

namespace Mhasb.Wsit.Web.Areas.UserManagement.Controllers
{

    public class UsersController : CommonBaseController
    {
        private IUserService uService = new UserService();

        private ISettingsService setService = new SettingsService();
        private readonly IAreaTimeService iTimeZone = new AreaTimeService();
        private readonly ICompanyService cService = new CompanyService();
        private readonly IFinalcialSetting fService = new FinalcialSettingService();
        private readonly ICompanyViewLog _companyViewLog = new CompanyViewLogService();
        private readonly ICountryService countryService = new CountryService();
        //private readonly ICompanyService iCompany = new CompanyService();

        //
        // GET: /UserManagement/Users/
        public ActionResult Create()
        {
            return View();
        }

        //[HttpPost]
        //public ActionResult Create(User user)
        //{
        //    user.ConfirmPassword = user.Password;
        //    user.CreatedTime = DateTime.Now;
        //    uService.AddUser(user);

        //    return View();
        //}

        //private readonly ICompanyService iCompany = new CompanyService();



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
                    Encryptor encrypt = new Encryptor();
                    user.Password = encrypt.GetMD5(user.Password);
                    user.ConfirmPassword = encrypt.GetMD5(user.ConfirmPassword);
                    if (uService.AddUser(user))
                    {
                        //return View("RegistrationSuccess");

                        CustomPrincipal.Login(user.Email, user.Password, false);
                        return RedirectToAction("MyMhasb", "Users", new { Area = "UserManagement" });
                    }
                    else 
                    {
                        ModelState.AddModelError("Msg", "Registration Failed!!!");
                        // return Content("Already Register");
                        return View();
                    }
                }
                else
                {
                    ModelState.AddModelError("Msg", "Already Register");
                    // return Content("Already Register");
                    return View();
                }

            }

            else
            {
                ModelState.AddModelError("Msg", "You Must Agree with Terms and Condition....");
                // return Content("Already Register");
                return View();
            }
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
            Encryptor encryptor = new Encryptor();
            password = encryptor.GetMD5(password);
            if (CustomPrincipal.Login(email, password, false))
            {
                var user = uService.GetSingleUserByEmail(email);
                var userSetting = setService.GetAllByUserId(user.Id);

                if(userSetting!=null && userSetting.lglast==true)
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

        public ActionResult Dashboard()
        {
            string absUrl;
            if (!checkCompanyFlow(out absUrl))
            {
                return Redirect(absUrl);
            }
            var tt = HttpContext.User.Identity.Name;
            var user = uService.GetSingleUserByEmail(tt);
            var userSettings = setService.GetAllByUserId(user.Id);
            var logObj = _companyViewLog.GetLastViewCompanyByUserId(user.Id);
            int companyId = 0;
            if (logObj != null)
            {
                companyId = (int)logObj.CompanyId;
            }
            var activatedCompany = cService.GetSingleCompany(companyId);
            ViewBag.CompanyLocation = activatedCompany.Location;
            //var financialSettings = fService.GetCurrentFinalcialSettingByComapny(userSettings.Companies.Id);
            var financialSettings = fService.GetCurrentFinalcialSettingByComapny(logObj.Companies.Id);
            if(financialSettings==null)
            {
                TempData.Add("errMsg","No financial settings found for current date...Please Add Financial settings.");
                return RedirectToAction("Create", "FinalcialSetting", new { area = "OrgSettings" });
            }

            ViewBag.CompanyCurrency = financialSettings.Currencies.Name;



            return View("Deshboard_new");
        }

        public ActionResult MyMhasb()
        {
            try
            {
                User user = uService.GetSingleUserByEmail(HttpContext.User.Identity.Name);
                if (user == null)
                {
                    return RedirectToAction("Logout");
                }

                var myCompanyList = cService.GetLastVisitorWiseCompanyList(user.Id);

                var companyArray = uService.GetcompanyByUserID(user.Id);
                //User user = uService.GetSingleUserByEmail("zahedwsit@dfg.com");
                ViewBag.CompanyArr = user.Id;
                //var accountsetting = setService.GetAllByUserId(user.Id);
                var accountsetting = _companyViewLog.GetLastViewCompanyByUserId(user.Id);
                ViewBag.userName = user.FirstName + " " + user.LastName;
                ViewBag.lastLoginCompany = accountsetting != null ? accountsetting.Companies.TradingName : "Company was not set.";
                ViewBag.lastLoginTime = DateTime.Now;
                return View("MyMhasb_new", myCompanyList);
            }
            catch (Exception ex)
            {
                var rr = ex.Message;
                ModelState.AddModelError("Msg", rr);
                // return Content("Already Register");
                return View("MyMhasb_new",new List<LogView>());
                
            }
            

        }
        public JsonResult GetCompany()
        {

            User user = uService.GetSingleUserByEmail(HttpContext.User.Identity.Name);
            var myCompanyList = cService.GetAllCompaniesByUserEmployee(user.Id).Select(c => new { c.Id, DisplayName = c.TradingName }).ToList();

            if (myCompanyList.Count() <= 0)
            {
                var success = "False";
                return Json(success, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(myCompanyList, JsonRequestBehavior.AllowGet);
            }

        }
        public JsonResult GetSelectedCompanyName()
        {
            User user = uService.GetSingleUserByEmail(HttpContext.User.Identity.Name);
            var setObj = setService.GetAllByUserId(user.Id);
            if (setObj == null)
            {
                var success = "False";
                return Json(success, JsonRequestBehavior.AllowGet);
            }
            else
            {

                return Json(setObj, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult AccountSettings()
        {

            var accsetting = new AccountSetting();
            var email = HttpContext.User.Identity.Name;
            var users = uService.GetSingleUserByEmail(email);
            users.Password = "Password is hidden.";
            try
            {
                accsetting.Users = users;
                accsetting.AccSettings = new Settings();
                ViewBag.TimeZoneList = new SelectList(iTimeZone.GetAllAreaTimes(), "Id", "ZoneName");


                ViewBag.CompanyList = new SelectList(cService.GetAllCompaniesByUserEmployee(users.Id), "Id", "DisplayName");
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

            if (String.IsNullOrEmpty(Email))
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
            var Encryptor = new Encryptor();
            User.Password = Encryptor.GetMD5(password);
            User.ConfirmPassword = Encryptor.GetMD5(password);
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
        public ActionResult UpdateSettings(bool lgcompany, bool lgdash, bool lglast, int TimezoneId, int? ComanyId)
        //public ActionResult UpdateSettings(Settings setting)
        {

            var email = HttpContext.User.Identity.Name;
            var users = uService.GetSingleUserByEmail(email);
            var setObj = setService.GetAllByUserId(users.Id);

            //var logEntry = new CompanyViewLog()AddCompanyViewLog;
            //logEntry.UserId = users.Id;
            //logEntry.CompanyId = ComanyId;
            //logEntry.LoginTime = DateTime.Now;
            ////logEntry.IpAddress = "";
            //_companyViewLog.AddCompanyViewLog(logEntry);

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
        [HttpPost]
        public ActionResult UpdateCompnay(int ComanyId)
        {
            var email = HttpContext.User.Identity.Name;
            var users = uService.GetSingleUserByEmail(email);
            var setObj = setService.GetAllByUserId(users.Id);
            var logEntry = new CompanyViewLog();
            logEntry.UserId = users.Id;
            logEntry.CompanyId = ComanyId;
            logEntry.LoginTime = DateTime.Now;
            //logEntry.IpAddress = "";
            _companyViewLog.AddCompanyViewLog(logEntry);

            try
            {
                if (setObj != null)
                {
                    setObj.userId = users.Id;
                    setObj.CompanyId = ComanyId;
                    setService.UpdateSettings(setObj);
                    return Json(new { success = "True" });
                }
                else
                {
                    Settings newObj = new Settings();
                    newObj.userId = users.Id;
                    newObj.CompanyId = ComanyId;
                    setService.AddSettings(newObj);

                    return Json(new { success = "True" });
                }
            }
            catch (Exception ex)
            {
                var msg = ex.Message;
                return Json(new { success = "False" });
            }



        }

        public JsonResult GetSettingsByUserId ()
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

                var rr = ex.Message;
            }


            return Json(setObj, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetUserProfile()
        {
            User user = uService.GetSingleUserByEmail(HttpContext.User.Identity.Name);
            var myProfule = uService.GetUserProfile(user.Id);
            
            if (myProfule == null)
            {
                var success = "False";
                return Json(success, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var dataSet = new { myProfule.FirstName, myProfule.LastName,
                                    Bio = (myProfule.EmployeeProfiles!=null)? myProfule.EmployeeProfiles.Bio: "",
                    ImageLocation=(myProfule.EmployeeProfiles!=null)? myProfule.EmployeeProfiles.ImageLocation : "" 
                };
                return Json(dataSet, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult Finish() {

            User user = uService.GetSingleUserByEmail(HttpContext.User.Identity.Name);
            var logObj = _companyViewLog.GetLastViewCompanyByUserId(user.Id);
            if (logObj.Companies.CompleteFlag == 5 )
            {
                return RedirectToAction("MyMhasb", "Users", new { Area = "UserManagement" });
            }
            else if(logObj.Companies.CompleteFlag == 4)
            {
                return View();
            }
            else
            {
                string absUrl;
                if (!checkCompanyFlow(out absUrl))
                {
                    return Redirect(absUrl);
                }
                TempData.Add("errMsg", "Something Wrong...");
                return RedirectToAction("MyMhasb", "Users", new { Area = "UserManagement" });
            }

            
        }
        [HttpPost]
        // ReSharper disable once MethodOverloadWithOptionalParameter
        public ActionResult Finish(int flag=5)
        {
            var user = uService.GetSingleUserByEmail(HttpContext.User.Identity.Name);
            var logObj = _companyViewLog.GetLastViewCompanyByUserId(user.Id);
            int companyId = 0;
            if (logObj != null)
                companyId = (int) logObj.CompanyId;
            if (cService.UpdateCompleteFlag(companyId, flag))
            {
                return RedirectToAction("Dashboard", "Users", new { Area = "UserManagement" });
            }
            return View(); //RedirectToAction("Finish", "Usres", new { area = "UserManagement" });
        }
        [HttpPost]
        public ActionResult MatchPassword(string oldpass, string newPass)
        {
          var user = uService.GetSingleUserByEmail(HttpContext.User.Identity.Name);
          var Encrypted = new Encryptor();
          if (user.Password == Encrypted.GetMD5(oldpass))
          {
              if (newPass != "") 
              {
                  if (Encrypted.GetMD5(newPass.ToString()) == user.Password) 
                  {
                      return Json(new { msg="OldAndNewPassAreSame",flag="false"});
                  }
                  return Json(new { msg="Alright",flag="true"});
              }
              return Json(new { msg="OldPassMatched",flag="true"});
          }
          else 
          {
              return Json(new{msg="InvalidOldPass",flag="false"});
          }
        }
    }
    
}