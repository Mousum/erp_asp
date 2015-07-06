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
using Mhasb.Wsit.CustomModel.GridView;

namespace Mhasb.Wsit.Web.Controllers
{
    public class CommonBaseController : Controller
    {

        /// <summary>
        /// Declare Global User Variable
        /// </summary>
        /// <param name="filterContext"></param>
        /// 

        IUserInRoleService userInRoleService = new UserInRoleService();
        IUserService userService = new UserService();
        ISettingsService setService = new SettingsService();
        IRoleVsActionService rvaService = new RoleVsActionService();
        ICompanyService cService = new CompanyService();
        ICompanyViewLog companyViewLog = new CompanyViewLogService();


        public long UserId;
        public int CompanyId;
       
        public CommonBaseController()
        {
            UserId = 0;
            CompanyId = 0;
            init();
        }
        public void init()
        {
            if (HttpContext != null)
            {
                var user = userService.GetSingleUserByEmail(HttpContext.User.Identity.Name);
                if (user != null)
                    UserId = user.Id;
                var logObj = companyViewLog.GetLastViewCompanyByUserId(UserId);

                if (logObj != null)
                {
                    CompanyId = (int)logObj.CompanyId;
                }
            }
        }

        [NonAction]
        public bool checkCompanyFlow(out string absUrl)
        {
            init();
            absUrl = "";
            
            // if user null, then redirect to login page and  clear session/cookie data.
            if (UserId == 0)
            {
                absUrl = Url.Action("Logout", "Users", new { area = "UserManagement" });
                return false;
            }
            if (CompanyId == 0)
            {
                absUrl =Url.Action("MyMhasb", "Users", new { area = "UserManagement" });
                return false;
            }

            var myCompany = cService.GetSingleCompany(CompanyId);
            //if(myCompany==null)
            //{
            //    filterContext.Result = new RedirectResult(Url.Action("MyMhasb", "Users", new { area = "UserManagement" }));
            //    return;
            //}




            if (myCompany.CompleteFlag != 5 && myCompany.Users.Id == UserId)
            {
                if (myCompany.CompleteFlag == 0)
                    absUrl =Url.Action("Update", "Company", new { area = "OrganizationManagement" });
                else if (myCompany.CompleteFlag == 1)
                    absUrl =Url.Action("Create", "FinalcialSetting", new { area = "OrgSettings" });
                else if (myCompany.CompleteFlag == 2)
                    absUrl =Url.Action("Create", "Invitations", new { area = "NotificationManagement" });
                else if (myCompany.CompleteFlag == 3)
                    absUrl =Url.Action("Create", "ChartOfAccounts", new { area = "Accounts" });
                else if (myCompany.CompleteFlag == 4)
                    absUrl =Url.Action("Finish", "Users", new { area = "UserManagement" });
                return false;
            }
            else if (myCompany.CompleteFlag != 5 && myCompany.Users.Id != UserId)
            {
                //should be change 
                absUrl ="~/Home/AccessDenied";
                return false;
            }

            return true;
        }
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);





        }

        public ActionResult DataGridView()
        {
            List<DataGridView> dgvObj = new List<DataGridView>();
            DataGridView dgv = new DataGridView();
            dgv.Name = "A";
            dgv.Type = "text";
            dgv.Options = null;
            dgvObj.Add(dgv);
            dgv = new DataGridView();
            List<OptionList> optObj = new List<OptionList>();
            OptionList opt = new OptionList();
            opt.Name = "Option 1";
            opt.Value = "1";
            optObj.Add(opt);
            opt = new OptionList();
            opt.Name = "Option 2";
            opt.Value = "2";
            optObj.Add(opt);

            dgv.Name = "B";
            dgv.Type = "dropdown";
            dgv.Options = optObj;
            dgvObj.Add(dgv);
            
            //string[,] opt = new string[,]{
            //                                {"name","val"},
            //                                {"name2","val2"}
            //                            };
          

                  
            //string[,] HeaderList = new string[,]{
            //                                        { "Name", "text", null},
            //                                        {"Type","Dropdown", string.Format(opt.ToString())}
            //                                    };
            //ViewBag.HeaderList = HeaderList;
            //ViewBag.HeaderCount = HeaderList.LongLength/3;
            return View( dgvObj);
        }

    }
}