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
    public class CompanyProfileService : ICompanyProfile
    {



        private readonly CrudOperation<CompanyProfile> cpRep = new CrudOperation<CompanyProfile>();
        public bool AddCompanyProfile(CompanyProfile cp)
        {
            try
            {
                cp.State = ObjectState.Added;

                cpRep.AddOperation(cp);
                return true;
            }
            catch (Exception ex)
            {
                var rr = ex.Message;
                return false;
            }

        }

        public bool UpdateCompanyProfile(CompanyProfile cp)
        {
            try
            {
                cp.State = ObjectState.Modified;
                cpRep.UpdateOperation(cp);
                return true;
            }
            catch (Exception ex)
            {
                var rr = ex.Message;
                return false;
            }
        }

        public CompanyProfileCustom GetCompanyProfile(long companyId)
        {
            try
            {
                var comProfile = cpRep.GetOperation()
                                   .Include(cp => cp.ContactDetails)
                                   .Filter(cp => cp.Companies.Id== companyId)
                                   .Get().FirstOrDefault();
                var comProfileObj = new CompanyProfile
                {
                    Id = comProfile.Id,
                    BusinessName=comProfile.BusinessName,
                    Vision=comProfile.Vision,
                    TurnOver=comProfile.TurnOver,
                    Objectives=comProfile.Objectives,
                    Mission=comProfile.Mission,
                    Experties=comProfile.Experties,
                    Activities=comProfile.Activities,
                    Markets=comProfile.Markets,
                    PreviousWork=comProfile.PreviousWork,
                    Address=comProfile.Address,
                    Location=comProfile.Location,
                    Email=comProfile.Email,
                    ImageLocation=comProfile.ImageLocation
                    
                };
                var comProfileCustom = new CompanyProfileCustom();
                comProfileCustom.companyProfile = comProfileObj;

                if (comProfile.ContactDetails.Count < 1)
                {
                    return null;
                }
                foreach (var oo in comProfile.ContactDetails)
                {
                    if (oo.FieldName == "Phone")
                        comProfileCustom.Phone = GetContactObject(oo);
                    else if (oo.FieldName == "Fax")
                        comProfileCustom.Fax = GetContactObject(oo);
                    else if (oo.FieldName == "Website")
                        comProfileCustom.Website = GetContactObject(oo);
                    else if (oo.FieldName == "Facebook")
                        comProfileCustom.Facebook = GetContactObject(oo);
                    else if (oo.FieldName == "Twitter")
                        comProfileCustom.Twitter = GetContactObject(oo);
                    else if (oo.FieldName == "Google")
                        comProfileCustom.Google = GetContactObject(oo);
                    else if (oo.FieldName == "LinkedIn")
                        comProfileCustom.LinkedIn = GetContactObject(oo);
                    else if (oo.FieldName == "Skype")
                        comProfileCustom.Skype = GetContactObject(oo);
                }
                return comProfileCustom;
            }
            catch (Exception ex)
            {
                return null;
            }

        }

        private ContactDetail GetContactObject(ContactDetail oo)
        {
            return new ContactDetail
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
