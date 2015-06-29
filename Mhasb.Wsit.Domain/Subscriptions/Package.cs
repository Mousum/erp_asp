using Mhasb.Wsit.Domain;

namespace Mhasb.Domain.Subscriptions
{
    public class Package : IObjectStateLong
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public decimal Amount { get; set; }
        public int Duration { get; set; }

        public string Descriptions { get; set; }

        public PackageEnum Status { get; set; }
        public ObjectState State { get; set; }

    }
    public enum PackageEnum
    {
        Active = 1,
        Suspended = 2,
        Expired = 3
    }

}
