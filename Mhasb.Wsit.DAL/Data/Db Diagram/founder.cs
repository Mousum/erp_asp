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
    
    public partial class founder
    {
        public founder()
        {
            this.sharetransfers = new HashSet<sharetransfer>();
            this.sharetransfers1 = new HashSet<sharetransfer>();
        }
    
        public int Id { get; set; }
        public string name { get; set; }
        public string residence { get; set; }
        public string tel { get; set; }
        public string fax { get; set; }
        public string pobox { get; set; }
        public string email { get; set; }
        public int shares_owned { get; set; }
        public double shares_unit_value { get; set; }
        public int countryid { get; set; }
        public int languageid { get; set; }
        public int companyid { get; set; }
    
        public virtual country country { get; set; }
        public virtual language language { get; set; }
        public virtual company company { get; set; }
        public virtual ICollection<sharetransfer> sharetransfers { get; set; }
        public virtual ICollection<sharetransfer> sharetransfers1 { get; set; }
    }
}
