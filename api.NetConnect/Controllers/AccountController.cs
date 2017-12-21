using api.NetConnect.Converters;
using api.NetConnect.data.ViewModel.Account;
using api.NetConnect.DataControllers;
using api.NetConnect.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web;
using System.Web.Http;
using static api.NetConnect.Helper.PasswordHelper;

namespace api.NetConnect.Controllers
{
    [Authorize()]
    public class AccountController : ApiController
    {
        #region Frontend
        [HttpGet]
        public IHttpActionResult Reservations()
        {
            AccountReservationViewModel viewmodel = new AccountReservationViewModel();
            viewmodel.Authenticated = UserHelper.Authenticated;

            try
            {
                viewmodel.Data.FromModel(UserDataController.GetItem(UserHelper.CurrentUserID));
            }
            catch(Exception ex)
            {
                viewmodel.Success = false;
                viewmodel.AddDangerAlert("Ein unerwarteter Fehler is aufgetreten.");
                viewmodel.AddDangerAlert(ExceptionHelper.FullException(ex));
            }

            return Ok(viewmodel);
        }

        [HttpGet]
        public IHttpActionResult Tournaments()
        {
            AccountTournamentViewModel viewmodel = new AccountTournamentViewModel();
            viewmodel.Authenticated = UserHelper.Authenticated;

            try
            {
                viewmodel.Data.FromModel(UserDataController.GetItem(UserHelper.CurrentUserID));
            }
            catch (Exception ex)
            {
                viewmodel.Success = false;
                viewmodel.AddDangerAlert("Ein unerwarteter Fehler is aufgetreten.");
                viewmodel.AddDangerAlert(ExceptionHelper.FullException(ex));
            }

            return Ok(viewmodel);
        }

        [HttpGet]
        public IHttpActionResult Edit()
        {
            AccountEditViewModel viewmodel = new AccountEditViewModel();
            viewmodel.Authenticated = UserHelper.Authenticated;

            try
            {
                viewmodel.Data.FromModel(UserDataController.GetItem(UserHelper.CurrentUserID));
            }
            catch (Exception ex)
            {
                viewmodel.Success = false;
                viewmodel.AddDangerAlert("Ein unerwarteter Fehler is aufgetreten.");
                viewmodel.AddDangerAlert(ExceptionHelper.FullException(ex));
            }

            return Ok(viewmodel);
        }

        [HttpPut]
        public IHttpActionResult Edit(AccountEditViewModelItem request)
        {
            AccountEditViewModel viewmodel = new AccountEditViewModel();
            viewmodel.Authenticated = UserHelper.Authenticated;

            try
            {
                var updateModel = UserDataController.GetItem(request.ID);
                updateModel.ToModel(request);

                if (request.OldPassword != null && request.NewPassword1 != null && request.NewPassword2 != null)
                    updateModel.Password = PasswordHelper.ChangePassword(request.ID, request.OldPassword, request.NewPassword1, request.NewPassword2);

                updateModel = UserDataController.Update(updateModel);
                viewmodel.Data.FromModel(updateModel);
            }
            catch (WrongPasswordException ex)
            {
                viewmodel.Success = false;
                viewmodel.AddWarningAlert(ExceptionHelper.FullException(ex));
            }
            catch (PasswordsNotEqualException ex)
            {
                viewmodel.Success = false;
                viewmodel.AddWarningAlert(ExceptionHelper.FullException(ex));
            }
            catch (Exception ex)
            {
                viewmodel.Success = false;
                viewmodel.AddDangerAlert("Ein unerwarteter Fehler is aufgetreten.");
                viewmodel.AddDangerAlert(ExceptionHelper.FullException(ex));
            }

            return Ok(viewmodel);
        }
        #endregion
    }
}
