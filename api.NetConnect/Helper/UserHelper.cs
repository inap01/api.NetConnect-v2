using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;

namespace api.NetConnect.Helper
{
    public class UserHelper
    {
        public static Boolean Authenticated
        {
            get
            {
                return HttpContext.Current.GetOwinContext().Authentication.User.Identity.IsAuthenticated;
            }
        }
        public static Int32 CurrentUserID
        {
            get
            {
                var nameIdentifier = HttpContext.Current.GetOwinContext().Authentication.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier);
                return Convert.ToInt32(nameIdentifier.Value);
            }
        }
        public static String CurrentUserName
        {
            get
            {
                var name = HttpContext.Current.GetOwinContext().Authentication.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Name);
                return name.Value.ToString();
            }
        }
        public static String CurrentUserEmail
        {
            get
            {
                var email = HttpContext.Current.GetOwinContext().Authentication.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Email);
                return email.Value.ToString();
            }
        }
        public static UserRole CurrentUserRole
        {
            get
            {
                var role = HttpContext.Current.GetOwinContext().Authentication.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Role);
                return (UserRole)Enum.Parse(typeof(UserRole), role.Value.ToString());
            }
        }
    }
}