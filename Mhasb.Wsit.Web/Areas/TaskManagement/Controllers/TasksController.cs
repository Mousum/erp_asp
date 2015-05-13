using Mhasb.Domain.Organizations;
using Mhasb.Services.Organizations;
using Mhasb.Services.Users;
using Mhasb.Wsit.Domain;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using PagedList.Mvc;


namespace Mhasb.Wsit.Web.Areas.TaskManagement.Controllers
{
    public class TasksController : Controller
    {
        private readonly ITaskManagerService itService = new TaskManagerService();
        private readonly IProjectService pService = new ProjectService();
        private readonly IEmployeeService eService = new EmployeeService();
        private ISettingsService setService = new SettingsService();
        private IUserService uService = new UserService();


        // GET: TaskManagement/Tasks
        public ActionResult Index()
        {
            var user = uService.GetSingleUserByEmail(HttpContext.User.Identity.Name);
            var AccSet = setService.GetAllByUserId(user.Id);
            try
            {
                var Emp = eService.GetEmpByCompanyId(AccSet.Companies.Id).Distinct().Select(u => new { Id = u.Id, Name = u.Users.FirstName + " " + u.Users.LastName });//there will be employees from employee service under compnies of the user Loged in
                ViewBag.Employess = new SelectList(Emp, "Id", "Name");
            }
            catch (Exception ex)
            {
                var msg = ex.Message;

            }

            //var model = pService.GetAllProject();
            return View("Index1");
        }
        //method for paging and search option
        public ActionResult List(string currentFilter, string searchString, int? page)
        {
            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            ViewBag.CurrentFilter = searchString;
            List<Project> projects = pService.GetAllProject();
            if (!String.IsNullOrEmpty(searchString))
            {
                projects = projects.Where(s => s.ProjectName.Contains(searchString)).ToList();
            }

            int pageSize = 3;
            int pageNumber = (page ?? 1);
            return PartialView(projects.ToPagedList(pageNumber, pageSize));

        }








        [HttpPost]
        public string CreateProject(string ProjectName, int ManagerId, string StartingDate, string FinishingDate)
        {
            var user = uService.GetSingleUserByEmail(HttpContext.User.Identity.Name);
            var AccSet = setService.GetAllByUserId(user.Id);
            var newProj = new Project();
            newProj.ProjectName = ProjectName;
            newProj.ManagerId = ManagerId;
            newProj.CompanyId = AccSet.Companies.Id;
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

            TaskManager newTask = new TaskManager();
            newTask.TaskTo = TaskTo;
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


        [HttpPost]
        public PartialViewResult UpdateProject(int id)
        {
            var user = uService.GetSingleUserByEmail(HttpContext.User.Identity.Name);
            var AccSet = setService.GetAllByUserId(user.Id);
            try
            {
                var Emp = eService.GetEmpByCompanyId(AccSet.Companies.Id).Distinct().Select(u => new { Id = u.Id, Name = u.Users.FirstName + " " + u.Users.LastName });//there will be employees from employee service under compnies of the user Loged in
                ViewBag.Employess = new SelectList(Emp, "Id", "Name");
            }
            catch (Exception ex)
            {
                var msg = ex.Message;
              

            }
            var model = pService.GetSingleProject(id);
            return PartialView("_updateProject", model);
        }
        [HttpPost]
        public string EditProject(int id, string ProjectName, int ManagerId, string StartingDate, string FinishingDate)
        {
            string[] dateString = StartingDate.Split('/');
            DateTime start_date = Convert.ToDateTime(dateString[1] + "/" + dateString[0] + "/" + dateString[2]);
            string[] dateString1 = FinishingDate.Split('/');
            DateTime end_date = Convert.ToDateTime(dateString1[1] + "/" + dateString1[0] + "/" + dateString1[2]);
            var proj = new Project();
            proj.Id = id;
            proj.ProjectName = ProjectName;
            proj.ManagerId = ManagerId;

            proj.StartingDate = start_date;
            proj.FinishingDate = end_date;

            if (pService.UpdateProject(proj))
            {
                return "Success";
            }
            else
            {
                return "Failed";
            }
        }


        [HttpPost]
        public PartialViewResult UpdateTask(int id)
        {
            var user = uService.GetSingleUserByEmail(HttpContext.User.Identity.Name);
            var AccSet = setService.GetAllByUserId(user.Id);
            try
            {
                var Emp = eService.GetEmpByCompanyId(AccSet.Companies.Id).Distinct().Select(u => new { Id = u.Id, Name = u.Users.FirstName + " " + u.Users.LastName });//there will be employees from employee service under compnies of the user Loged in
                ViewBag.Employess = new SelectList(Emp, "Id", "Name");
            }
            catch (Exception ex)
            {
                var msg = ex.Message;

            }
            var listSt = Enum.GetValues(typeof(EnumStatus))
                                    .Cast<EnumStatus>()
                                    .Select(v => new { Id = Convert.ToInt32(v), Name = v.ToString() })
                                    .ToList();

            ViewBag.status = new SelectList(listSt, "Id", "Name");
            var model = itService.GetSingleTask(id);
            return PartialView("_updateTask", model);
        }
        [HttpPost]
        public string EditTask(int id, int TaskTo, string TaskTitle, string StartingDate, string FinishingDate,int Status)
        {
            EnumStatus enumDisplayStatus = (EnumStatus)Status;
            string StatusValue = enumDisplayStatus.ToString();
            
            
            string[] date1 = StartingDate.Split(' ');
            string[] date2 = FinishingDate.Split(' ');
            string[] dateString = date1[0].Split('/');
            DateTime start_date = Convert.ToDateTime(dateString[1] + "/" + dateString[0] + "/" + dateString[2]);
            
            string[] dateString1 = date2[0].Split('/');
            DateTime end_date = Convert.ToDateTime(dateString1[1] + "/" + dateString1[0] + "/" + dateString1[2]);
           
            
            var task = new TaskManager();
            task.Id = id;
            task.TaskTo = TaskTo;
            task.Tite = TaskTitle;
            task.StartingDate = start_date;
            task.FinishingDate = end_date;
            task.Status = (EnumStatus)Enum.Parse(typeof(EnumStatus), StatusValue, true);
           // task.Status = EnumStatus.stringValue;
            if (itService.UpdateTask(task))
            {
                return "Success";
            }
            else
            {
                return "Failed";
            }
        }




    }
}