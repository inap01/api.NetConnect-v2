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
using api.NetConnect.data.ViewModel;

namespace api.NetConnect.Controllers
{
    public class AuthController : BaseController
    {
        [HttpPost]
        public IHttpActionResult Auth(LoginRequest request)
        {
            LoginViewModel viewmodel = new LoginViewModel();
            viewmodel.Authenticated = this.User.Identity.IsAuthenticated;
            UserDataController dataCtrl = new UserDataController();

            try
            {
                User u;

                if(dataCtrl.ValidateUser(request.Email, request.Password, out u))
                {
                    ClaimsIdentity identity = InitializeIdentity(u);

                    var authentication = HttpContext.Current.GetOwinContext().Authentication;
                    authentication.SignIn(new Microsoft.Owin.Security.AuthenticationProperties()
                    {
                        IsPersistent = true
                    }, identity);

                    viewmodel.Data.FromModel(u);

                    HttpContext.Current.Response.AddHeader("X-Redirect", Properties.Settings.Default.BaseAbosulteUrl + "/user/" + u.ID);
                }
                else
                {
                    viewmodel.Data = null;
                    return Warning(viewmodel, "Anmeldung fehlerhaft.");
                }
            }
            catch(Exception ex)
            {
                viewmodel.Data = null;
                return Error(viewmodel, ex, "Anmeldung fehlgeschlagen.");
            }

            return Ok(viewmodel, "Die Anmeldung war erfolgreich!");
        }

        private static ClaimsIdentity InitializeIdentity(User u)
        {
            ClaimsIdentity identity = new ClaimsIdentity("Application");

            Helper.UserRole Role = Helper.UserRole.User;
            if (u.IsAdmin)
                Role = Helper.UserRole.Admin;
            else if (u.IsTeam)
                Role = Helper.UserRole.Team;

            var userDetails = new Claim[]
            {
                new Claim(ClaimTypes.NameIdentifier, u.ID.ToString()),
                new Claim(ClaimTypes.Name, u.FirstName + " " + u.LastName),
                new Claim(ClaimTypes.Role, Role.ToString()),
                new Claim(ClaimTypes.Email, u.Email)
            };

            identity.AddClaims(userDetails);

            return identity;
        }

        [HttpPost]
        public IHttpActionResult Logout()
        {
            BaseViewModel viewmodel = new BaseViewModel();

            var authentication = HttpContext.Current.GetOwinContext().Authentication;
            authentication.SignOut();

            HttpContext.Current.Response.AddHeader("X-Redirect", Properties.Settings.Default.BaseAbosulteUrl);

            return Ok(viewmodel);
        }

        [HttpGet]
        public IHttpActionResult CheckLogin()
        {
            LoginViewModel viewmodel = new LoginViewModel();
            UserDataController dataCtrl = new UserDataController();

            if (viewmodel.Authenticated)
            {
                viewmodel.Data.FromModel(dataCtrl.GetItem(UserHelper.CurrentUserID));
            }

            return Ok(viewmodel);
        }

        [HttpPost]
        public IHttpActionResult Register(RegisterRequest request)
        {
            BaseViewModel viewmodel = new BaseViewModel();
            UserDataController dataCtrl = new UserDataController();

            try
            {
                Boolean checkEmail = dataCtrl.CheckExistingEmail(request.Email);
                Boolean checkNickname = dataCtrl.CheckExistingNickname(request.Nickname);
                if (checkEmail)
                {
                    return Warning(viewmodel, "Eingegebene Email wird bereits verwendet.");
                }
                else if (checkNickname)
                {
                    return Warning(viewmodel, "Eingegebener Nickname wird bereits verwendet.");
                }
                else
                {
                    if(request.Password1 == request.Password2)
                    {
                        String Salt;
                        String HashedPassword = PasswordHelper.CreatePassword(request.Password1, out Salt);
                        dataCtrl.Insert(request.ToModel(HashedPassword, Salt));
                    }
                    else
                    {
                        return Warning(viewmodel, "Die eingegebenen Passwörter stimmen nicht überein.");
                    }
                }
            }
            catch (Exception ex)
            {
                return Error(viewmodel, ex);
            }

            return Ok(viewmodel, "Registrierung erfolgreich. Du kannst dich nun einloggen.");
        }
    }
}
