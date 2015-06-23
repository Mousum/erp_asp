using Mhasb.Domain.Contacts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mhasb.Services.Contact
{
    interface ITelePhoneService
    {
        bool CreateTelePhone(TelePhone telephone);
        bool UpdateTelePhone(TelePhone telephone);
        bool DeleteTelePhone(long Id);
        TelePhone GetTelePhoneById(int Id);
        List<TelePhone> GetAllTelePhone();
    }
}
