using Mhasb.Wsit.DAL.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mhasb.Wsit.DAL.Operations
{
    public interface ICrudOperation<T> where T : class
    {
        RepositoryQuery<T> GetOperation();
        void AddOperation(T Operation);
        void DeleteOperation(object Id);
        void UpdateOperation(T Operation);
        T GetSingleObject(object id);

    }
}
