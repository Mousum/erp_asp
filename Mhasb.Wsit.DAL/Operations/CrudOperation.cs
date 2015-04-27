using Mhasb.Wsit.DAL.Infrastructure;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mhasb.Wsit.DAL.Operations
{
    public class CrudOperation<T> : ICrudOperation<T> where T : class
    {
        private readonly UnitOfWork unitOfWork = new UnitOfWork();

        #region ICrudOperation<T> Members

        public RepositoryQuery<T> GetOperation()
        {
            var countryRep = unitOfWork.Repository<T>().Query();
            return countryRep;
        }

        public void AddOperation(T Operation)
        {
           

            try
            {
                var countryRep = unitOfWork.Repository<T>();
                countryRep.Insert(Operation);
                unitOfWork.Save();
            }
            catch (DbEntityValidationException e)
            {
                               throw e;
            }

        }

        public void DeleteOperation(object Id)
        {
            unitOfWork.Repository<T>().Delete(Id);
            unitOfWork.Save();
        }

        public void UpdateOperation(T Operation)
        {
            unitOfWork.Repository<T>().Update(Operation);
            unitOfWork.Save();

        }

        public T GetSingleObject(object id)
        {
            var singleObject = unitOfWork.Repository<T>().FindById(id);
            return singleObject;
        }

        #endregion
        
    }
}
