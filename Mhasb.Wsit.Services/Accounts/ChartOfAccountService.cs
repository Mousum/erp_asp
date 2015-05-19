using Mhasb.Domain.Accounts;
using Mhasb.Domain.OrgSettings;
using Mhasb.Wsit.DAL.Operations;
using Mhasb.Wsit.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mhasb.Wsit.CustomModel.Accounts;

namespace Mhasb.Services.Accounts
{
   public class ChartOfAccountService : IChartOfAccountService
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
        public List<ChartOfAccount> GetAllChartOfAccountByCompanyId(int CompanyId)
        {
            try
            {
                var cAObj = _finalCrudOperation.GetOperation()
                                        .Include(c=>c.Companies)
                                        .Filter(c=>c.CompanyId == CompanyId || c.CompanyId == null)
                                        .Get().ToList();

                return cAObj;
            }
            catch (Exception ex)
            {
                var rr = ex.Message;
                return null;
            }
        }
        public List<ChartOfAccount> GetAllChartOfAccountByComIdCostCentre(int CompanyId)
        {
            try
            {
                var cAObj = _finalCrudOperation.GetOperation()
                                        .Include(c => c.Companies)
                                        .Filter(c => c.CompanyId == CompanyId && c.IsCostCenter == false)
                                     //   .Filter(c => c.IsCostCenter == false)
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

       public string GeneratedCode(string pCode,int level)
       {
           var maxVal = _finalCrudOperation.GetOperation()
               .Filter(c => c.Level == level)
               .Get()
               .Where(c => c.ACode.Contains(pCode))
               .Max(r=>r.ACode);
           var maxValue = 1;
           var returnCode = "";
           if (maxVal != null)
           {
               var tt = maxVal.Substring(1, 2);
               maxValue = Convert.ToInt32(tt)+1;
               if (level != 4)
               {
                   returnCode = pCode + maxValue.ToString().PadLeft(2, '0');
               }
               else
               {
                   returnCode = pCode + maxValue.ToString().PadLeft(5, '0');
               }
                
           }
           else
           {
               if (level != 4)
               {
                   returnCode = pCode + maxValue.ToString().PadLeft(2, '0');
               }
               else
               {
                   returnCode = pCode + maxValue.ToString().PadLeft(5, '0');
               }
           }
           return returnCode;
       }

       public List<TreeViewNode> TreeViewList(string pcode, int level)
       {
           var tvList = _finalCrudOperation
               .GetOperation()
               .Filter(ca => ca.Level == level)
               .Get()
               .Select(c => new TreeViewNode
               {
                   id=c.ACode,
                   text = c.AName,
                   classes = c.Level == 4?"file":"folder",
                   hasChildren = c.Level!=4 
               }).ToList();


           return tvList;

           // return new List<TreeViewNode>();
       }
    }
}
