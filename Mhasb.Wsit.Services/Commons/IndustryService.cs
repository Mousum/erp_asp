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


        public bool CreateIndustry(Industry industry)
        {
            try
            {
                industry.State = ObjectState.Added;
                industryRep.AddOperation(industry);
                return true;
            }
            catch (Exception ex)
            {
                var msg = ex.Message;
                return false;
            }
        }

        public bool UpdateIndustry(Industry industry)
        {
            try
            {
                industry.State = ObjectState.Modified;
                industryRep.AddOperation(industry);
                return true;
            }
            catch (Exception ex)
            {
                var msg = ex.Message;
                return false;
            }
        }

        public bool DeleteIndustry(int Id)
        {
            try
            {
                industryRep.DeleteOperation(Id);
                return true;
            }
            catch (Exception ex)
            {
                var msg = ex.Message;
                return false;
            }
        }
    }
}
