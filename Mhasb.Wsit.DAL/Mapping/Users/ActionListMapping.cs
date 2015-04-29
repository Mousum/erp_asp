using Mhasb.Domain.Users;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mhasb.DAL.Mapping.Users
{
   public class ActionListMapping:EntityTypeConfiguration<ActionList>
    {
       public ActionListMapping() {
           this.HasKey(a=>a.Id);
           this.Ignore(a => a.State);

           this.Property(a => a.ModuleName).HasMaxLength(100).HasColumnName("module_name");
           this.Property(a => a.ControllerName).HasMaxLength(100).HasColumnName("controller_name");
           this.Property(a => a.ActionName).HasMaxLength(100).HasColumnName("action_name");
           this.Property(a => a.NameToShow).HasMaxLength(100).HasColumnName("name_to_show");
           this.Property(a => a.OrderNo).HasColumnName("order_no");
           this.Property(a => a.IsShowInMenu).HasColumnName("is_show_in_menu");

           this.ToTable("sec.actionlists");

       }
    }
}
