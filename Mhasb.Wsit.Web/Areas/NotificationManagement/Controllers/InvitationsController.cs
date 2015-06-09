using Mhasb.Domain.Notifications;
using Mhasb.Services.Notifications;
using Mhasb.Domain.Organizations;
using Mhasb.Services.Organizations;
using Mhasb.Services.Users;
using Mhasb.Domain.Users;
using Mhasb.Wsit.Web.AuthSecurity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using Mhasb.Wsit.Web.Controllers;
using Mhasb.Services.Loggers;


namespace Mhasb.Wsit.Web.Areas.NotificationManagement.Controllers
{
    public class InvitationsController : BaseController
    {
        private IInvitationService inService = new InvitationService();
        private readonly ICompanyService iCompany = new CompanyService();
        private IUserService uService = new UserService();
        private IEmployeeService eService = new EmployeeService();
        private IRoleService rService = new RoleService();
        private readonly ISettingsService sService = new SettingsService();
        private IDesignation degRep = new DesignationService();
        private IUserInRoleService URSer = new UserInRoleService();
        private readonly ICompanyViewLog _companyViewLog = new CompanyViewLogService();

        public ActionResult Index()
        {
            var model = inService.GetAllInvitation();
            return View(model);
        }

        public ActionResult Create()
        {

            User user = uService.GetSingleUserByEmail(HttpContext.User.Identity.Name);
            var logObj = _companyViewLog.GetLastViewCompanyByUserId(user.Id);
            if (logObj.Companies.CompleteFlag <= 5 && logObj.Companies.CompleteFlag >= 2) 
            {

                ViewBag.CompanyCompleteFlag = logObj.Companies.CompleteFlag;
                var designations = degRep.GetDesignations();
                var roles = rService.GetAllRoles();

                //// Add Role if not exist
                //var uu = _companyViewLog.GetLastViewCompanyByUserId(user.Id);
                //if (!roles.Any())
                //{

                //}
                List<Company> myCompanyList = iCompany.GetAllCompanies()
                                                       .Where(c => c.Users.Id == user.Id).ToList();
                var employeeType = Enum.GetValues(typeof(EmpTypeEnum))
                                        .Cast<EmpTypeEnum>()
                                        .Select(v => new { Id = Convert.ToInt32(v), Name = v.ToString() })
                                        .ToList();
                ViewBag.Desginations = new SelectList(designations, "Id", "DesignationName");
                ViewBag.EmployeeType = new SelectList(employeeType, "Name", "Name");
                ViewBag.Companies = new SelectList(myCompanyList, "Id", "TradingName");
                ViewBag.roles = new SelectList(roles, "Id", "RoleName");

                return View();

            }
            else
            {
                string absUrl;
                if (!checkCompanyFlow(out absUrl))
                {
                    return Redirect(absUrl);
                }
                TempData.Add("errMsg","Something Wrong...");
                return RedirectToAction("MyMhasb", "Users", new { Area = "UserManagement" });
            }

            
           

        }
        [HttpPost]
        public ActionResult Create(Invitation invitation)
        {
            Random random = new Random();
            string rand = random.Next().ToString();
            invitation.SendDate = DateTime.Now;
            invitation.UpdateDate = DateTime.Now;
            invitation.Token = rand;
            invitation.Status = StatusEnum.test1;
            if (inService.CreateInvitation(invitation))
            {
                //Create the key email objects
                MailMessage myemail = new MailMessage(); //Create message object
                SmtpClient mysmtpserver = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential("sumon20@gmail.com", "638848")
            }; ; //Set mail server

                //Set mail recipients
                myemail.To.Add(invitation.Email);
                //   myemail.To.Add("user2@example.com");

                //Set email properties
                myemail.From = new MailAddress("sumon20@gmail.com", "HHASB ERP");

                myemail.Subject = "This is the Email Subject";
                string host = Url.Content(HttpContext.Request.Url.Host + "/NotificationManagement/Invitations/InvitationConfirm/" + invitation.Id + "?token=" + invitation.Token);
                //    string host = Url.Content("http://localhost:2376/NotificationManagement/Invitations/InvitationConfirm/" + invitation.Id + "?token=" + invitation.Token);
                myemail.Body = "<p>Hello There"
                    + ", </p><p>To verify your account, please click the following link:</p>"
                                            + "<p><a href='" + host + "' target='_blank'> Click"
                                            + "</a></p><div>Best regards,</div><div>Someone</div><p>Do not forward "
                                            + "<b>this email. The verify link is private.</b></p>";
                myemail.IsBodyHtml = true; //Send this as plain-text



                //Send the email
                mysmtpserver.Send(myemail);

                var user = uService.GetSingleUserByEmail(HttpContext.User.Identity.Name);
                var AccSet = sService.GetAllByUserId(user.Id);
                var logObj = _companyViewLog.GetLastViewCompanyByUserId(user.Id);
                int companyId = 0;
                if (logObj != null)
                {
                    companyId = (int)logObj.CompanyId;
                }
                if (logObj.Companies.CompleteFlag ==2)
                {
                    int flag = 3;
                    if (iCompany.UpdateCompleteFlag(companyId, flag))
                    {
                        return RedirectToAction("Create", "ChartOfAccounts", new { Area = "Accounts" });
                    }
                    else
                    {
                        return RedirectToAction("Create");
                    }
                }
                
                //string host = HttpContext.Request.Url.Host + ":2376/NotificationManagement/Invitations/InvitationConfirm/" + invitation.Id + "?token=" + invitation.Token;


                //string msg = "<html><head><meta content=\"text/html; charset=utf-8\" /></head><body><p>Hello There"
                //                            + ", </p><p>To verify your account, please click the following link:</p>"
                //                            + "<p><a href=\"" + host + "\" target=\"_blank\">" + host
                //                            + "</a></p><div>Best regards,</div><div>Someone</div><p>Do not forward "
                //                            + "this email. The verify link is private.</p></body></html>";
                //msg = HttpUtility.HtmlEncode(msg);



                //string to = invitation.Email;
                //string subject = "Invitation From MHASB Erp";
                //string body = msg;
                //sendMail(to, subject, body);

            }
            return RedirectToAction("Create");
            
        }

