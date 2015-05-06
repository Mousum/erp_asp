using Mhasb.Domain.Commons;
using Mhasb.Domain.Organizations;
using Mhasb.Wsit.CustomModel.Organizations;
using Mhasb.Wsit.DAL.Operations;
using Mhasb.Wsit.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mhasb.Services.Organizations
{
    public class EmployeeProfileService : IEmployeeProfile
    {
        private readonly CrudOperation<EmployeeProfile> epRep = new CrudOperation<EmployeeProfile>();
        public bool AddEmployeeProfile(EmployeeProfile ep)
        {
            try
            {
                ep.State = ObjectState.Added;
                epRep.AddOperation(ep);
                return true;
            }
            catch (Exception ex)
            {
                var rr = ex.Message;
                return false;
            }

        }

        public bool UpdateEmployeeProfile(EmployeeProfile ep)
        {
            try
            {
                ep.State = ObjectState.Modified;
                epRep.UpdateOperation(ep);
                return true;
            }
            catch (Exception ex)
            {
                var rr = ex.Message;
                return false;
            }
        }

        public EmployeeProfileCustom GetEmployeeProfile(long userId)
        {
            var empProfile = epRep.GetOperation()
                                   .Include(ep => ep.ContactDetails)
                                   .Filter(ep => ep.Users.Id == userId)
                                   .Get().FirstOrDefault();

            var empProfileObj = new EmployeeProfile {
                                Id = empProfile.Id,
                                ImageLocation = empProfile.ImageLocation,
                                IsActive = empProfile.IsActive,
                                JobTitle = empProfile.JobTitle,
                                Location = empProfile.Location
                            };
            var empProfileCustom = new EmployeeProfileCustom();
                empProfileCustom.employeeProfile = empProfileObj;

                if (empProfile.ContactDetails.Count<1)
                {
                    return new EmployeeProfileCustom(empProfileObj);
                }
            foreach(var oo in empProfile.ContactDetails)
            {
                if (oo.FieldName == "Phone")
                    empProfileCustom.Phone = GetContactObject(oo);
                else if (oo.FieldName == "Fax")
                    empProfileCustom.Phone = GetContactObject(oo);
                else if (oo.FieldName == "Website")
                    empProfileCustom.Phone = GetContactObject(oo);
                else if (oo.FieldName == "Facebook")
                    empProfileCustom.Phone = GetContactObject(oo);
                else if (oo.FieldName == "Twitter")
                    empProfileCustom.Phone = GetContactObject(oo);
                else if (oo.FieldName == "Google")
                    empProfileCustom.Phone = GetContactObject(oo);
                else if (oo.FieldName == "LinkedIn")
                    empProfileCustom.Phone = GetContactObject(oo);
                else if (oo.FieldName == "Skype")
                    empProfileCustom.Phone = GetContactObject(oo);
            }
            return empProfileCustom;
        }

        private ContactDetail GetContactObject (ContactDetail oo)
        {
          return  new ContactDetail
                {
                    Id = oo.Id,
                    EmployeeProfileId = oo.EmployeeProfileId,
                    CompanyProfileId = oo.CompanyProfileId,
                    FieldName = oo.FieldName,
                    FieldUrl = oo.FieldUrl,
                    FieldValueOne = oo.FieldValueOne,
                    FieldValueTwo = oo.FieldValueTwo,
                    FieldValueThree = oo.FieldValueThree
                };
        }
    }
}
