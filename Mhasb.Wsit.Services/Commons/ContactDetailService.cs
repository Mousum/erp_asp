using Mhasb.Domain.Commons;
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
                return true;
            }
            catch (Exception ex)
            {
                var rr = ex.Message;
                return false;
            }

        }
    }
}
