using Mhasb.Domain.Users;
using Mhasb.Services.Loggers;
using Mhasb.Services.Users;
using Mhasb.Wsit.Web.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Mhasb.Wsit.Web.Areas.UserManagement.Controllers
{
    public class RolesController : BaseController
    {
        private IRoleService rService = new RoleService();
        private readonly IUserService uService = new UserService();
        private readonly ISettingsService setService = new SettingsService();
        private readonly ICompanyViewLog _companyViewLog = new CompanyViewLogService();

        public ActionResult Index() {
            var model = rService.GetAllRoles();
            return View(model);
            //return View();
        }

        public ActionResult CreateRole() {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateRole(Role role) 
        {
            User user = uService.GetSingleUserByEmail(HttpContext.User.Identity.Name);
            var logObj = _companyViewLog.GetLastViewCompanyByUserId(user.Id);
            int companyId = 0;
            if (logObj != null)
            {
                companyId = (int)logObj.CompanyId;
            }

            //var accountsetting = setService.GetAllByUserId(user.Id);

            //role.CompanyId = accountsetting.Companies.Id;
            role.CompanyId = companyId;
            if (rService.AddRole(role))
                return RedirectToAction("Index");
            else return Content("Failed to add Role");
        }

        public ActionResult EditRole(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var model = rService.GetSingleRole(id);
            if (model == null)
            {
                return HttpNotFound();
            }
            return View(model);
        }

        [HttpPost]

        public ActionResult EditRole(Role  role)
        {
            rService.EditRole(role);
            //return View();
            return RedirectToAction("Index");
        }


    }
}