using api.NetConnect.Converters;
using api.NetConnect.data.Entity;
using api.NetConnect.data.ViewModel;
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

        [HttpPut]
        public IHttpActionResult TransferReservation(TransferReservationRequest request)
        {
            BaseViewModel viewmodel = new BaseViewModel();

            try
            {
                Int32 TransferUserID;
                Seat seat = SeatDataController.GetItem(request.SeatID);
                try
                {
                    TransferUserID = UserDataController.GetItem(request.Email, "Email").ID;
                }
                catch(Exception ex)
                {
                    viewmodel.Success = false;
                    viewmodel.AddWarningAlert("Die Email wurde nicht vergeben.");
                    return Ok(viewmodel);
                }

                if(TransferUserID == UserHelper.CurrentUserID)
                {
                    viewmodel.Success = false;
                    viewmodel.AddWarningAlert("Du kannst keine Tickets an dich selber versenden.");
                    return Ok(viewmodel);
                }

                if (seat.UserID != UserHelper.CurrentUserID)
                {
                    viewmodel.Success = false;
                    viewmodel.AddWarningAlert("Du bist nicht Inhaber dieses Tickets.");
                    return Ok(viewmodel);
                }

                if(UserDataController.ValidateUser(UserHelper.CurrentUserEmail, request.Password))
                {
                    seat.TransferUserID = TransferUserID;
                    SeatDataController.Update(seat);
                }
                else
                {
                    viewmodel.Success = false;
                    viewmodel.AddWarningAlert("Das eingegebene Passwort stimmt nicht.");
                    return Ok(viewmodel);
                }

                viewmodel.AddSuccessAlert("Ticket wurde versendet.");
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
        public IHttpActionResult AcceptTransfer(Int32 ID)
        {
            BaseViewModel viewmodel = new BaseViewModel();

            try
            {
                Seat seat = SeatDataController.GetItem(ID);
                
                if(seat.TransferUserID == null)
                {
                    viewmodel.Success = false;
                    viewmodel.AddDangerAlert("Der Platz wurde nicht transferiert.");
                }

                if (seat.TransferUserID != UserHelper.CurrentUserID)
                {
                    viewmodel.Success = false;
                    viewmodel.AddWarningAlert("Der Platz wurde dir nicht zugesendet.");
                }


                seat.UserID = seat.TransferUserID ?? default(int);
                seat.TransferUserID = null;
                SeatDataController.Update(seat);

                viewmodel.AddSuccessAlert("Transfer wurde durchgeführt.");
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
        public IHttpActionResult RefuseTransfer(Int32 ID)
        {
            BaseViewModel viewmodel = new BaseViewModel();

            try
            {
                Seat seat = SeatDataController.GetItem(ID);

                if (seat.TransferUserID == null)
                {
                    viewmodel.Success = false;
                    viewmodel.AddDangerAlert("Der Platz wurde nicht transferiert.");
                }

                if (seat.TransferUserID != UserHelper.CurrentUserID)
                {
                    viewmodel.Success = false;
                    viewmodel.AddWarningAlert("Der Platz wurde dir nicht zugesendet.");
                }

                
                seat.TransferUserID = null;
                SeatDataController.Update(seat);

                viewmodel.AddSuccessAlert("Transfer wurde abgelehnt.");
            }
            catch (Exception ex)
            {
                viewmodel.Success = false;
                viewmodel.AddDangerAlert("Ein unerwarteter Fehler is aufgetreten.");
                viewmodel.AddDangerAlert(ExceptionHelper.FullException(ex));
            }

            return Ok(viewmodel);
        }

        [HttpDelete]
        public IHttpActionResult CancelReservation(Int32 ID)
        {
            BaseViewModel viewmodel = new BaseViewModel();

            try
            {
                SeatDataController.Delete(ID);

                viewmodel.AddSuccessAlert("Stornierung war erfolgreich.");
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
                    updateModel.Password = PasswordHelper.ChangePassword(UserDataController.GetItem(UserHelper.CurrentUserID), request.OldPassword, request.NewPassword1, request.NewPassword2);

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
