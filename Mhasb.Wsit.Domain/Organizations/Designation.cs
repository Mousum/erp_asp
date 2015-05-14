using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mhasb.Wsit.Domain;

namespace Mhasb.Domain.Organizations
{
   public class Designation:IObjectStateInt
    {
       public string DesignationName { get; set; }

       public int Id
        {
            get;
            set;
        }

        public ObjectState State
        {
            get;
            set;
        }
    }
}
