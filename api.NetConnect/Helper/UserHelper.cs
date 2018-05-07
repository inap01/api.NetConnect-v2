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
                return getOwinUser().Identity.IsAuthenticated;
            }
        }
        public static Int32 CurrentUserID
        {
            get
            {
                var nameIdentifier = getOwinUser().Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier);
                if (nameIdentifier == null)
                    //throw new Exception("UserHelper: No ClaimTypes.NameIdentifier in ClaimsPrincipal");
                    return 0;

                return Convert.ToInt32(nameIdentifier.Value);
            }
        }
        public static String CurrentUserName
        {
            get
            {
                var name = getOwinUser().Claims.FirstOrDefault(x => x.Type == ClaimTypes.Name);
                return name.Value.ToString();
            }
        }
        public static String CurrentUserEmail
        {
            get
            {
                var email = getOwinUser().Claims.FirstOrDefault(x => x.Type == ClaimTypes.Email);
                return email.Value.ToString();
            }
        }
        public static UserRole CurrentUserRole
        {
            get
            {
                var role = getOwinUser().Claims.FirstOrDefault(x => x.Type == ClaimTypes.Role);
                return (UserRole)Enum.Parse(typeof(UserRole), role.Value.ToString());
            }
        }

        private static ClaimsPrincipal getOwinUser()
        {
            HttpContext context = HttpContext.Current;
            if (context == null)
                throw new Exception("UserHelper: HttpContext.Current is null");

            Microsoft.Owin.IOwinContext owinContext = HttpContext.Current.GetOwinContext();
            if (owinContext == null)
                throw new Exception("UserHelper: HttpContext.Current.GetOwinContext() is null");

            Microsoft.Owin.Security.IAuthenticationManager authManager = owinContext.Authentication;
            if (authManager == null)
                throw new Exception("UserHelper: HttpContext.Current.GetOwinContext().Authentication is null");

            ClaimsPrincipal user = authManager.User;
            if (user == null)
                throw new Exception("UserHelper: HttpContext.Current.GetOwinContext().Authentication.User is null");

            return HttpContext.Current.GetOwinContext().Authentication.User;
        }
    }
}