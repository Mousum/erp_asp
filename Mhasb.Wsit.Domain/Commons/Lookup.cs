using Mhasb.Wsit.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mhasb.Domain.Commons
{
    public class Lookup
    {
        public int LookupId { get; set; }
        public string LookupType { get; set; }
        public string Key { get; set; }
        public decimal Quantity { get; set; }
        public string Value { get; set; }
        public string Description { get; set; }
        public int Order { get; set; }

        public ObjectState State { get; set; }
    }
}

