using Mhasb.Domain.Organizations;
using Mhasb.Wsit.Domain;

namespace Mhasb.Domain.Commons
{
    public class ContactDetail : IObjectStateLong
    {
        public long? EmployeeProfileId { get; set; }
        public int? CompanyProfileId { get; set; }
        public string FieldName { get; set; }
        public string FieldValueOne { get; set; }
        public string FieldValueTwo { get; set; }
        public string FieldValueThree { get; set; }
        public string FieldUrl { get; set; }

        public virtual EmployeeProfile EmployeeProfiles { get; set; }
        public virtual CompanyProfile CompanyProfiles { get; set; }
        public long Id
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
