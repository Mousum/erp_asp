using System.Web.Mvc;

namespace Mhasb.Wsit.Web.Areas.NotificationManagement
{
    public class NotificationManagementAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "NotificationManagement";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "NotificationManagement_default",
                "NotificationManagement/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}