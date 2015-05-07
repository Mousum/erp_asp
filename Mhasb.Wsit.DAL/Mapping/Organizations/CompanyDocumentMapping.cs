using Mhasb.Domain.Organizations;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mhasb.DAL.Mapping.Organizations
{
    public class CompanyDocumentMapping : EntityTypeConfiguration<CompanyDocument>
    {
        public CompanyDocumentMapping()
        {
            // Key
            this.HasKey(cd => cd.Id);



         }
    }
}
