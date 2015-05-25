using Mhasb.Domain.Users;
using Mhasb.Wsit.DAL.Operations;
using Mhasb.Wsit.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mhasb.Services.Users
{
   public class SettingsService : ISettingsService
    {
        public readonly CrudOperation<Settings> setRep = new CrudOperation<Settings>();   




        public bool AddSettings(Settings setting)
        {
            try
            {
                setting.State = ObjectState.Added;
                setRep.AddOperation(setting);
                return true;
            }
            catch (Exception ex)
            {
                var rr = ex.Message;
                return false;
            }
        }

        public bool UpdateSettings(Settings setting)
        {
            try {
                var dbObj = setRep.GetSingleObject(setting.Id);
                dbObj.lgcompany = setting.lgcompany;
                dbObj.lgdash = setting.lgdash;
                dbObj.lglast = setting.lglast;
                dbObj.CompanyId = setting.CompanyId;
                dbObj.State = ObjectState.Modified;
                setRep.UpdateOperation(dbObj);
              return true;

            }catch(Exception ex){
                var rr = ex.Message;
                return false;
            }
        }

        public Settings GetAllByUserId(long userId)
        {
            // change SingleOrDefault to FirstOrDefault
            var setObj = setRep.GetOperation()
                                  .Include(st=>st.Users)
                                  .Include(st=>st.Companies)
                                  .Filter(st => st.userId == userId)
                                  .Get().FirstOrDefault();

                return setObj;
            
            

        }

    
    }
}
