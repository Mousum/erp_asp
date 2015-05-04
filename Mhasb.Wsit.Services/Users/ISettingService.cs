using Mhasb.Domain.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mhasb.Services.Users
{
    public interface ISettingsService
    {
         bool AddSettings(Settings setting);
         bool UpdateSettings(Settings setting);
         bool GetAllByUserId(long userId);


    }
}
