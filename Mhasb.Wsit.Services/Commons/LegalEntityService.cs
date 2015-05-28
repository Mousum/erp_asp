using Mhasb.Domain.Commons;
using Mhasb.Wsit.DAL.Operations;
using Mhasb.Wsit.Domain;
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
        public bool AddLegalEntiy(LegalEntity legalEntity)
        {
            try
            {
                legalEntity.State = ObjectState.Added;
                legalEntityRep.AddOperation(legalEntity);

                return true;

            }
            catch (Exception ex)
            {
                var rr = ex.Message;
                return false;
            }
        }

        public bool UpdateLegalEntiy(LegalEntity legalEntity)
        {
            try
            {
                legalEntity.State = ObjectState.Modified;
                legalEntityRep.UpdateOperation(legalEntity);

                return true;

            }
            catch (Exception ex)
            {
                var rr = ex.Message;
                return false;
            }
        }
        public bool DeleteLegalEntity(int lEId)
        {
            try
            {
                legalEntityRep.DeleteOperation(lEId);
                return true;

            }
            catch (Exception ex)
            {
                var rr = ex.Message;
                return false;
            }
        }
        public LegalEntity GetSingleLegalEntity(int lEId)
        {
            try
            {
                 var obj = legalEntityRep.GetOperation()
                     .Filter(l=>l.Id==lEId).Get().SingleOrDefault();
                return obj;

            }
            catch (Exception ex)
            {
                var rr = ex.Message;
                return null;
            }
        }
    }
}
