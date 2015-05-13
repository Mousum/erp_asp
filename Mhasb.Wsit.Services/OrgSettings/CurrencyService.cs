using Mhasb.Domain.OrgSettings;
using Mhasb.Wsit.DAL.Operations;
using Mhasb.Wsit.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mhasb.Services.OrgSettings
{
    public class CurrencyService : ICurrency
    {
        private readonly CrudOperation<Currency> curRep = new CrudOperation<Currency>();
        public bool AddCurrency(Currency cur)
        {
            try
            {
                cur.State = ObjectState.Added;
                curRep.AddOperation(cur);
                return true;
            }
            catch (Exception ex)
            {
                var rr = ex.Message;
                return false;
            }

        }

        public List<Currency> GetAllCurrency()
        {
            try
            {
                var curObj = curRep.GetOperation()
                                        .Get().ToList();

                return curObj;
            }
            catch (Exception ex)
            {
                var rr = ex.Message;
                return null;
            }
        }
    }
}
