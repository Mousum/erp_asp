using Mhasb.Domain.Commons;
using Mhasb.Wsit.DAL.Operations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mhasb.Services.Commons
{
    public class AreaTimeService : IAreaTimeService
    {
        private readonly CrudOperation<AreaTime> areaTimeRep = new CrudOperation<AreaTime>();

        public List<AreaTime> GetAllAreaTimes()
        {
            try
            {
                var areaTimeObj = areaTimeRep.GetOperation()
                                        .Get().ToList();

                return areaTimeObj;

            }
            catch (Exception ex)
            {
                var rr = ex.Message;
                return null;
            }

        }

    }
}
