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
        public string CreateProject(string ProjectName, int ManagerId, string StartingDate, string FinishingDate)
        {

            /* This Part Will Be Omitted*/
            var emp = eService.GetEmpByUserId(ManagerId);
            /*End*/
            if (emp != null)//for now
            {//for now
                var newProj = new Project();
                newProj.ProjectName = ProjectName;
                newProj.ManagerId = emp.Id;//direct "ManagerId"
                newProj.CompanyId = 1;
                newProj.ProjectDate = DateTime.Now;
                newProj.StartingDate = Convert.ToDateTime(StartingDate);
                newProj.FinishingDate = Convert.ToDateTime(FinishingDate);
                if (pService.CreateProject(newProj))
                {
                    return "Success";
                }
                else
                {
                    return "Failed";
                }
            }//fornow

            else//fornow
            {
                return "User IS not an employee";
            }

        }
        [HttpPost]
        public PartialViewResult TaskList(long id)
        {
            var model = itService.GetTaskByProjectId(id);
            ViewBag.ProjectId = id;
            return PartialView("_taskList", model);
        }
        [HttpPost]
        public string CreateTask(int TaskTo, long ProjectId, string TaskTitle, string StartingDate, string FinishingDate)
        {
               /* This Part Will Be Omitted*/
            var emp = eService.GetEmpByUserId(TaskTo);
            /*End*/
            if (emp != null)//for now
            {//for now
                TaskManager newTask = new TaskManager();
                newTask.TaskTo = emp.Id;//Direct "TaskTo"
                newTask.ProjectId = ProjectId;
                newTask.Tite = TaskTitle;
                newTask.TaskDate = DateTime.Now;
                newTask.StartingDate = Convert.ToDateTime(StartingDate);
                newTask.FinishingDate = Convert.ToDateTime(FinishingDate);
                newTask.Status = Domain.EnumStatus.Ongoing;

                if (itService.CreateTask(newTask))
                {
                    return "Success";
                }
                else
                {
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