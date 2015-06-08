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
            IUserService userService = new UserService();
            ISettingsService setService = new SettingsService();
            IRoleVsActionService rvaService = new RoleVsActionService();
            ICompanyService cService = new CompanyService();
            ICompanyViewLog companyViewLog = new CompanyViewLogService();


            var user = userService.GetSingleUserByEmail(HttpContext.User.Identity.Name);
            // if user null, then redirect to login page and  clear session/cookie data.
            if (user == null)
            {
                filterContext.Result = new RedirectResult(Url.Action("Logout", "Users", new { area = "UserManagement" }));
                return;
            }

            var logObj = companyViewLog.GetLastViewCompanyByUserId(user.Id);
            int companyId = 0;
            if (logObj != null)
                companyId = (int)logObj.CompanyId;
            else
            {
                filterContext.Result = new RedirectResult(Url.Action("MyMhasb", "Users", new { area = "UserManagement" }));
                return;
            }
            if (companyId == 0)
            {
                filterContext.Result = new RedirectResult(Url.Action("MyMhasb", "Users", new { area = "UserManagement" }));
                return;
            }
            
            var myCompany = cService.GetSingleCompany(companyId);
            if (myCompany == null)
            {
                filterContext.Result = new RedirectResult(Url.Action("MyMhasb", "Users", new { area = "UserManagement" }));
                return;
            }

            //if(!((action == "Update" && controller == "Company") || (action == "Create" && controller == "FinalcialSetting")|| (action == "Create" && controller == "Invitations")|| (action == "Create" && controller == "ChartOfAccounts")|| (action == "Finish" && controller == "Users")))
            //{
            //    string absUrl;
            //    if (!checkCompanyFlow(out absUrl))
            //    {
            //        filterContext.Result = new RedirectResult(absUrl);
            //        return;
            //    }
            //}

            //if (myCompany.CompleteFlag != 5  && myCompany.Users.Id==user.Id)
            //{
            //    if(myCompany.CompleteFlag==0)
            //        filterContext.Result = new RedirectResult(Url.Action("Update", "Company", new { area = "OrganizationManagement" }));
            //    else if (myCompany.CompleteFlag == 1)
            //        filterContext.Result = new RedirectResult(Url.Action("Create", "FinalcialSetting", new { area = "OrgSettings" }));
            //    else if (myCompany.CompleteFlag == 2)
            //        filterContext.Result = new RedirectResult(Url.Action("Create", "Invitations", new { area = "NotificationManagement" }));
            //    else if (myCompany.CompleteFlag == 3)
            //        filterContext.Result = new RedirectResult(Url.Action("Create", "ChartOfAccounts", new { area = "Accounts" }));
            //    else if (myCompany.CompleteFlag == 4)
            //        filterContext.Result = new RedirectResult(Url.Action("Finish", "Users", new { area = "UserManagement" }));
            //    return;
            //}
            //else if (myCompany.CompleteFlag != 5 && myCompany.Users.Id != user.Id)
            //{
            //    //should be change 
            //    filterContext.Result = new RedirectResult("~/Home/AccessDenied");
            //    return;
            //}

            // Block this code for companies setup follow 
            //var comSet = setService.GetAllByUserId(user.Id);
            //if (comSet == null)
            //{
            //    filterContext.Result = new RedirectResult("~/OrganizationManagement/Company/Update");
            //    return;
            //}
            //var myCompany = comSet.Companies.Id;
            var activatedCompany = cService.GetSingleCompany(companyId);
            if (activatedCompany.Users.Id == user.Id)
                return;

            var roleList = userInRoleService.GetRoleListByUserAndCompany(user.Id, companyId);
            foreach (var role in roleList)
            {
                var accessableActionList = rvaService.GetActionByRoleId(role.RoleId);
                foreach (var accessableAction in accessableActionList)
                {
                    if (accessableAction.ActionId == actionList.Id)
                        return;
                }

            }



            // Old Block Dont upBlock pls brothers 
            //if (roleList.SelectMany(role => rvaService.GetActionByRoleID(role.RoleId)).Any(accessableAction => accessableAction.ActionId == actionList.Id))
            //{
            //    return;
            //}

            //filterContext.Result = new RedirectResult("~/Home/AccessDenied");
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