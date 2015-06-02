using Mhasb.Domain.Users;
using Mhasb.Services.Users;
using Mhasb.Wsit.Web.AuthSecurity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Mhasb.Services.Accounts;
using Mhasb.Services.Organizations;

namespace Mhasb.Wsit.Web.Controllers
{
    [Authorize]
    public class BaseController : Controller
    {
        protected override void OnActionExecuted(ActionExecutedContext filterContext)
        {
             base.OnActionExecuted(filterContext);

        }
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);

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


            if ((action == "Registration" && controller == "Company") || (action == "InvitationConfirm" && controller == "Invitations"))
                return;


            actionList = actionService.GetActionListByActionList(actionList);

            IUserInRoleService userInRoleService = new UserInRoleService();
            IUserService userService = new UserService();
            ISettingsService setService = new SettingsService();
            IRoleVsActionService rvaService = new RoleVsActionService();
            ICompanyService cService = new CompanyService();

            var user = userService.GetSingleUserByEmail(HttpContext.User.Identity.Name);
            if (user == null)
            {
                filterContext.Result = new RedirectResult("~/");
                return;
            }
            var comSet = setService.GetAllByUserId(user.Id);
            if (comSet == null)
            {
                filterContext.Result = new RedirectResult("~/OrganizationManagement/Company/Registration");
                return;
            }
            var myCompany = comSet.Companies.Id;
            var activatedCompany = cService.GetSingleCompany(myCompany);
            if (activatedCompany.Users.Id == user.Id)
                return;

            var roleList = userInRoleService.GetRoleListByUserAndCompany(user.Id, myCompany);
            foreach (var role in roleList)
            {
                var accessableActionList = rvaService.GetActionByRoleId(role.RoleId);
                foreach (var accessableAction in accessableActionList)
                {
                    if (accessableAction.ActionId == actionList.Id)
                        return;
                }

            }
            //if (roleList.SelectMany(role => rvaService.GetActionByRoleID(role.RoleId)).Any(accessableAction => accessableAction.ActionId == actionList.Id))
            //{
            //    return;
            //}

            filterContext.Result = new RedirectResult("~/Home/AccessDenied");
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