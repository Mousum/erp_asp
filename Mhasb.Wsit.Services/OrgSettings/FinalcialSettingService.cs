using System;
using System.Linq;
using Mhasb.Domain.OrgSettings;
using Mhasb.Wsit.DAL.Operations;
using Mhasb.Wsit.Domain;

namespace Mhasb.Services.OrgSettings
{
   public class FinalcialSettingService:IFinalcialSetting
    {
       private readonly CrudOperation<FinalcialSetting> _finalCrudOperation = new CrudOperation<FinalcialSetting>();
        public bool AddFinalcialSetting(FinalcialSetting finalcialSetting)
        {

            try
            {
                finalcialSetting.State = ObjectState.Added;
                _finalCrudOperation.AddOperation(finalcialSetting);

                return true;
            }
            catch (Exception ex)
            {

                var errorMsg = ex.Message;
                return false;
            }
        }

        public bool UpdateFinalcialSetting(FinalcialSetting finalcialSetting)
        {
            try
            {
                var dbObj = _finalCrudOperation
                    .GetSingleObject(finalcialSetting.Id);
                dbObj.FoundedYear = finalcialSetting.FoundedYear;
                dbObj.PeriodId = finalcialSetting.PeriodId;
                dbObj.CurrencyId = finalcialSetting.CurrencyId;
                dbObj.SharesCurrencyId = finalcialSetting.SharesCurrencyId;
                dbObj.StartingDate = finalcialSetting.StartingDate;
                dbObj.EndingDate = finalcialSetting.EndingDate;
                dbObj.PeriodLockDate = finalcialSetting.PeriodLockDate;
                dbObj.YearLockDate = finalcialSetting.YearLockDate;
                dbObj.SharePrice = finalcialSetting.SharePrice;

                dbObj.State = ObjectState.Modified;
                _finalCrudOperation.UpdateOperation(dbObj);


                return true;
            }
            catch (Exception ex)
            {
                var errorMsg = ex.Message;
                return false;
            }
        }

        public FinalcialSetting GetFinalcialSetting(int finalcialSettingId)
        {
            try
            {
                var fSetttingObj = _finalCrudOperation.GetOperation()
                    .Include(fs => fs.Companies)
                    .Include(fs => fs.Currencies)
                    .Include(fs => fs.FinalcialPeriods)
                    .Filter(fs => fs.Id == finalcialSettingId)
                    .Get()
                    .SingleOrDefault();


                return fSetttingObj;
            }
            catch (Exception ex)
            {
                var errorMsg = ex.Message;

                return null;
            }
        }
    }
}
