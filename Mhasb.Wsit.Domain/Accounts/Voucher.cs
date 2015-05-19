using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mhasb.Domain.OrgSettings;
using Mhasb.Wsit.Domain;

namespace Mhasb.Domain.Accounts
{
    public class Voucher:IObjectStateLong
    {

        public int FinancialSettingId { get; set; }
        public int VoucherTypeId { get; set; }
        public int BranchId { get; set; }
        public int CurrencyId { get; set; }

        public DateTime VoucherDate { get; set; }
        public string VoucherNo { get; set; }
        public string RefferenceNo { get; set; }
        public string Description { get; set; }
        public string ChequeNo { get; set; }
        public DateTime ChequeDate { get; set; }
        public string BillNo { get; set; }
        public DateTime BillDate { get; set; }
        public string ChallenNo { get; set; }
        public string OrderNo { get; set; }

        public short Posted { get; set; }
        public short Disbursed { get; set; }
        public bool Df { get; set; }

        // financial Setting
        public virtual FinancialSetting FinancialSettings { get; set; }
        //voucher type
        //public virtual VoucherType VoucherTypes { get; set; }
        // branch 
        public virtual ICollection<VoucherDetail> VoucherDetails { get; set; }
        // currency
        public virtual Currency Currencies { get; set; }
        public long Id { get; set; }

        public ObjectState State { get; set; }
    }
}
