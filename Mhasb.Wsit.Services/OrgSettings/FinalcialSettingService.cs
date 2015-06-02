using System;
using System.Linq;
using Mhasb.Domain.OrgSettings;
using Mhasb.Wsit.DAL.Operations;
using Mhasb.Wsit.Domain;

namespace Mhasb.Services.OrgSettings
{
   public class FinalcialSettingService:IFinalcialSetting
    {
       private readonly CrudOperation<FinancialSetting> _finalCrudOperation = new CrudOperation<FinancialSetting>();
        public bool AddFinalcialSetting(FinancialSetting finalcialSetting)
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

        public bool UpdateFinalcialSetting(FinancialSetting finalcialSetting)
        {
            try
            {
                var dbObj = _finalCrudOperation
                    .GetSingleObject(finalcialSetting.Id);
                dbObj.FoundedYear = finalcialSetting.FoundedYear;
                dbObj.FinalcialPeriod = finalcialSetting.FinalcialPeriod;
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

        public FinancialSetting GetFinalcialSetting(int finalcialSettingId)
        {
            try
            {
                var fSetttingObj = _finalCrudOperation.GetOperation()
                    .Include(fs => fs.Companies)
                    .Include(fs => fs.Currencies)
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

        public FinancialSetting GetCurrentFinalcialSettingByComapny(int CompanyId)
        {
            try
            {
                var fSetttingObj = _finalCrudOperation.GetOperation()
                    .Include(fs => fs.Companies)
                    .Include(fs => fs.Currencies)
                    //.Filter(fs => fs.CompanyId == CompanyId && DateTime.Compare(fs.StartingDate, DateTime.Now) <= 0 && DateTime.Compare(fs.EndingDate, DateTime.Now) >= 0)
                    .Filter(fs => fs.CompanyId == CompanyId)
                    .Get()
                    .FirstOrDefault();


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
