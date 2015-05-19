using System;
using System.Collections.Generic;
using System.Data.Entity;
using Mhasb.Domain.Accounts;
using Mhasb.Domain.Commons;

namespace Mhasb.Wsit.DAL.Data
{
    class DropCreateWSDb : CreateDatabaseIfNotExists<WsDbContext>
    {
        protected override void Seed(WsDbContext context)
        {
            try
            {
                // Insert data into LookUp
                var lookupList = new List<Lookup>();

                lookupList.Add(new Lookup { Id = 1, LookupType = "AccountTax", Key = "1", Value = "Sales Tax on Imports (0%)", Order = 1, Description = "", Quantity = 0 });
                lookupList.Add(new Lookup { Id = 2, LookupType = "AccountTax", Key = "2", Value = "Tax Exempt (0%)", Order = 2, Description = "", Quantity = 0 });
                lookupList.Add(new Lookup { Id = 3, LookupType = "AccountTax", Key = "3", Value = "Tax on Purchases (0%)", Order = 3, Description = "", Quantity = 0 });
                lookupList.Add(new Lookup { Id = 4, LookupType = "AccountTax", Key = "4", Value = "Tax on Sales (0%)", Order = 4, Description = "", Quantity = 0 });

                foreach (var lookup in lookupList)
                {
                    context.Set<Lookup>().Add(lookup);
                }
               
                // Insert Chat of Account First Level Entry
                var coaList = new List<ChartOfAccount>();
                coaList.Add(new ChartOfAccount { Id = 1, ACode = "1", AName = "Assets", Description = "", Level = 1, ShowInDashboard = true });
                coaList.Add(new ChartOfAccount { Id = 2, ACode = "2", AName = "Liabilities", Description = "", Level = 1, ShowInDashboard = true });
                coaList.Add(new ChartOfAccount { Id = 3, ACode = "3", AName = "Equity", Description = "", Level = 1, ShowInDashboard = true });
                coaList.Add(new ChartOfAccount { Id = 4, ACode = "4", AName = "Expenses", Description = "", Level = 1, ShowInDashboard = true });
                coaList.Add(new ChartOfAccount { Id = 5, ACode = "5", AName = "Revenue", Description = "", Level = 1, ShowInDashboard = true });

                foreach (var coa in coaList)
                {
                    context.Set<ChartOfAccount>().Add(coa);
                }

               // base.Seed(context);
                context.ApplyStateChanges();
                context.SaveChanges();
            }
            catch (Exception ee)
            {
                var rr = ee.Message;
            }
           
        }
    }
}
