using Mhasb.Domain.Notifications;
using Mhasb.Services.Notifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace Mhasb.Wsit.Web.Areas.NotificationManagement.Controllers
{
    public class InvitationsController:Controller
    {
        private IInvitationService inService = new InvitationService();

        public ActionResult Index() 
        {
            var model = inService.GetAllInvitation();
            return View(model);
        }

        public ActionResult Create() 
        {
            var employeeType = Enum.GetValues(typeof(EmpTypeEnum))
                                    .Cast<EmpTypeEnum>()
                                    .Select(v => new { Id = Convert.ToInt32(v), Name = v.ToString() })
                                    .ToList();
            ViewBag.EmployeeType = new SelectList(employeeType, "Name", "Name");
            return View();
        }
        [HttpPost]
        public ActionResult Create(Invitation invitation) 
        {
           Random random = new Random();
            //string to = invitation.Email;
            string rand = random.Next().ToString();
            //string subject = "Invitation From MHASB Erp";
            //string body = "Hello Doly, ";
            //if (sendMail(to, subject, body))
            //{
               
                invitation.CompanyId = 1;//Static Company id , combo will be added later 
                invitation.SendDate = DateTime.Now;
                invitation.UpdateDate = DateTime.Now;
                invitation.Token = rand;
                invitation.Status = StatusEnum.test1;
                inService.CreateInvitation(invitation);
                
            //}
            return RedirectToAction("Index");
        }

        public ActionResult InvitationConfirm(int id,string token)
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
                        return RedirectToAction("Index");
                    }
                    else {
                        return Content("You are trying to use a token that is already used !");
                    }
                    
                   // return RedirectToAction("Index");
                }
                else 
                {
                    return Content("token mismatch");
                }
            }
            else {
                return Content("Invitation Not Found!");
            }
 

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
                Body = body
            })
            {
                smtp.Send(message);
            }

            return true;
        }
    }
}