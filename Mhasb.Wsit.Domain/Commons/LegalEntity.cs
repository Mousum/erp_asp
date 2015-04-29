using Mhasb.Wsit.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mhasb.Domain.Commons
{
    public class LegalEntity : IObjectStateInt
    {
        public string LegalEntityName { get; set; }
        public int Id { get; set; }
        public ObjectState State { get; set; }
        
    }
}
