using Mhasb.Domain.Organizations;
using Mhasb.Services.Organizations;
using Mhasb.Services.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace Mhasb.Wsit.Web.Areas.TaskManagement.Controllers
{
    public class TasksController : Controller
    {
        private readonly ITaskManagerService itService = new TaskManagerService();
        private readonly IProjectService pService = new ProjectService();
        private readonly IEmployeeService eService = new EmployeeService();
        private IUserService uService = new UserService();
        // GET: TaskManagement/Tasks
        public ActionResult Index()
        {
           // var  user = uService.GetSingleUserByEmail(HttpContext.User.Identity.Name);
            var Employess = uService.GetAllUsers();//there will be employees from employee service under compnies of the user Loged in
            ViewBag.Employess = new SelectList(Employess, "Id", "FirstName");
            var model = pService.GetAllProject();
            return View(model);
        }
        [HttpPost]
        public string CreateProject(string ProjectName,int ManagerId,string StartingDate,string FinishingDate) {
         
            /* This Part Will Be Omitted*/
            var emp = eService.GetEmpByUserId(ManagerId);
             /*End*/
            if (emp != null) {
                var newProj = new Project();
                newProj.ProjectName = ProjectName;
                newProj.ManagerId = emp.Id;
                newProj.ProjectDate = DateTime.Now;
                newProj.StartingDate = Convert.ToDateTime(StartingDate);
                newProj.FinishingDate = Convert.ToDateTime(FinishingDate);
                if (pService.CreateProject(newProj))
                {
                    return "Success";
                }
                else {
                    return "Failed";
                }
            }
            
            else
            {
                return "User IS not an employee";
            }

        }
    }
}