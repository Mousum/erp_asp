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
    
    public partial class adminuserlog
    {
        public int Id { get; set; }
        public int adminuser { get; set; }
        public int actionid { get; set; }
        public string ip { get; set; }
        public System.DateTime actionname { get; set; }
    
        public virtual adminuser adminuser1 { get; set; }
        public virtual actionlist actionlist { get; set; }
    }
}
