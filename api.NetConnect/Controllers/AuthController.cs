using api.NetConnect.DataControllers;
using api.NetConnect.data.ViewModel.Profile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using api.NetConnect.Converters;
using api.NetConnect.Helper;
using System.Security.Claims;
using System.Web;
using Microsoft.Owin;
using Owin;

namespace api.NetConnect.Controllers
{
    public class AuthController : ApiController
    {
        [HttpGet]
        public IHttpActionResult Auth()
        {
            AuthViewModel viewmodel = new AuthViewModel();

            Int32 id = 1;

            ClaimsIdentity identity = InitializeIdentity(id, "Marius Hartmann", "Admin");

            var authentication = HttpContext.Current.GetOwinContext().Authentication;
            authentication.SignIn(new Microsoft.Owin.Security.AuthenticationProperties()
            {
                IsPersistent = true
            }, identity);

            return Ok(viewmodel);
        }

        [HttpPost]
        public IHttpActionResult Auth(AuthViewModel request)
        {
            AuthViewModel viewmodel = new AuthViewModel();

            return Ok(viewmodel);
        }

        private static ClaimsIdentity InitializeIdentity(Int32 id, String name, String role)
        {
            ClaimsIdentity identity = new ClaimsIdentity("Application");

            var userDetails = new Claim[]
            {
                new Claim(ClaimTypes.Name, id.ToString()),
                new Claim(ClaimTypes.NameIdentifier, name),
                new Claim(ClaimTypes.Role, role)
            };

            identity.AddClaims(userDetails);

            return identity;
        }

        [HttpPost]
        public IHttpActionResult Logout()
        {
            ClearAuthentication();

            return Ok();
        }

        private static void ClearAuthentication()
        {
            HttpRequest req = HttpContext.Current.Request;
            HttpResponse res = HttpContext.Current.Response;
            HttpCookieCollection reqCookie = req.Cookies;

            req.Cookies.AllKeys.Where(c => c.EndsWith("Authentication")).ToList().ForEach(c =>
            {
                if (String.Equals(reqCookie[c].Name, Properties.Settings.Default.AuthCookieName, StringComparison.InvariantCultureIgnoreCase))
                    reqCookie[c].Domain = Properties.Settings.Default.AuthCookieName;

                reqCookie[c].Expires = DateTime.ParseExact("1970-01-01", "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture).ToLocalTime();
                res.SetCookie(reqCookie[c]);
            });
        }
    }

    public class AuthViewModel
    {
    }
}
