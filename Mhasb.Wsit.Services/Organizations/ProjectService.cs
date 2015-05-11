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
    public class ProjectService : IProjectService
    {
        private readonly CrudOperation<Project> proRep = new CrudOperation<Project>();
        public bool CreateProject(Project project)
        {
            try
            {
                project.State = ObjectState.Added;
                proRep.AddOperation(project);
                return true;
            }
            catch (Exception ex)
            {
                var msg = ex.Message;
                return false;
            }
        }

        public bool UpdateProject(Project project)
        {
            try
            {
                var dbObj = proRep.GetSingleObject(project.Id);

                dbObj.StartingDate = project.StartingDate;
                dbObj.FinishingDate = project.FinishingDate;
                dbObj.ManagerId = project.ManagerId;
                dbObj.ProjectName = project.ProjectName;
                dbObj.State = ObjectState.Modified;
                proRep.UpdateOperation(dbObj);
                return true;
            }
            catch (Exception ex)
            {
                var msg = ex.Message;
                return false;
            }
        }

        public bool DeleteProject(long Id)
        {
            try
            {
                proRep.DeleteOperation(Id);
                return true;
            }
            catch (Exception ex)
            {
                var msg = ex.Message;
                return false;
            }
            throw new NotImplementedException();
        }

        public Project GetSingleProject(long Id)
        {
            try { 
                var proObj=proRep.GetOperation()
                    .Include(c=>c.Employees)
                    .Filter(c=>c.Id==Id)
                    .Get().SingleOrDefault();
                return proObj;
            }
            catch(Exception ex) {
                var msg = ex.Message;
                return null;
            }
        }

        public List<Project> GetAllProject()
        {
            try {
                var proObj = proRep.GetOperation()
                    .Include(c => c.Employees)
                    .Include(t=>t.TaskManagers)
                    .Get().ToList();
                return proObj;
            }catch(Exception ex){
                var msg = ex.Message;
                return null;
            }
        }

        public List<Project> GetAllProjectByManagerId(long ManagerId)
        {
            try
            {
                var proObj = proRep.GetOperation()
                    .Include(c => c.Employees)
                    .Filter(c => c.ManagerId == ManagerId)
                    .Get().ToList();
                return proObj;
            }
            catch (Exception ex)
            {
                var msg = ex.Message;
                return null;
            }
        }
    }
}
