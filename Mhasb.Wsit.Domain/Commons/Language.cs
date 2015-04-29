using Mhasb.Domain.Organizations;
using Mhasb.Wsit.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mhasb.Domain.Commons
{
    public class Language : IObjectStateInt
    {
        public string LanguageName { get; set; }

        public virtual ICollection<Company> Companies { get; set; }
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
