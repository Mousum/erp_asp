//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Mhasb.DAL.Data.Db_Diagram
{
    using System;
    using System.Collections.Generic;
    
    public partial class company_profiles
    {
        public company_profiles()
        {
            this.contact_detail = new HashSet<contact_detail>();
        }
    
        public int Id { get; set; }
        public long UserId { get; set; }
        public string business_name { get; set; }
        public string vision { get; set; }
        public Nullable<int> turn_over { get; set; }
        public string objectives { get; set; }
        public string mission { get; set; }
        public string expertise { get; set; }
        public string activities { get; set; }
        public string markets { get; set; }
        public string previous_work { get; set; }
        public string address { get; set; }
        public string imagelocation { get; set; }
        public Nullable<int> location { get; set; }
        public string Email { get; set; }
        public int companyid { get; set; }
    
        public virtual ICollection<contact_detail> contact_detail { get; set; }
        public virtual company company { get; set; }
    }
}
