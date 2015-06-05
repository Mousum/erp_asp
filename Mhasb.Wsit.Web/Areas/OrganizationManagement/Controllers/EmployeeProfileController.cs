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
    public class EmployeeProfileController : Controller
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
            string msg="", profilePicErr = "",contactDetailErr="";
            bool success = false;
            try
            {
                var user = uService.GetSingleUserByEmail(HttpContext.User.Identity.Name);
                HttpPostedFileBase profilePic = Request.Files["profile_pic"];
                //HttpPostedFileBase doc = Request.Files["documentLocation[]"];

                string profilePicName = "Employee" + "_" + user.Id.ToString() + "_" + Path.GetRandomFileName() + ".png";
                string profilePicLocation = Server.MapPath("~/Uploads/EmployeeProfiles/");
                EmployeeProfile ep = new EmployeeProfile();
                ep = employeeProfileCustom.employeeProfile;
                ep.Users = new User { Id = user.Id, Email = user.Email };
                if (profilePic.ContentLength > 1) 
                {
                    if (ImageUpload(profilePic, profilePicName, profilePicLocation))
                    {
                        ep.ImageLocation = "Uploads/EmployeeProfiles/" + profilePicName;
                    }
                    else
                        profilePicErr = "Picture Upload Unsuccessfull!!!";
                }


                if (iEP.AddEmployeeProfile(ep))
                {
                    success = true;
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

                        if (!(iCD.AddContactDetail(phone) && iCD.AddContactDetail(fax) && iCD.AddContactDetail(website) && iCD.AddContactDetail(facebook) && iCD.AddContactDetail(twitter) && iCD.AddContactDetail(google) && iCD.AddContactDetail(linkedin) && iCD.AddContactDetail(skype)))
                            contactDetailErr = "One or more Contact Details could not added successfully..";
                    }
                    catch (Exception e)
                    {
                        var tt = e;
                        contactDetailErr = "One or more Contact Details could not added successfully..";
                    }
                    msg = "Success";

                }
                else
                    msg = "Profile could not Added Successfully....";


            }
            catch (Exception ex)
            {
                msg = "Profile could not Added Successfully....";
            }
            TempData.Add("errMsg",msg+profilePicErr+contactDetailErr);
            if (!success)
                return RedirectToAction("Create");

            return RedirectToAction("MyMhasb", "Users", new { Area = "UserManagement" });
        }


        [AllowAnonymous]
        [HttpPost]
        public ActionResult Update(EmployeeProfileCustom employeeProfileCustom)
        {
            string msg="", profilePicErr = "",contactDetailErr="";
            bool success = false;
            try
            {
                EmployeeProfile ep = new EmployeeProfile();
                ep = employeeProfileCustom.employeeProfile;
                HttpPostedFileBase profilePic = Request.Files["profile_pic"];
                var user = uService.GetSingleUserByEmail(HttpContext.User.Identity.Name);
                string profilePicName;
                string profilePicLocation;
                string tempPath = Server.MapPath("~/Uploads/Temp");
                if(profilePic.ContentLength>0)
                {
                    if (!Directory.Exists(tempPath))
                    {
                        Directory.CreateDirectory(tempPath);
                    }

                    if (ep.ImageLocation== null)
                    {
                        profilePicName =  "Employee" + "_" + user.Id.ToString() + "_" + Path.GetRandomFileName() + ".png";
                        profilePicLocation = Server.MapPath("~/Uploads/EmployeeProfiles/");
                    }
                    else
                    {
                        profilePicName = ep.ImageLocation.Split('/').Last();
                        profilePicLocation = ep.ImageLocation;
                        profilePicLocation = Path.GetDirectoryName(profilePicLocation);
                        profilePicLocation = Server.MapPath("~/" + profilePicLocation + "/");
                        if (System.IO.File.Exists(profilePicLocation+profilePic))
                        {
                            System.IO.File.Move(profilePicLocation + profilePic, tempPath + profilePic);
                        }
                    }
                    
                    
                    if (ImageUpload(profilePic, profilePicName, profilePicLocation)) 
                    {
                        ep.ImageLocation = "Uploads/EmployeeProfiles/" + profilePicName;
                        if (System.IO.File.Exists(tempPath+profilePicName))
                        {
                            System.IO.File.Delete(tempPath+profilePicName);
                        }
                    }
                    else
                    {
                        if (System.IO.File.Exists(tempPath + profilePic))
                        {
                            System.IO.File.Move(tempPath + profilePic, profilePicLocation + profilePic);
                        }
                        profilePicErr = "New Image could not uploaded Successfully....";
                    }
                    
                    
                }

                if (iEP.UpdateEmployeeProfile(ep))
                {
                    success = true;
                    try
                    {
                        if (!(iCD.UpdateContactDetail(employeeProfileCustom.Phone) && iCD.UpdateContactDetail(employeeProfileCustom.Fax) && iCD.UpdateContactDetail(employeeProfileCustom.Website) && iCD.UpdateContactDetail(employeeProfileCustom.Facebook) && iCD.UpdateContactDetail(employeeProfileCustom.Twitter) && iCD.UpdateContactDetail(employeeProfileCustom.Google) && iCD.UpdateContactDetail(employeeProfileCustom.LinkedIn) && iCD.UpdateContactDetail(employeeProfileCustom.Skype)))
                            contactDetailErr="One or more Contact Field Updating Unsuccessfull!!!";


                    }
                    catch (Exception ex)
                    {
                        contactDetailErr = "One or more Contact Field Updating Unsuccessfull!!!!";
                    }

                    msg = "Your profile is successfully Updated";
                    
                }
                else
                {
                    //if (System.IO.File.Exists(tempPath + profilePic))
                    //{
                    //    System.IO.File.Move(tempPath + profilePic, profilePicLocation + profilePic);
                    //}
                    msg="Profile Updating cannot done successfully";
                }
            }
            catch (Exception ex)
            {
                msg = "Profile Updating cannot done successfully";
            }

            TempData.Add("errMsg",msg+profilePicErr+contactDetailErr);
            if (!success)
                return RedirectToAction("Update");

            return RedirectToAction("MyMhasb", "Users", new { Area = "UserManagement" });

        }




        public bool ImageUpload(HttpPostedFileBase file, string fileName, string filePath)
        {
            try
            {

                string uploadPath = filePath;
                bool isValid = false;
                string[] fileExtensions = { ".bmp", ".jpg", ".png", ".jpeg",".JPG"};
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