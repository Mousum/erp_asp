using Mhasb.Domain.Contacts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mhasb.Services.Contact
{
   public interface IContactDetailsService
    {
       bool CreateContactDetails(ContactDetails contactdetails);
       bool UpdateContactDetails(ContactDetails contactdetails);
       bool DeleteContactDetails(long Id);
       ContactDetails GetContactDetailsById(int id);
       List<ContactDetails> GetAllContactDetails();
            

    }
}
