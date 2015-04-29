using Mhasb.Domain.Commons;
using Mhasb.Wsit.DAL.Operations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mhasb.Services.Commons
{
    public class IndustryService : IIndustryService
    {
        private readonly CrudOperation<Industry> industryRep = new CrudOperation<Industry>();

        public List<Industry> GetAllIndustries()
        {
            try
            {
                var industryObj = industryRep.GetOperation()
                                        .Get().ToList();

                return industryObj;

            }
            catch (Exception ex)
            {
                var rr = ex.Message;
                return null;
            }

        }
    }
}
