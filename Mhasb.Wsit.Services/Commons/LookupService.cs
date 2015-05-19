using Mhasb.Domain.Commons;
using Mhasb.Wsit.DAL.Operations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mhasb.Services.Commons
{
    public class LookupService : ILookupService
    {
        private readonly CrudOperation<Lookup> _finalCrudOperation = new CrudOperation<Lookup>();
     
        public List<Lookup> GetLookupByType(string LookupType)
        {
            try {

                var LookupObj = _finalCrudOperation.GetOperation()
                                        .Filter(c => c.LookupType == LookupType)
                                        .Get().ToList();
                return LookupObj;
            }catch(Exception ex){
                var msg = ex.Message;
                return null;
            }
        }
    }
}
