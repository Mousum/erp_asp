using Mhasb.Domain.Commons;
using Mhasb.Domain.Organizations;
using Mhasb.Domain.Users;
using Mhasb.Services.Commons;
using Mhasb.Services.Organizations;
using Mhasb.Services.Users;
using Mhasb.Wsit.CustomModel.Organizations;
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

        
        public ActionResult Create()
        {
            var user = uService.GetSingleUserByEmail(HttpContext.User.Identity.Name);
            ViewBag.FirstName = user.FirstName;
            ViewBag.LastName = user.LastName;
            EmployeeProfileCustom epc = iEP.GetEmployeeProfile(user.Id);
            if (epc != null)
                return View("Edit",epc);
            return View();
        }

        
        [HttpPost]
        public ActionResult Create(EmployeeProfileCustom employeeProfileCustom)
        {
            try
            {
                var user = uService.GetSingleUserByEmail(HttpContext.User.Identity.Name);
                HttpPostedFileBase profilePic = Request.Files["profile_pic"];
                //HttpPostedFileBase doc = Request.Files["documentLocation[]"];

                string profilePicName = "Employee_" + "_" + user.Id.ToString() + "_" + Path.GetRandomFileName() + ".png";
                string profilePicLocation = Server.MapPath("~/Uploads/EmployeeProfiles/");
                if (ImageUpload(profilePic, profilePicName, profilePicLocation))
                {
                    EmployeeProfile ep = new EmployeeProfile();
                    ep = employeeProfileCustom.employeeProfile;
                    ep.Users = new User { Id = user.Id, Email = user.Email };
                    ep.ImageLocation = "Uploads/EmployeeProfiles/" + profilePicName;
                    if (iEP.AddEmployeeProfile(ep))
                    {
                        try
                        {
                            ContactDetail phone = employeeProfileCustom.Phone;
                            phone.EmployeeProfileId = ep.Id;

                            ContactDetail fax = employeeProfileCustom.Fax;
                            fax.EmployeeProfileId = ep.Id;

                            ContactDetail facebook = employeeProfileCustom.Facebook;
                            facebook.EmployeeProfileId = ep.Id;

                            ContactDetail google = employeeProfileCustom.Google;
                            google.EmployeeProfileId = ep.Id;

                            ContactDetail linkedin = employeeProfileCustom.LinkedIn;
                            linkedin.EmployeeProfileId = ep.Id;

                            ContactDetail skype = employeeProfileCustom.Skype;
                            skype.EmployeeProfileId = ep.Id;

                            ContactDetail twitter = employeeProfileCustom.Twitter;
                            twitter.EmployeeProfileId = ep.Id;

                            ContactDetail website = employeeProfileCustom.Website;
                            website.EmployeeProfileId = ep.Id;

                            iCD.AddContactDetail(phone);
                            iCD.AddContactDetail(fax);
                            iCD.AddContactDetail(website);
                            iCD.AddContactDetail(facebook);
                            iCD.AddContactDetail(twitter);
                            iCD.AddContactDetail(google);
                            iCD.AddContactDetail(linkedin);
                            iCD.AddContactDetail(skype);
                        }
                        catch (Exception e)
                        {
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


        [AllowAnonymous]
        [HttpPost]
        public ActionResult Update(EmployeeProfileCustom employeeProfileCustom)
        {

            try
            {
                EmployeeProfile ep = new EmployeeProfile();
                ep = employeeProfileCustom.employeeProfile;
                HttpPostedFileBase profilePic = Request.Files["profile_pic"];
                var user = uService.GetSingleUserByEmail(HttpContext.User.Identity.Name);
                if(profilePic.ContentLength>0)
                {
                    string prevImage = Request.MapPath("~/" + ep.ImageLocation);
                    
                    string profilePicName = "Employee_" + "_" + user.Id.ToString() + "_" + Path.GetRandomFileName() + ".png";
                    string profilePicLocation = Server.MapPath("~/Uploads/EmployeeProfiles/");
                    if (ImageUpload(profilePic, profilePicName, profilePicLocation)) 
                    {
                        ep.ImageLocation = "Uploads/EmployeeProfiles/" + profilePicName;
                        if (System.IO.File.Exists(prevImage))
                        {
                            System.IO.File.Delete(prevImage);
                        }
                    }
                    else
                    {
                        TempData.Add("errMsg", "Photo Upload Unsuccessfull");
                        return RedirectToAction("Create", "EmployeeProfile", new { area = "OrganizationManagement" });
                      //  return Content("Photo Upload Unsuccessfull!!!...");
                    }
                    
                    
                }

                if (iEP.UpdateEmployeeProfile(ep))
                {
                    try
                    {
                        iCD.UpdateContactDetail(employeeProfileCustom.Phone);
                        iCD.UpdateContactDetail(employeeProfileCustom.Fax);
                        iCD.UpdateContactDetail(employeeProfileCustom.Website);
                        iCD.UpdateContactDetail(employeeProfileCustom.Facebook);
                        iCD.UpdateContactDetail(employeeProfileCustom.Twitter);
                        iCD.UpdateContactDetail(employeeProfileCustom.Google);
                        iCD.UpdateContactDetail(employeeProfileCustom.LinkedIn);
                        iCD.UpdateContactDetail(employeeProfileCustom.Skype);
                    }
                    catch (Exception ex)
                    {
                        TempData.Add("errMsg", "One or more Contact Field Updating Unsuccessfull");
                        return RedirectToAction("Create", "EmployeeProfile", new { area = "OrganizationManagement" });
                       // return Content("One or more Contact Field Updating Unsuccessfull!!!!");
                    }
                    
                }
                else
                {
                    TempData.Add("errMsg", "Profile Updating cannot done successfully");
                    return RedirectToAction("Create", "EmployeeProfile", new { area = "OrganizationManagement" });
                  //  return Content("Profile Updating cannot done successfully!!!!");
                }
            }
            catch (Exception ex)
            {
                TempData.Add("errMsg", "Failed");
                return RedirectToAction("Create", "EmployeeProfile", new { area = "OrganizationManagement" });
                //return Content("Failed");
            }

            TempData.Add("SucMasg", "Success");
            return RedirectToAction("Create", "EmployeeProfile", new { area = "OrganizationManagement" });
            //return Content("Success");
            //return RedirectToAction("MyMhasb", "Users", new { Area = "UserManagement" });
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