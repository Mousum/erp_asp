using Mhasb.Domain.Accounts;
using Mhasb.Wsit.DAL.Operations;
using Mhasb.Wsit.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mhasb.Services.Accounts
{
    public class VoucherTypeService : IVoucherType
    {

        private readonly CrudOperation<VoucherType> voucherRep = new CrudOperation<VoucherType>();
        public bool AddVoucherType(VoucherType voucherType)
        {
            try
            {
                voucherType.State = ObjectState.Added;
                voucherRep.AddOperation(voucherType);
                voucherType.State = ObjectState.Unchanged;
                return true;
            }
            catch (Exception ex)
            {
                var rr = ex.Message;
                return false;
            }

        }
        public bool UpdateVoucherType(VoucherType voucherType)
        {
            try
            {
                voucherType.State = ObjectState.Modified;
                voucherRep.UpdateOperation(voucherType);
                voucherType.State = ObjectState.Unchanged;
                return true;
            }
            catch (Exception ex)
            {
                var rr = ex.Message;
                return false;
            }

        }


        public List<VoucherType> GetAllVoucherType()
        {
            try
            {
                var vouchObj = voucherRep.GetOperation()
                                        .Get().ToList();

                return vouchObj;
            }
            catch (Exception ex)
            {
                var rr = ex.Message;
                return null;
            }
        }

        public VoucherType GetVoucherTypeById(int id)
        {
            try
            {
                var vouchObj = voucherRep.GetOperation()
                                        .Filter(c=>c.Id==id)
                                        .Get().SingleOrDefault();

                return vouchObj;
            }
            catch (Exception ex)
            {
                var rr = ex.Message;
                return null;
            }
        }

        public bool DeleteVoucherTypeById(int id)
        {
            try
            {
                voucherRep.DeleteOperation(id);
                return true;
            }
            catch (Exception ex)
            {
                var rr = ex.Message;
                return false;
            }

        }

    }
}
