using Mhasb.Domain.Users;
using Mhasb.Services.Users;
using Mhasb.Wsit.Web.AuthSecurity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Mhasb.Wsit.Web.Controllers
{
    [Authorize]
    public class BaseController : Controller
    {
        protected override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            base.OnActionExecuted(filterContext);

             IActionListService actionService = new ActionListService();
            // To get area,controller and action name from http request
            var routeData = filterContext.RequestContext.RouteData;
            var module = (string)routeData.DataTokens["area"];
            var controller = routeData.GetRequiredString("controller");
            var action = routeData.GetRequiredString("action");
            // to save Action List 
            var actionList = new ActionList
            {
                ModuleName = module,
                ControllerName = controller,
                ActionName = action,
                IsShowInMenu = false,
            };
            actionService.AddActionListFromBaseController(actionList);


        }
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);
        }
        protected override void OnAuthorization(AuthorizationContext filterContext)
        {
            base.OnAuthorization(filterContext);
            HttpCookie cookie = Request.Cookies[FormsAuthentication.FormsCookieName];
            if (cookie != null)
            {
                FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(cookie.Value);
                var newTicket = FormsAuthentication.RenewTicketIfOld(ticket);
                if (newTicket.Expiration != ticket.Expiration)
                {
                    string encryptedTicket = FormsAuthentication.Encrypt(newTicket);

                    cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
                    cookie.Path = FormsAuthentication.FormsCookiePath;
                    Response.Cookies.Add(cookie);
                }
                CustomPrincipal.Login(ticket.UserData);
            }
        }
       
	}
}