using Mhasb.Domain.Organizations;
using Mhasb.Wsit.DAL.Operations;
using Mhasb.Wsit.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mhasb.Services.Organizations
{
    public class TaskManagerService : ITaskManagerService
    {
        private readonly CrudOperation<TaskManager> taskRep= new CrudOperation<TaskManager>();

        public bool CreateTask(Domain.Organizations.TaskManager task)
        {
            try {
                task.State = ObjectState.Added;
                taskRep.AddOperation(task);
                return true;
            }
            catch(Exception ex) {
                var msg = ex.Message;
                return false;
            }
        }

        public bool UpdateTask(Domain.Organizations.TaskManager task)
        {
            try {
                task.State = ObjectState.Modified;
                taskRep.UpdateOperation(task);
                return true;
            }catch(Exception ex){
                var msg = ex.Message;
                return false;
            }
        }

        public bool DeleteTask(long Id)
        {
            try {
                taskRep.DeleteOperation(Id);
                return true;
             
            }catch(Exception ex){
                var msg = ex.Message;
                return false;
            }
        }

        public TaskManager GetSingleTask(long Id)
        {
            try {
                var taskObj = taskRep.GetOperation()
                        .Include(c => c.Employees)
                        .Include(c => c.Projects)
                        .Filter(c => c.Id == Id)
                        .Get().SingleOrDefault();
                return taskObj;
            }catch(Exception ex){
                var msg = ex.Message;
                return null;
             
            }
            
        }

        public TaskManager GetTaskByEmpId(long empId)
        {
            try {
                var taskObj = taskRep.GetOperation()
                    .Include(c => c.Employees)
                    .Include(c => c.Projects)
                    .Filter(c => c.TaskTo == empId)
                    .Get().SingleOrDefault();
                return taskObj;
            }catch(Exception ex){
                var msg = ex.Message;
                return null;
            }
        }

        public List<TaskManager> GetTaskByProjectId(long projectId)
        {
            try
            {
                var taskObj = taskRep.GetOperation()
                    .Include(c => c.Employees)
                    .Include(c => c.Projects)
                    .Filter(c => c.ProjectId == projectId)
                    .Get().ToList();
                return taskObj;
            }
            catch (Exception ex)
            {
                var msg = ex.Message;
                return null;
            }
        }

        public List<TaskManager> GetAllTask()
        {

            try
            {
                var taskObj = taskRep.GetOperation()
                    .Include(c => c.Employees)
                    .Include(c => c.Projects)
                    .Get().ToList();
                return taskObj;
            }
            catch (Exception ex)
            {
                var msg = ex.Message;
                return null;
            }
        }
    }
}
