using Mhasb.Wsit.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mhasb.Domain.Commons
{
    public class Industry : IObjectStateInt
    {
        public string Name { get; set; }
        public int Id { get; set; }
        public ObjectState State { get; set; }
    }
}
