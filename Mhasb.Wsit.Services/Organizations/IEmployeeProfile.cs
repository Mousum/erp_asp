using Mhasb.Domain.Organizations;
using Mhasb.Wsit.CustomModel.Organizations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mhasb.Services.Organizations
{
    public interface IEmployeeProfile
    {
        bool AddEmployeeProfile( EmployeeProfile employee);
        bool UpdateEmployeeProfile(EmployeeProfile ep);
        EmployeeProfileCustom GetEmployeeProfile(long userId);
    }
}
