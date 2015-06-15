using Mhasb.Domain.Users;
using Mhasb.Wsit.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mhasb.Domain.AdminUsers
{
    public class AdminUserLog : IObjectStateInt
    {
        [Key]
        public int Id { get; set; }

        public int AdminId { get; set; }

        public int ActionId { get; set; }

        public string Ip { get; set; }

        public DateTime ActionTime { get; set; }
       
        public ObjectState State { get; set; }
        //Admin User 

        public virtual AdminUser AdminUser { get; set; }
        // Action 
        public virtual ActionList ActionList { get; set; }

       
    }
}
