using System;
using System.Linq;
using Mhasb.Domain.OrgSettings;
using Mhasb.Wsit.DAL.Operations;
using Mhasb.Wsit.Domain;

namespace Mhasb.Services.OrgSettings
{
   public class TaxSettingService:ITaxSetting
    {
       private readonly CrudOperation<TaxSetting> _crudOperation = new CrudOperation<TaxSetting>();
       public bool AddTaxSetting(TaxSetting taxSetting)
       {
           try
           {
               taxSetting.State = ObjectState.Added;
               _crudOperation.AddOperation(taxSetting);

               return true;
           }
           catch (Exception ex)
           {
               var errMsg = ex.Message;
               return false;
           }
       }

       public bool UpdateTaxSetting(TaxSetting taxSetting)
       {
           try
           {
               var dbObj = _crudOperation.GetSingleObject(taxSetting.Id);
               dbObj.DisplayName = taxSetting.DisplayName;
               dbObj.TaxBasis = taxSetting.TaxBasis;
               dbObj.TaxId = taxSetting.TaxId;
               dbObj.FinalcialPeriod = taxSetting.FinalcialPeriod;
               dbObj.State = ObjectState.Modified;
               
               _crudOperation.UpdateOperation(dbObj);

               return true;
           }
           catch (Exception ex)
           {

               var errMsg = ex.Message;
               return false; 
           }
       }

       public TaxSetting GeTaxSetting(int taxSettingId)
       {
           try
           {
               var taxSettingObj = _crudOperation.GetOperation()
               .Include(t => t.Companies)
               .Filter(t => t.Id == taxSettingId)
               .Get()
               .SingleOrDefault();
               return taxSettingObj;
           }
           catch (Exception ex)
           {

               var errMsg = ex.Message;
               return null;
           }
       }   
    }
}
