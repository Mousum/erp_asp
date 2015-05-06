using Mhasb.Domain.Commons;
using Mhasb.Domain.Organizations;
using Mhasb.Domain.Users;
using Mhasb.Services.Commons;
using Mhasb.Services.Organizations;
using Mhasb.Services.Users;
using Mhasb.Wsit.Web.Areas.OrganizationManagement.Models;
using Mhasb.Wsit.Web.Controllers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace Mhasb.Wsit.Web.Areas.OrganizationManagement.Controllers
{
    public class EmployeeProfileController : BaseController
    {
        //
        // GET: /OrganizationManagement/EmployeeProfile/
        //public ActionResult Index()
        //{
        //    //return View();
        //}

        private readonly IEmployeeProfile iEP = new EmployeeProfileService();
        private readonly IContactDetail iCD = new ContactDetailService();
        private readonly IUserService uService = new UserService();

        [AllowAnonymous]
        public ActionResult Create()
        {
            var user = uService.GetSingleUserByEmail(HttpContext.User.Identity.Name);
            ViewBag.FirstName = user.FirstName;
            ViewBag.LastName = user.LastName;
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult Create(EmployeeProfileCustom employeeProfileCustom)
        {
            try
            {
                var user = uService.GetSingleUserByEmail(HttpContext.User.Identity.Name);
                HttpPostedFileBase profilePic = Request.Files["profile_pic"];
                //HttpPostedFileBase doc = Request.Files["documentLocation[]"];

                string profilePicName = "Employee_" + "_"+user.Id.ToString() +"_" + Path.GetRandomFileName() + ".png";
                string profilePicLocation = Server.MapPath("~/Uploads/EmployeeProfiles/");
                if (ImageUpload(profilePic, profilePicName, profilePicLocation))
                {
                    EmployeeProfile ep = new EmployeeProfile();
                    ep = employeeProfileCustom.employeeProfile;
                    ep.Users = new User { Id = user.Id, Email = user.Email };
                    ep.ImageLocation = "Uploads/EmployeeProfiles/"+profilePicName;
                    if (iEP.AddEmployeeProfile(ep))
                    {
                        try {
                            iCD.AddContactDetail(employeeProfileCustom.Phone);
                            iCD.AddContactDetail(employeeProfileCustom.Fax);
                            iCD.AddContactDetail(employeeProfileCustom.Website);
                            iCD.AddContactDetail(employeeProfileCustom.Facebook);
                            iCD.AddContactDetail(employeeProfileCustom.Twitter);
                            iCD.AddContactDetail(employeeProfileCustom.Google);
                            iCD.AddContactDetail(employeeProfileCustom.LinkedIn);
                            iCD.AddContactDetail(employeeProfileCustom.Skype);
                        }
                        catch (Exception e) {
                            var tt = e;
                        }
                        
                    }
                }
      
                
            }
            catch (Exception ex)
            {

            }
            
            //return Content("sdffs");
            return RedirectToAction("MyMhasb", "Users", new { Area = "UserManagement" });
        }

        public bool ImageUpload(HttpPostedFileBase file, string fileName, string filePath)
        {
            try
            {

                string uploadPath = filePath;
                bool isValid = false;
                string[] fileExtensions = { ".bmp", ".jpg", ".png", ".jpeg"};
                for (int i = 0; i < fileExtensions.Length; i++)
                {

                    if (file.FileName.Contains(fileExtensions[i]))
                    {

                        isValid = true;
                        break;
                    }
                }

                if (isValid)
                {
                    if (!Directory.Exists(uploadPath))
                    {
                        Directory.CreateDirectory(uploadPath);
                    }
                    file.SaveAs(uploadPath + fileName);


                    return true;
                    //System.IO.File.Move(uploadPath + file.FileName, uploadPath + fileName + ".png");
                    //return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception e)
            {
                return false;
            }
        }

	}
}