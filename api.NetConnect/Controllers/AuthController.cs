using api.NetConnect.DataControllers;
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
using api.NetConnect.data.Entity;
using api.NetConnect.data.ViewModel.Auth;

namespace api.NetConnect.Controllers
{
    public class AuthController : ApiController
    {
        [HttpGet]
        public IHttpActionResult Auth()
        {
            LoginViewModel viewmodel = new LoginViewModel();

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
        public IHttpActionResult Auth(LoginRequest request)
        {
            LoginViewModel viewmodel = new LoginViewModel();

            try
            {
                User u;

                if(UserDataController.ValidateUser(request.Email, request.Password, out u))
                {
                    ClaimsIdentity identity = InitializeIdentity(u.ID, u.FirstName + u.LastName, "Admin");

                    var authentication = HttpContext.Current.GetOwinContext().Authentication;
                    authentication.SignIn(new Microsoft.Owin.Security.AuthenticationProperties()
                    {
                        IsPersistent = true
                    }, identity);

                    viewmodel.Data.FromModel(u);
                    viewmodel.AddSuccessAlert("Die Anmeldung war erfolgreich!");
                }
            }
            catch(Exception ex)
            {
                viewmodel.Success = false;
                viewmodel.Data = null;
                viewmodel.AddDangerAlert("Anmeldung fehlgeschlagen.");
                viewmodel.AddDangerAlert(ExceptionHelper.FullException(ex));
            }

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
}
