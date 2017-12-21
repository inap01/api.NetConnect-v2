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
        public static Int32 CurrentUserID {
            get
            {
                var nameIdentifier = HttpContext.Current.GetOwinContext().Authentication.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier);
                return Convert.ToInt32(nameIdentifier.Value);
            }
        }
    }
}