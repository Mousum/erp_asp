using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Mhasb.Wsit.Web.Controllers
{
    [Authorize]
    public class BaseController : Controller
    {
        protected override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            base.OnActionExecuted(filterContext);
            // To get area,controller and action name from http request
            var routeData = filterContext.RequestContext.RouteData;
            var module = (string)routeData.DataTokens["area"];
            var controller = routeData.GetRequiredString("controller");
            var action = routeData.GetRequiredString("action");


        }
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);
        }
        protected override void OnAuthorization(AuthorizationContext filterContext)
        {
            base.OnAuthorization(filterContext);
        }
       
	}
}