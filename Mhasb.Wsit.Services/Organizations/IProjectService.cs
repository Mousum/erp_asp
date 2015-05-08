using Mhasb.Domain.Organizations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mhasb.Services.Organizations
{
    public interface IProjectService
    {
        bool CreateProject(Project project);
        bool UpdateProject(Project project);

        bool DeleteProject(long Id);

        Project GetSingleProject(long Id);

        List<Project> GetAllProject();

        List<Project> GetAllProjectByManagerId(long ManagerId);
    }
}
