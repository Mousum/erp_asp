using Mhasb.Domain.Commons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mhasb.Services.Commons
{
    public interface ILegalEntityService
    {
        List<LegalEntity> GetAllLegalEntities();
        bool AddLegalEntiy(LegalEntity legalEntity);
        bool UpdateLegalEntiy(LegalEntity legalEntity);
        bool DeleteLegalEntity(int lEId);
        LegalEntity GetSingleLegalEntity(int lEId);
    }
}
