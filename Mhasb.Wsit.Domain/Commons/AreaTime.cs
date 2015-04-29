using Mhasb.Wsit.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mhasb.Domain.Commons
{
    public class AreaTime : IObjectStateInt
    {
        public string ZoneName { get; set; }
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
