using Mhasb.Domain.Organizations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mhasb.Services.Organizations
{
    public interface IEmployeeService
    {
        bool CreateEmployee(Employee emp);
        bool UpdateEmployee(Employee emp);
        bool DeleteEmployee(int empId);
        Employee GetEmpByUserId(int UserId);
        List<Employee> GetEmpByCompanyId(int CompanyId);
        Employee GetEmpByEmpId(int empId);
        Employee GetEmployeeByUserId(long userId);



    }
}
