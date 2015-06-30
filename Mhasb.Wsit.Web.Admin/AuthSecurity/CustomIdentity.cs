using System;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using Mhasb.Services.AdminUsers;

namespace Mhasb.Wsit.Web.Admin.AuthSecurity
{
    public class CustomIdentity : ICustomIdentity
    {
        /// <summary>
        /// Authenticate and get identity out with roles
        /// </summary>
        /// <param name="userName">User name</param>
        /// <param name="password">Password</param>
        /// <returns>Instance of identity</returns>
        public static CustomIdentity GetCustomIdentity(string email, string password)
        {
            var identity = new CustomIdentity();
            var uServcice = new AdminUserService();

            bool loginResponse = uServcice.AdminLogin(email, password);


            if (loginResponse)
            {
                identity.IsAuthenticated = true;
                identity.Name = email;
                var roles = new[] { "admin" };
                identity.Roles = roles;

                return identity;
            }
            return identity;
        }

        private CustomIdentity()
        {
        }

        public string AuthenticationType
        {
            get { return "Custom"; }
        }

        public bool IsAuthenticated { get; private set; }

        public string Name { get; private set; }

        private string[] Roles { get; set; }

        public bool IsInRole(string role)
        {
            if (string.IsNullOrEmpty(role))
            {
                throw new ArgumentException("Role is null");
            }
            return Roles.Any(one => one.ToUpper().Trim() == role.ToUpper().Trim());
        }

        /// <summary>
        /// Create serialized string for storing in a cookie
        /// </summary>
        /// <returns>String representation of identity</returns>
        public string ToJson()
        {
            string returnValue;
            var representation = new IdentityRepresentation()
            {
                IsAuthenticated = this.IsAuthenticated,
                Name = this.Name,
                Roles = string.Join("|", this.Roles)
            };
            var jsonSerializer = new DataContractJsonSerializer(typeof(IdentityRepresentation));
            using (var stream = new MemoryStream())
            {
                jsonSerializer.WriteObject(stream, representation);
                stream.Flush();
                byte[] json = stream.ToArray();
                returnValue = Encoding.UTF8.GetString(json, 0, json.Length);
            }

            return returnValue;
        }

        /// <summary>
        /// Create identity from a cookie data
        /// </summary>
        /// <param name="cookieString">String stored in cookie, created via ToJson method</param>
        /// <returns>Instance of identity</returns>
        public static ICustomIdentity FromJson(string cookieString)
        {

            IdentityRepresentation serializedIdentity;
            using (var stream = new MemoryStream(Encoding.UTF8.GetBytes(cookieString)))
            {
                var jsonSerializer = new DataContractJsonSerializer(typeof(IdentityRepresentation));
                serializedIdentity = jsonSerializer.ReadObject(stream) as IdentityRepresentation;
            }
            var identity = new CustomIdentity()
            {
                IsAuthenticated = serializedIdentity.IsAuthenticated,
                Name = serializedIdentity.Name,
                Roles = serializedIdentity.Roles
                    .Split(new[] { "|" }, StringSplitOptions.RemoveEmptyEntries)
            };
            return identity;
        }
    }
}