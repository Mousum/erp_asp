using Mhasb.Domain.Accounts;
using Mhasb.Domain.OrgSettings;
using Mhasb.Wsit.DAL.Operations;
using Mhasb.Wsit.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
                                        .Filter(c=>c.CompanyId == CompanyId || c.CompanyId.HasValue ==false)
                                        .Get()
                                        .OrderBy(c=>c.ACode)
                                        .ToList();
                return cAObj;
            }
            catch (Exception ex)
            {
                var rr = ex.Message;
                return null;
            }
        }


        public List<ChartOfAccount> GetAllChartOfAccountByCompanyIdForSecondLevel(int CompanyId)
        {
            try
            {
                var cAObj = _finalCrudOperation.GetOperation()
                                        .Include(c => c.Companies)
                                        .Filter(c => c.CompanyId == CompanyId || c.CompanyId.HasValue == false && c.Level<=2)
                                        .Get()
                                        .OrderBy(c => c.ACode)
                                        .ToList();
                return cAObj;
            }
            catch (Exception ex)
            {
                var rr = ex.Message;
                return null;
            }
        }

        public List<ChartOfAccount> CodeWiseGetAllChartOfAccountByCompanyIdForLastLevel(int CompanyId,string code)
        {
            try
            {
                var cAObj = _finalCrudOperation.GetOperation()
                                        .Include(c => c.Companies)
                                        .Filter(c => c.CompanyId == CompanyId || c.CompanyId.HasValue == false && c.Level > 2 && c.ACode.StartsWith(code))
                                        .Get()
                                        .OrderBy(c => c.ACode)
                                        .ToList();
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
                                        .Filter(c => c.CompanyId == CompanyId && c.IsCostCenter == false &&c.Level==4)
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
               .Get()
               .Where(
                   c =>
                       ("0" == pcode || c.Level == level+1) &&
                       ("0" == pcode || c.ACode.StartsWith(pcode, StringComparison.OrdinalIgnoreCase)) &&
                       ("0" != pcode || c.Level == 1)
                       )
               .Select(c => new TreeViewNode
               {
                   id = c.ACode,
                   text = c.AName,
                   classes = c.Level == 4 ? "file" : "folder",
                   hasChildren = c.Level != 4
               }).ToList();


           return tvList;

       }

       public List<ChartOfAccount> GetBaseAccountTypes()
       {
           return null;
       }

       public bool AddBaseAccountTypes()
       {
           // Insert Chat of Account First Level Entry
           var coaList = new List<ChartOfAccount>();
           coaList.Add(new ChartOfAccount { Id = 1, ACode = "1", AName = "Assets", Description = "", Level = 1, ShowInDashboard = true });
           coaList.Add(new ChartOfAccount { Id = 2, ACode = "2", AName = "Liabilities", Description = "", Level = 1, ShowInDashboard = true });
           coaList.Add(new ChartOfAccount { Id = 3, ACode = "3", AName = "Equity", Description = "", Level = 1, ShowInDashboard = true });
           coaList.Add(new ChartOfAccount { Id = 4, ACode = "4", AName = "Expenses", Description = "", Level = 1, ShowInDashboard = true });
           coaList.Add(new ChartOfAccount { Id = 5, ACode = "5", AName = "Revenue", Description = "", Level = 1, ShowInDashboard = true });

           try
           {
               foreach (var coa in coaList)
               {
                   coa.State = ObjectState.Added;
                   _finalCrudOperation.AddOperation(coa);
                   coa.State = ObjectState.Unchanged;
               }
               return true;
           }
           catch (Exception ex)
           {
               var rr = ex.Message;
               return false;
           }

          
       }
       public List<ChartOfAccount> GetDefaultChartOfAccounts()  //This function Will Return Only The Default COAs
       {

           try
           {
               var cAObj = _finalCrudOperation.GetOperation()
                                       .Filter(c=>c.CompanyId.HasValue == false)
                                       .Get()
                                       .OrderBy(c => c.ACode)
                                       .ToList();
               return cAObj;
           }
           catch (Exception ex)
           {
               var rr = ex.Message;
               return null;
           }
       }
    }
}
