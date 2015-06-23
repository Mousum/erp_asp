using Mhasb.Domain.Contacts;
using Mhasb.Wsit.DAL.Operations;
using Mhasb.Wsit.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mhasb.Services.Contact
{
    public class FinancialDetailsService : IFinancialDetailsService
    {
        private readonly CrudOperation<FinancialDetails> _rep = new CrudOperation<FinancialDetails>();
        public bool CreateFinancialDetails(FinancialDetails financialdetails)
        {
            try {
                 financialdetails.State=ObjectState.Added;
                _rep.AddOperation(financialdetails);
                return true;
            }catch(Exception ex){
                var msg = ex.Message;
                return false;
            }
        }

        public bool UpdateFinancialDetails(FinancialDetails financialdetails)
        {
            try
            {
                financialdetails.State = ObjectState.Modified;
                _rep.AddOperation(financialdetails);
                return true;
            }
            catch (Exception ex)
            {
                var msg = ex.Message;
                return false;
            }
        }

        public bool DeleteFinancialDetails(long Id)
        {
            try
            {
                _rep.DeleteOperation(Id);
                return true;
            }
            catch (Exception ex)
            {
                var msg = ex.Message;
                return false;
            }
        }

        public FinancialDetails GetFinancialDetailsById(long Id)
        {
            try
            {

                var _obj = _rep.GetOperation()
                    .Filter(i=>i.Id==Id)
                    .Get().SingleOrDefault();
                return _obj;
            }
            catch (Exception ex)
            {
                var msg = ex.Message;
                return null;
            }
        }

        public List<FinancialDetails> GetAllFinancialDetails()
        {
            try
            {
                var _obj = _rep.GetOperation()
                    .Get().ToList();
                return _obj;
            }
            catch (Exception ex)
            {
                var msg = ex.Message;
                return null;
            }
        }
    }
}
