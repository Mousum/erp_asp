using Mhasb.Wsit.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mhasb.Domain.Subscriptions
{
   public class Package:IObjectStateLong
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public decimal Amount { get; set; }
        public int Duration { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public PackageEnum Status { get; set; }
        public ObjectState State { get; set; }

    }
    public enum PackageEnum
    {
        test1 = 1,
        test2 = 2,
        test3 = 3,
        test4 = 4
    }

}
