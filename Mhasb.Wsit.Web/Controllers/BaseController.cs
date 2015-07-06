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
using Mhasb.Services.Loggers;

namespace Mhasb.Wsit.Web.Controllers
{
    [Authorize]
    public class BaseController : CommonBaseController
    {
        protected override void OnActionExecuted(ActionExecutedContext filterContext)
        {
             base.OnActionExecuted(filterContext);
             //string absUrl;
             //if (!checkCompanyFlow(out absUrl))
             //{
             //    filterContext.Result = new RedirectResult(absUrl);
             //    return;
             //}

        }
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);

            IActionListService actionService = new ActionListService();
            init();

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

            // Check user type, if owner then he/she can add company. 
            if ((action == "Add" && controller == "Company") || (action == "InvitationConfirm" && controller == "Invitations"))
                return;

            // To get ActionList Id
            actionList = actionService.GetActionListByActionList(actionList);

            IUserInRoleService userInRoleService = new UserInRoleService();
            IRoleVsActionService rvaService = new RoleVsActionService();
            ICompanyService cService = new CompanyService();


            if (UserId ==0)
            {
                filterContext.Result = new RedirectResult(Url.Action("Logout", "Users", new { area = "UserManagement" }));
                return;
            }
            if (CompanyId == 0)
            {
                filterContext.Result = new RedirectResult(Url.Action("MyMhasb", "Users", new { area = "UserManagement" }));
                return;
            }
            
            var myCompany = cService.GetSingleCompany(CompanyId);
            if (myCompany == null)
            {
                filterContext.Result = new RedirectResult(Url.Action("MyMhasb", "Users", new { area = "UserManagement" }));
                return;
            }

            if (!((action == "Update" && controller == "Company") || (action == "Create" && controller == "FinalcialSetting") || (action == "Create" && controller == "Invitations") || (action == "Create" && controller == "ChartOfAccounts") || (action == "Finish" && controller == "Users")))
            {
                //string absUrl;
                //if (!checkCompanyFlow(out absUrl))
                //{
                //    filterContext.Result = new RedirectResult(absUrl);
                //    return;
                //}
                return;
            }

            var activatedCompany = cService.GetSingleCompany(CompanyId);
            if (activatedCompany.Users.Id == UserId)
                return;

            var roleList = userInRoleService.GetRoleListByUserAndCompany(UserId, CompanyId);
            foreach (var role in roleList)
            {
                var accessableActionList = rvaService.GetActionByRoleId(role.RoleId);
                foreach (var accessableAction in accessableActionList)
                {
                    if (accessableAction.ActionId == actionList.Id)
                        return;
                }

            }

            //filterContext.Result = new RedirectResult("~/Home/AccessDenied");


            // Old Block Dont upBlock pls brothers 
            //if (roleList.SelectMany(role => rvaService.GetActionByRoleID(role.RoleId)).Any(accessableAction => accessableAction.ActionId == actionList.Id))
            //{
            //    return;
            //}

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