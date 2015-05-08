using Mhasb.Domain.Organizations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mhasb.Services.Organizations
{
    public interface ITaskManagerService
    {
        bool CreateTask(TaskManager task);
        bool UpdateTask(TaskManager task);
        bool DeleteTask(long Id);

        TaskManager GetSingleTask(long Id);

        TaskManager GetTaskByEmpId(long empId);

        TaskManager GetTaskByProjectId(long projectId);
        List<TaskManager> GetAllTask();
    }
}
