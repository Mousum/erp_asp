using Mhasb.Domain.Commons;
using Mhasb.Wsit.DAL.Operations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mhasb.Services.Commons
{
    public class LegalEntityService : ILegalEntityService
    {
        private readonly CrudOperation<LegalEntity> legalEntityRep = new CrudOperation<LegalEntity>();

        public List<LegalEntity> GetAllLegalEntities()
        {
            try
            {
                var legalEntityObj = legalEntityRep.GetOperation()
                                        .Get().ToList();

                return legalEntityObj;

            }
            catch (Exception ex)
            {
                var rr = ex.Message;
                return null;
            }

        }

    }
}
