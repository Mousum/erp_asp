using Mhasb.Domain.Commons;
using Mhasb.Domain.Organizations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mhasb.Wsit.CustomModel.Organizations
{
    public class CompanyProfileCustom
    {
        public CompanyProfile companyProfile { get; set; }
        public ContactDetail Phone { get; set; }

        public ContactDetail Website { get; set; }
        public ContactDetail Fax { get; set; }
        public ContactDetail Skype { get; set; }
        public ContactDetail Facebook { get; set; }
        public ContactDetail Twitter { get; set; }
        public ContactDetail LinkedIn { get; set; }
        public ContactDetail Google { get; set; }


        public CompanyProfileCustom()
        {
            Phone = new ContactDetail { FieldName = "Phone" };
            Fax = new ContactDetail { FieldName = "Fax" };
            Website = new ContactDetail { FieldName = "Website" };
            Facebook = new ContactDetail { FieldName = "Facebook", FieldUrl = "www.facebook.com" };
            Twitter = new ContactDetail { FieldName = "Twitter", FieldUrl = "www.twitter.com" };
            Google = new ContactDetail { FieldName = "Google", FieldUrl = "www.google.com" };
            LinkedIn = new ContactDetail { FieldName = "LinkedIn", FieldUrl = "www.linkedin.com" };
            Skype = new ContactDetail { FieldName = "Skype", FieldUrl = "www.skype.com" };
        }

        public CompanyProfileCustom(CompanyProfile companyProfile)
        {
            Phone = new ContactDetail { FieldName = "Phone" };
            Fax = new ContactDetail { FieldName = "Fax" };
            Website = new ContactDetail { FieldName = "Website" };
            Facebook = new ContactDetail { FieldName = "Facebook", FieldUrl = "www.facebook.com" };
            Twitter = new ContactDetail { FieldName = "Twitter", FieldUrl = "www.twitter.com" };
            Google = new ContactDetail { FieldName = "Google", FieldUrl = "www.google.com" };
            LinkedIn = new ContactDetail { FieldName = "LinkedIn", FieldUrl = "www.linkedin.com" };
            Skype = new ContactDetail { FieldName = "Skype", FieldUrl = "www.skype.com" };
        }

    }
}
