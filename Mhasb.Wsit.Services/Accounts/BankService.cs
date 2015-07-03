using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mhasb.Domain.Accounts;
using Mhasb.Wsit.DAL.Operations;
using Mhasb.Wsit.Domain;

namespace Mhasb.Services.Accounts
{
    public class BankService :IBankService
    {
        private readonly CrudOperation<Bank> _crudOperation = new CrudOperation<Bank>();
        public bool AddBank(Bank bank)
        {
            try
            {
                bank.State = ObjectState.Added;
                _crudOperation.AddOperation(bank);
                return true;

            }
            catch (Exception ex)
            {
                var rr = ex.Message;
                return false;
            }
        }

        public bool UpdateBank(Bank bank)
        {
            try
            {
                var dbObj = _crudOperation.GetSingleObject(bank.Id);
                dbObj.AccountName = bank.AccountName;
                dbObj.AccountNumber = bank.AccountNumber;
                dbObj.BankName = bank.BankName;
                dbObj.CurrencyId = bank.CurrencyId;
                dbObj.State = ObjectState.Modified;
                _crudOperation.UpdateOperation(dbObj);

                return true;

            }
            catch (Exception ex)
            {
                var rr = ex.Message;
                return false;
            }
        }

        public bool DeleteBank(int id)
        {
            try
            {
                _crudOperation.DeleteOperation(id);
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
