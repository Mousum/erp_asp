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
    class ChartOfAccountService : IChartOfAccountService
    {
        private readonly CrudOperation<ChartOfAccount> _finalCrudOperation = new CrudOperation<ChartOfAccount>();

        public bool AddChartOfAccount(ChartOfAccount chartOfAccount) 
        {
            try
            {
                chartOfAccount.State = ObjectState.Added;
                _finalCrudOperation.AddOperation(chartOfAccount);
                return true;
            }
            catch (Exception ex)
            {
                var rr = ex.Message;
                return false;
            }
        }
        public bool UpdateChartOfAccount(ChartOfAccount chartOfAccount)
        {
            try
            {
                chartOfAccount.State = ObjectState.Modified;
                _finalCrudOperation.UpdateOperation(chartOfAccount);
                return true;
            }
            catch (Exception ex)
            {
                var rr = ex.Message;
                return false;
            }
        }
        public bool DeleteChartOfAccount(int CaId) 
        {
            try
            {
                _finalCrudOperation.DeleteOperation(CaId);
                return true;
            }
            catch (Exception ex)
            {
                var rr = ex.Message;
                return false;
            }

        }
        public List<ChartOfAccount> GetAllChartOfAccount() 
        {
            try
            {
                var cAObj = _finalCrudOperation.GetOperation()
                                        .Get().ToList();

                return cAObj;
            }
            catch (Exception ex)
            {
                var rr = ex.Message;
                return null;
            }
        }
        public ChartOfAccount GetSingleChartOfAccount(int caId) 
        {
            try
            {
                var curObj = _finalCrudOperation.GetOperation()
                                    .Filter(c => c.Id == caId)
                                    .Get().SingleOrDefault();

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