        [AllowAnonymous]
        public ActionResult InvitationConfirm(int id, string token)
        {
            var Invitation = inService.GetSingleInvitation(id);
            if (Invitation != null)
            {
                if (Invitation.Token == token)
                {
                    if (Invitation.Status != StatusEnum.test2)
                    {

                        Invitation.Status = StatusEnum.test2;
                        inService.AcceptInvitation(Invitation);
                        ViewBag.Company = Invitation.CompanyId;
                        ViewBag.email = Invitation.Email;
                        var user = uService.GetSingleUserByEmail(Invitation.Email);
                        if (user == null)
                        {
                            return View("InvitationConfirm");
                        }
                        else
                        {
                            var emp = new Employee();
                            emp.UserId = user.Id;
                            emp.CompanyId = Invitation.CompanyId;
                            emp.DesignationId = Invitation.DesignationId;
                            if (eService.CreateEmployee(emp))
                            {
                                var userInRole = new UserInRole();
                                userInRole.EmployeeId = emp.Id;
                                userInRole.RoleId = Invitation.RoleId;
                                userInRole.IsActive = true;
                                URSer.AddUserInRole(userInRole);

                                Session.Add("msg", "You have to login first");
                                return Redirect(Url.Content("~/"));


                            }
                            else
                            {
                                return Content("Employee Profile couldn't be completed");
                            }
                        }

                    }
                    else
                    {
                        return Content("You are trying to use a token that is already used !");
                    }

                    // return RedirectToAction("Index");
                }
                else
                {
                    return Content("token mismatch");
                }
            }
            else
            {
                return Content("Invitation Not Found!");
            }


        }
        [AllowAnonymous]
        [HttpPost]
        public ActionResult InvitationConfirm(int id, User user)
        {

            var tnc = Request.Params.Get("tnc");
            if (tnc != null && tnc == "on")
            {
                if (uService.GetSingleUserByEmail(user.Email) == null)
                {
                    if (uService.AddUser(user))
                    {
                        var Invitation = inService.GetSingleInvitation(id);
                        var emp = new Employee();
                        emp.UserId = user.Id;
                        emp.CompanyId = Invitation.CompanyId;
                        emp.DesignationId = Invitation.DesignationId;
                        if (eService.CreateEmployee(emp))
                        {
                            var accountSetting = new Settings();
                            accountSetting.userId = user.Id;
                            accountSetting.lgdash = false;
                            accountSetting.lglast = false;
                            accountSetting.lgcompany = true;
                            accountSetting.CompanyId = Invitation.CompanyId;

                            if (sService.AddSettings(accountSetting))
                            {
                                var userInRole = new UserInRole();
                                userInRole.EmployeeId = emp.Id;
                                userInRole.RoleId = Invitation.RoleId;
                                userInRole.IsActive = true;
                                if (URSer.AddUserInRole(userInRole))
                                {
                                    CustomPrincipal.Login(user.Email, user.Password, false);
                                    return RedirectToAction("Create", "EmployeeProfile", new { area = "OrganizationManagement" });
                                }
                            }
                        }
                        else
                        {
                            return Content("Failed");
                        }
                    }
                }
                else
                {
                    ModelState.AddModelError("Msg", "Already Register");
                    //return Content("Already Register");
                    return View();

                }
            }


            return Content("You Must Agree with our terms and Condition");
        }


        public bool sendMail(string to, string mailSubject, string mailBody)
        {
            var fromAddress = new MailAddress("sumon20@gmail.com", "HHASB ERP");
            var toAddress = new MailAddress(to, "Employee");
            string fromPassword = "638848";
            string subject = mailSubject;
            string body = mailBody;

            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
            };
            using (var message = new MailMessage(fromAddress, toAddress)
            {
                Subject = subject,
                Body = body,
                IsBodyHtml = true,

            })
            {
                smtp.Send(message);
            }

            return true;
        }
    }
}