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
    public class AreaTimeService : IAreaTimeService
    {
        private readonly CrudOperation<AreaTime> areaTimeRep = new CrudOperation<AreaTime>();

        public bool AddAreaTime(AreaTime areaTime) 
        {
            try
            {
                areaTime.State = ObjectState.Added;
                areaTimeRep.AddOperation(areaTime);
                return true;
            }
            catch (Exception ex)
            {
                var rr = ex.Message;
                return false;
            }

        }
        public bool UpdateAreaTime(AreaTime areaTime)
        {
            try
            {
                areaTime.State = ObjectState.Modified;
                areaTimeRep.UpdateOperation(areaTime);
                return true;
            }
            catch (Exception ex)
            {
                var rr = ex.Message;
                return false;
            }

        }
        public bool DeleteAreaTime(int areaTimeId)
        {
            try
            {
                areaTimeRep.DeleteOperation(areaTimeId);
                return true;
            }
            catch (Exception ex)
            {
                var rr = ex.Message;
                return false;
            }

        }
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
        public AreaTime GetSingleAreaTime(int areaTimeId)
        {
            try
            {
                //company.State = ObjectState.Unchanged;
                var comObj = areaTimeRep.GetOperation()
                                        .Filter(c => c.Id == areaTimeId)
                                        .Get().SingleOrDefault();

                //companyRep.GetSingleObject(companyId);
                return comObj;

            }
            catch (Exception ex)
            {
                var rr = ex.Message;
                return null;
            }
        }
    }
}
