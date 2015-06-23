using Mhasb.Domain.Contacts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mhasb.Services.Contact
{
    public interface IContactInformationService
    {
         bool CreateContInfo(ContactInformation ContactInformation);
         bool UpdateContInfo(ContactInformation ContactInformation);
         bool DeleteContInfo(long Id);
         List<ContactInformation> GetAllContactInformation();
         ContactInformation GetAllContactInformationById(long Id);
    }
}
