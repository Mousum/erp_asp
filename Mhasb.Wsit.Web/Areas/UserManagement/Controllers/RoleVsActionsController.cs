using Mhasb.Domain.Users;
using Mhasb.Services.Users;
using Mhasb.Wsit.Web.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Mhasb.Wsit.Web.Areas.UserManagement.Controllers
{
    public class RoleVsActionsController : BaseController
    {
        private IRoleVsActionService roleVsActionService = new RoleVsActionService();
        private IRoleService rService = new RoleService();
        private IActionListService alService = new ActionListService();

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Create()
        {
            var ActionList = alService.GetAllActionList();

            var mList = ActionList.Select(m => new { Id = m.ModuleName, ModuleName = m.ModuleName }).Distinct();
            var cList = ActionList.Select(m => new { Id = m.ControllerName, ControllerName = m.ControllerName }).Distinct();

            ViewBag.roleList = new SelectList(rService.GetAllRoles(), "Id", "RoleName");
            ViewBag.moduleList = new SelectList(mList, "Id", "ModuleName");// ModuleName
            ViewBag.ControllerList = new SelectList(cList, "Id", "ControllerName");//ControllerName
            return View();
        }
        [HttpPost]
        public ActionResult Create(int RoleId, string ModuleName, string ControllerName, int[] ActionId)
        {
            var roleVsAction = new RoleVsAction();
            var model = roleVsActionService.GetActionByRoleID(RoleId);
            model = model.Where(m => m.ActionLists.ModuleName == ModuleName && m.ActionLists.ControllerName == ControllerName).ToList();
            foreach (var item in model)
            {
                if (ActionId == null)
                {
                    roleVsAction.ActionId = item.ActionId;
                    roleVsAction.IsActive = false;
                    roleVsAction.RoleId = RoleId;
                }
                else
                {
                    if (ActionId.Contains(item.ActionId))
                    {
                        roleVsAction.ActionId = item.ActionId;
                        roleVsAction.IsActive = true;
                        roleVsAction.RoleId = RoleId;
                    }
                    else
                    {
                        roleVsAction.ActionId = item.ActionId;
                        roleVsAction.IsActive = false;
                        roleVsAction.RoleId = RoleId;

                    }
                }

                roleVsActionService.AddRoleVsAction(roleVsAction);
            }
            ViewBag.Msg = "Operation SuccessFull";

            return View("CreateSucess");
        }

        [HttpPost]
        public PartialViewResult GetActionList(int RoleId, string moduleName, string controllerName)
        {
            var model = roleVsActionService.GetActionByRoleID(RoleId);
            if (moduleName != "" && controllerName != "")
            {
                model = model.Where(m => m.ActionLists.ModuleName == moduleName && m.ActionLists.ControllerName == controllerName).ToList();
            }
            else if (moduleName != "")
            {
                model = model.Where(m => m.ActionLists.ModuleName == moduleName).ToList();
            }
            else if (controllerName != "")
            {
                model = model.Where(m => m.ActionLists.ControllerName == controllerName).ToList();
            }
            else if (moduleName == "" && controllerName == "")
            {
                model = model;
            }
            return PartialView("_GetActionList", model);
        }




    }
}