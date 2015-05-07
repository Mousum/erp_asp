﻿using Mhasb.Domain.Commons;
using Mhasb.Wsit.DAL.Operations;
using Mhasb.Wsit.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mhasb.Services.Commons
{
    public class ContactDetailService : IContactDetail
    {
        private readonly CrudOperation<ContactDetail> contactRep = new CrudOperation<ContactDetail>();
        public bool AddContactDetail(ContactDetail contactDetail)
        {
            try
            {
                contactDetail.State = ObjectState.Added;
                contactRep.AddOperation(contactDetail);
                contactDetail.State = ObjectState.Unchanged;
                return true;
            }
            catch (Exception ex)
            {
                var rr = ex.Message;
                return false;
            }

        }

        public bool UpdateContactDetail(ContactDetail contactDetail)
        {
            try
            {
                contactDetail.State = ObjectState.Modified;
                contactRep.UpdateOperation(contactDetail);
                contactDetail.State = ObjectState.Unchanged;
                return true;
            }
            catch (Exception ex)
            {
                var rr = ex.Message;
                return false;
            }

        }

        public ContactDetail GetSingleContactDetailByEmployeeIdAndFieldName(long id,string fieldName){
            try
            {
                //company.State = ObjectState.Unchanged;
                var conObj = contactRep.GetOperation()
                                        .Filter(c => c.EmployeeProfileId == id && c.FieldName==fieldName)
                                        .Get().SingleOrDefault();

                //companyRep.GetSingleObject(companyId);
                return conObj;
            }
            catch (Exception ex)
            {
                var rr = ex.Message;
                return null;
            }
        }

    }
}
