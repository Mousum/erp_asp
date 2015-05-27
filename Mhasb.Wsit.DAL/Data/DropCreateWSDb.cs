using System;
using System.Collections.Generic;
using System.Data.Entity;
using Mhasb.Domain.Accounts;
using Mhasb.Domain.Commons;

namespace Mhasb.Wsit.DAL.Data
{
   class DropCreateWSDb : CreateDatabaseIfNotExists<WsDbContext>
    {
        public DropCreateWSDb()
        {
            
        }
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
               
               



                //base.Seed(context);
                context.SaveChanges();
            }
            catch (Exception ee)
            {
                var rr = ee.Message;
            }
           
        }
    }
}
