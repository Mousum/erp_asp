using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mhasb.Domain.Accounts;

namespace Mhasb.Wsit.DAL.Data
{
    class DropCreateWSDb : CreateDatabaseIfNotExists<WsDbContext>
    {
        protected override void Seed(WsDbContext context)
        {
            // Insert Chat of Account First Level Entry

            //context.Set<ChartOfAccount>().Add();
            //context.SaveChanges();

        }
    }
}
