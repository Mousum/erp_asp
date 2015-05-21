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

namespace Mhasb.Wsit.Web.Areas.NotificationManagement.Controllers
{
    public class InvitationsController : Controller
    {
        private IInvitationService inService = new InvitationService();
        private readonly ICompanyService iCompany = new CompanyService();
        private IUserService uService = new UserService();
        private IEmployeeService eService = new EmployeeService();
        private IRoleService rService = new RoleService();
        private readonly ISettingsService sService = new SettingsService();
        private IDesignation degRep = new DesignationService();
        public ActionResult Index()
        {
            var model = inService.GetAllInvitation();
            return View(model);
        }

        public ActionResult Create()
        {

            var roles = rService.GetAllRoles();
            User user = uService.GetSingleUserByEmail(HttpContext.User.Identity.Name);
            List<Company> myCompanyList = iCompany.GetAllCompanies()
                                                   .Where(c => c.Users.Id == user.Id).ToList();
            var employeeType = Enum.GetValues(typeof(EmpTypeEnum))
                                    .Cast<EmpTypeEnum>()
                                    .Select(v => new { Id = Convert.ToInt32(v), Name = v.ToString() })
                                    .ToList();
            ViewBag.EmployeeType = new SelectList(employeeType, "Name", "Name");
            ViewBag.Companies = new SelectList(myCompanyList, "Id", "DisplayName");
            ViewBag.roles = new SelectList(roles, "Id", "RoleName");



            return View();
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

                string host = HttpContext.Request.Url.Host + ":2376/NotificationManagement/Invitations/InvitationConfirm/" + invitation.Id + "?token=" + invitation.Token;
                string msg = "<html><head><meta content=\"text/html; charset=utf-8\" /></head><body><p>Hello There"
                                            + ", </p><p>To verify your account, please click the following link:</p>"
                                            + "<p><a href=\"" + host + "\" target=\"_blank\">" + host
                                            + "</a></p><div>Best regards,</div><div>Someone</div><p>Do not forward "
                                            + "this email. The verify link is private.</p></body></html>";
                msg = HttpUtility.HtmlEncode(msg);



                string to = invitation.Email;
                string subject = "Invitation From MHASB Erp";
                string body = msg;
                sendMail(to, subject, body);

            }
            return RedirectToAction("Index");
        }


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
                        return View("InvitationConfirm");
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
                        
                        // get designation 
                        var degObj = degRep.GetDesignations().FirstOrDefault();
                        // if designation not exist then insert data into designation table
                        if (degObj == null)
                        {
                            var designation = new Designation {DesignationName ="Employee Type"};
                            degRep.AddDesignation(designation);

                            degObj = degRep.GetDesignations().FirstOrDefault();
                        }
                        emp.DesignationId = degObj.Id;

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
                                CustomPrincipal.Login(user.Email, user.Password, false);
                                return RedirectToAction("Create", "EmployeeProfile", new { area = "OrganizationManagement" });
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
                IsBodyHtml = true
            })
            {
                smtp.Send(message);
            }

            return true;
        }
    }
}