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
    
    public partial class rolevsaction
    {
        public int Id { get; set; }
        public int roleid { get; set; }
        public int actionid { get; set; }
        public bool isactive { get; set; }
    
        public virtual actionlist actionlist { get; set; }
        public virtual role role { get; set; }
    }
}
