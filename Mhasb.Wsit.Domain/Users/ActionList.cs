using Mhasb.Wsit.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mhasb.Domain.Users
{
    public class ActionList:IObjectStateInt
    {

        public string ModuleName { get; set; }
        public string ControllerName { get; set; }
        public string ActionName { get; set; }
        public string NameToShow { get; set; }
        public bool IsShowInMenu { get; set; }
        public int OrderNo { get; set; }

        public int Id { get; set; }
        public ObjectState State { get; set; }
    }
}
