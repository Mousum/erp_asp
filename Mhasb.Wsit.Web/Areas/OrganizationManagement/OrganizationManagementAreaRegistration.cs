using System.Web.Mvc;

namespace Mhasb.Wsit.Web.Areas.OrganizationManagement
{
    public class OrganizationManagementAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "OrganizationManagement";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "OrganizationManagement_default",
                "OrganizationManagement/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}