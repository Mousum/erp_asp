using Mhasb.Domain.Users;
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

        public ActionResult Index() 
        {
            var model = userInRoleService.GetAllUserInRole();
            return View(model);
        }

        public ActionResult Create() 
        {
            IUserService uService = new UserService();
         
            ViewBag.UserList = new SelectList(uService.GetAllUsers(),"Id","FirstName");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(string EmployeeId, int[] RoleId)
        {
            IEmployeeService eService = new EmployeeService();
            var Uemployee = eService.GetEmpByUserId(Convert.ToInt32(EmployeeId));
            UserInRole uIR = new UserInRole();
            var userInRole = userInRoleService.GetRoleListByUser(Convert.ToInt64(EmployeeId));
            foreach (var item in userInRole)
            {
                if (RoleId.Contains(item.RoleId))
                {

                    uIR.EmployeeId = Uemployee.Id;
                    uIR.RoleId = Convert.ToInt32(item.RoleId);
                    uIR.IsActive = true;
                }
                else
                {
                    uIR.EmployeeId = Uemployee.Id;
                    uIR.RoleId = Convert.ToInt32(item.RoleId);
                    uIR.IsActive = false;
                }
                userInRoleService.AddUserInRole(uIR);

            }
          
            return Content("Role Added Successfully");
           
           
        }

        [HttpPost]
        public PartialViewResult GetUserInRole(string Id)
        {

            var model = userInRoleService.GetRoleListByUser(Convert.ToInt64(Id));
            return PartialView("GetUserInRole", model);
        }
    }
}