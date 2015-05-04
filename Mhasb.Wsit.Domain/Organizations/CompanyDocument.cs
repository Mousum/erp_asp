using Mhasb.Wsit.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mhasb.Domain.Organizations
{
    public class CompanyDocument : IObjectStateLong
    {
        public long Id { get; set; }

        public int companyId { get; set; }
        public string documentLocation { get; set; }
    }
}
