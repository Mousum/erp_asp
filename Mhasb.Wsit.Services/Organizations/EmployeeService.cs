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
  public  class EmployeeService :IEmployeeService
    {
        private readonly CrudOperation<Employee> empRep = new CrudOperation<Employee>();

        public bool CreateEmployee(Employee emp)
        {
            try
            {
                emp.State = ObjectState.Added;
                empRep.AddOperation(emp);
                return true;
            }
            catch (Exception ex)
            {
                var msg = ex.Message;
                return false;
            }
            

        }

        public bool UpdateEmployee(Employee emp)
        {
            try
            {
                emp.State = ObjectState.Modified;
                empRep.UpdateOperation(emp);
                return true;
            }
            catch (Exception ex)
            {
                var msg = ex.Message;
                return false;
            }
        }

        public bool DeleteEmployee(int empId)
        {
            try { 
             empRep.DeleteOperation(empId);
                return true;

            }catch(Exception ex){
                var msg = ex.Message;
                return false;
            }
        }


        public Employee GetEmpByUserId(int UserId)
        {
            try {
                var empObj = empRep.GetOperation()
                    .Include(c => c.Users)
                    .Include(c => c.Companies)
                    .Include(d=>d.Designations)
                    .Filter(c => c.UserId==UserId)
                    .Get().SingleOrDefault();
                return empObj;
            }catch(Exception ex){
                var msg = ex.Message;
                return null;
            }
        }

        public Employee GetEmpByEmpId(int empId)
        {
            try
            {
                var empObj = empRep.GetOperation()
                    .Include(c => c.Users)
                    .Include(c => c.Companies)
                    .Include(d => d.Designations)
                    .Filter(c => c.Id == empId)
                    .Get().SingleOrDefault();
                return empObj;
            }
            catch (Exception ex)
            {
                var msg = ex.Message;
                return null;
            }
        }


        public List<Employee> GetEmpByCompanyId(int CompanyId)
        {
            try {
                var empObj = empRep.GetOperation()
                    .Include(c => c.Users)
                    .Include(c => c.Companies)
                    .Include(d => d.Designations)
                    .Filter(c => c.CompanyId== CompanyId)
                    .Get().ToList();
                return empObj;
                   
            }catch(Exception ex){
                var msg = ex.Message;
                return null; 
            }
        }

        public Employee GetEmployeeByUserId(long userId)
        {
            try
            {
                var empObj = empRep.GetOperation()
                    .Include(c => c.Users)
                    .Include(c => c.Companies)
                    .Include(d => d.Designations)
                    .Filter(c => c.UserId == userId)
                    .Get().SingleOrDefault();
                return empObj;
            }
            catch (Exception ex)
            {
                var msg = ex.Message;
                return null;
            }
        }

        public Employee GetEmployeeByUserIdAndCompanyId(long userId,int companyId)
        {
            try
            {
                var empObj = empRep.GetOperation()
                    .Include(c => c.Users)
                    .Include(c => c.Companies)
                    .Include(d => d.Designations)
                    .Filter(c => c.UserId == userId && c.CompanyId==companyId)
                    .Get().SingleOrDefault();
                return empObj;
            }
            catch (Exception ex)
            {
                var msg = ex.Message;
                return null;
            }
        }

    }
}
