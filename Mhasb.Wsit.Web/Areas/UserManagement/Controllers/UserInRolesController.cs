using Mhasb.Domain.Users;
using Mhasb.Services.Loggers;
using Mhasb.Services.Organizations;
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
    public class UserInRolesController : BaseController
    {
        private IUserInRoleService userInRoleService = new UserInRoleService();
        private IUserService uService = new UserService();
        private ISettingsService setService = new SettingsService();
        private IEmployeeService eService = new EmployeeService();
        private IRoleService rService    = new RoleService();
        private readonly ICompanyViewLog _companyViewLog = new CompanyViewLogService();

        public ActionResult Index() 
        {
            
            var model = userInRoleService.GetAllUserInRole();
            return View(model);
        }

        public ActionResult Create() 
        {
            IUserService uService = new UserService();
            var user = uService.GetSingleUserByEmail(HttpContext.User.Identity.Name);
            var AccSet = setService.GetAllByUserId(user.Id);
            var logObj = _companyViewLog.GetLastViewCompanyByUserId(user.Id);
            int companyId = 0;
            if (logObj != null)
            {
                companyId = (int)logObj.CompanyId;
            }
            try
            {
                var Emp = eService.GetEmpByCompanyId(companyId).Select(u => new { Id = u.Users.Id, Name = u.Users.FirstName + " " + u.Users.LastName });//there will be employees from employee service under compnies of the user Loged in
                ViewBag.UserList = new SelectList(Emp, "Id", "Name");
            }
            catch (Exception ex)
            {
                var msg = ex.Message;

            }
           // ViewBag.UserList = new SelectList(uService.GetAllUsers(),"Id","FirstName");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(string EmployeeId, int[] RoleId)
        {
           // IEmployeeService eService = new EmployeeService();
           // var Uemployee = eService.GetEmpByUserId(Convert.ToInt32(EmployeeId));
            
            var userInRole = userInRoleService.GetRoleListByUser(Convert.ToInt64(EmployeeId));
           // var userInRole = rService.GetAllRoles();
            foreach (var item in userInRole)
            {
                var uIR = new UserInRole();
                if (RoleId.Contains(item.RoleId))
                {

                    uIR.EmployeeId = Convert.ToInt32(EmployeeId);
                    uIR.RoleId = Convert.ToInt32(item.RoleId);
                    uIR.IsActive = true;
                }
                else
                {
                    uIR.EmployeeId = Convert.ToInt32(EmployeeId);
                    uIR.RoleId = Convert.ToInt32(item.RoleId);
                    uIR.IsActive = false;
                }
                userInRoleService.AddUserInRole(uIR);

            }
            TempData.Add("SucMsg","Role Added Successfully");
            return RedirectToAction("Index", "UserInRoles", new { area = "UserManagement" });
           
           
        }

        [HttpPost]
        public PartialViewResult GetUserInRole(string Id)
        {

            var model = userInRoleService.GetRoleListByUser(Convert.ToInt64(Id));
            return PartialView("GetUserInRole", model);
        }
    }
}