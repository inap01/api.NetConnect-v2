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
    public class AuthController : ApiController
    {
        [HttpPost]
        public IHttpActionResult Auth(LoginRequest request)
        {
            LoginViewModel viewmodel = new LoginViewModel();
            viewmodel.Authenticated = this.User.Identity.IsAuthenticated;

            try
            {
                User u;

                if(UserDataController.ValidateUser(request.Email, request.Password, out u))
                {
                    ClaimsIdentity identity = InitializeIdentity(u);

                    var authentication = HttpContext.Current.GetOwinContext().Authentication;
                    authentication.SignIn(new Microsoft.Owin.Security.AuthenticationProperties()
                    {
                        IsPersistent = true
                    }, identity);

                    viewmodel.Data.FromModel(u);
                    viewmodel.AddSuccessAlert("Die Anmeldung war erfolgreich!");

                    HttpContext.Current.Response.AddHeader("X-Redirect", Properties.Settings.Default.BaseAbosulteUrl + "/user/" + u.ID);
                }
                else
                {
                    viewmodel.Success = false;
                    viewmodel.Data = null;
                    viewmodel.AddWarningAlert("Anmeldung fehlerhaft.");
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
            viewmodel.Authenticated = UserHelper.Authenticated;
            
            if (viewmodel.Authenticated)
            {
                viewmodel.Data.FromModel(UserDataController.GetItem(UserHelper.CurrentUserID));
            }

            return Ok(viewmodel);
        }

        [HttpPost]
        public IHttpActionResult Register(RegisterRequest request)
        {
            BaseViewModel viewmodel = new BaseViewModel();
            viewmodel.Authenticated = UserHelper.Authenticated;

            try
            {
                Boolean checkEmail = UserDataController.CheckExistingEmail(request.Email);
                Boolean checkNickname = UserDataController.CheckExistingNickname(request.Nickname);
                if (checkEmail)
                {
                    viewmodel.Success = false;
                    viewmodel.AddWarningAlert("Eingegebene Email wird bereits verwendet.");
                }
                else if (checkNickname)
                {
                    viewmodel.Success = false;
                    viewmodel.AddWarningAlert("Eingegebener Nickname wird bereits verwendet.");
                }
                else
                {
                    if(request.Password1 == request.Password2)
                    {
                        String Salt;
                        String HashedPassword = PasswordHelper.CreatePassword(request.Password1, out Salt);
                        UserDataController.Insert(request.ToModel(HashedPassword, Salt));
                        viewmodel.AddSuccessAlert("Registrierung erfolgreich. Du kannst dich nun einloggen.");
                    }
                    else
                    {
                        viewmodel.Success = false;
                        viewmodel.AddWarningAlert("Die eingegebenen Passwörter stimmen nicht überein.");
                    }
                }
            }
            catch (Exception ex)
            {
                viewmodel.Success = false;
                viewmodel.AddDangerAlert("Ein unerwarteter Fehler is aufgetreten.");
                viewmodel.AddDangerAlert(ExceptionHelper.FullException(ex));
            }

            return Ok(viewmodel);
        }
    }
}
