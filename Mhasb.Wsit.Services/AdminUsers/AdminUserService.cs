﻿using Mhasb.Wsit.DAL.Operations;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mhasb.Domain.AdminUsers;
using Mhasb.Wsit.CustomModel.Organizations;
using Mhasb.Wsit.DAL.Data;
using Mhasb.Wsit.Domain;

namespace Mhasb.Services.AdminUsers
{
    public class AdminUserService : IAdminUserServices
    {
        private readonly CrudOperation<AdminUser> adRep = new CrudOperation<AdminUser>();
        public bool AdminLogin(string email, string password)
        {
            //var userObj = userRep.GetOperation()
            //                      .Filter(u => u.Email == email)
            //                      .GetIQueryable();

            var userObj = adRep.GetOperation()
                                  .Filter(u => u.Email == email)
                                  .Get().FirstOrDefault();


            if (userObj != null)
            {
                if (userObj.Password == password)
                    return true;
                else
                    return false;
            }
            else
            {
                return false;
            }

        }

        public bool AddAdminUser(AdminUser admin)
        {
            try
            {
                admin.State = ObjectState.Added;
                adRep.AddOperation(admin);
                return true;
            }
            catch (Exception e) 
            {
               var msg = e.Message;
               return false;
            }

        }
        public bool UpdateAdminUser(AdminUser admin) 
        {
            try
            {
                admin.State = ObjectState.Modified;
                adRep.UpdateOperation(admin);
                return true;
            }
            catch (Exception e)
            {
                var msg = e.Message;
                return false;
            }
        }
        public bool DeleteAdminUser(int adminUserId) 
        {
            try
            {
                adRep.DeleteOperation(adminUserId);
                return true;
            }
            catch (Exception e)
            {
                var msg = e.Message;
                return false;
            }
        }
        public List<AdminUser> GetAllAdminUsers() 
        {
            try
            {
                var voObj = adRep.GetOperation()
                                        .Get().ToList();

                return voObj;
            }
            catch (Exception ex)
            {
                var rr = ex.Message;
                return null;
            }
        }
        public AdminUser GetSingleAdminUser(int userId) 
        {
            try
            {
                var voObj = adRep.GetOperation()
                                        .Filter(ad => ad.Id == userId)
                                        .Get().SingleOrDefault();

                return voObj;
            }
            catch (Exception ex)
            {
                var rr = ex.Message;
                return null;
            }
        }

        public string InsertDefaultData()
        {
            using (var context = new WsDbContext())
            {

                const string query = "EXEC spset_general_data_entry";
                var rr = context.Database.ExecuteSqlCommand(query);

                if (rr > 0)
                    return "Default Data Added Successfully!";
                return "Default Data Already Inserted,No need to do again!";
            }
            
        }

    }
}
