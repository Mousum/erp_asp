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

namespace Mhasb.Wsit.Web.Areas.UserManagement.Controllers
{
    public class LoggedInUserController : BaseController
    {
        //
        // GET: /UserManagement/LoggedInUser/
        private IUserService uService = new UserService();

        private ISettingsService setService = new SettingsService();
        private readonly ICompanyService cService = new CompanyService();
        private readonly IFinalcialSetting fService = new FinalcialSettingService();
        private readonly ICompanyViewLog _companyViewLog = new CompanyViewLogService();



        public ActionResult Dashboard()
        {
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
            var financialSettings = fService.GetCurrentFinalcialSettingByComapny(userSettings.Companies.Id);

            ViewBag.CompanyCurrency = financialSettings.Currencies.Name;



            return View("Deshboard_new");
        }

        public ActionResult MyMhasb()
        {
            User user = uService.GetSingleUserByEmail(HttpContext.User.Identity.Name);
            if (user == null)
            {
                return RedirectToAction("Logout", "Users", new { Area = "UserManagement" });
            }
            //List<Company> myCompanyList = iCompany.GetAllCompanies()
            //                                       .Where(c => c.Users.Id == user.Id).ToList();
            //List<Company> myCompanyList = cService.GetAllCompaniesByUserEmployee(user.Id);
            List<LogView> myCompanyList = cService.GetLastVisitorWiseCompanyList(user.Id);
            var companyArray = uService.GetcompanyByUserID(user.Id);
            //User user = uService.GetSingleUserByEmail("zahedwsit@dfg.com");
            ViewBag.CompanyArr = user.Id;
            var accountsetting = setService.GetAllByUserId(user.Id);
            ViewBag.userName = user.FirstName + " " + user.LastName;
            ViewBag.lastLoginCompany = accountsetting != null ? accountsetting.Companies.DisplayName : "Company was not set.";
            ViewBag.lastLoginTime = DateTime.Now;
            return View("MyMhasb_new", myCompanyList);

        }

	}
}