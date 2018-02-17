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
    public class AccountController : BaseController
    {
        #region Frontend
        [HttpGet]
        public IHttpActionResult Reservations()
        {
            AccountReservationViewModel viewmodel = new AccountReservationViewModel();
            UserDataController dataCtrl = new UserDataController();

            try
            {
                viewmodel.Data.FromModel(dataCtrl.GetItem(UserHelper.CurrentUserID));
            }
            catch(Exception ex)
            {
                return Error(viewmodel, ex);
            }

            return Ok(viewmodel);
        }

        [HttpPut]
        public IHttpActionResult TransferReservation(TransferReservationRequest request)
        {
            BaseViewModel viewmodel = new BaseViewModel();
            UserDataController dataCtrl = new UserDataController();
            SeatDataController seatDataCtrl = new SeatDataController();

            try
            {
                Int32 TransferUserID;
                Seat seat = seatDataCtrl.GetItem(request.SeatID);
                try
                {
                    TransferUserID = dataCtrl.GetItems().Single(x => x.Email == request.Email).ID;
                }
                catch(Exception ex)
                {
                    return Warning(viewmodel, "Die Email wurde nicht vergeben.");
                }

                if(TransferUserID == UserHelper.CurrentUserID)
                {
                    return Warning(viewmodel, "Du kannst keine Tickets an dich selber versenden.");
                }

                if (seat.UserID != UserHelper.CurrentUserID)
                {
                    return Warning(viewmodel, "Du bist nicht Inhaber dieses Tickets.");
                }

                if(dataCtrl.ValidateUser(UserHelper.CurrentUserEmail, request.Password))
                {
                    seat.TransferUserID = TransferUserID;
                    seatDataCtrl.Update(seat);
                }
                else
                {
                    return Warning(viewmodel, "Das eingegebene Passwort stimmt nicht.");
                }
            }
            catch (Exception ex)
            {
                return Error(viewmodel, ex);
            }

            return Ok(viewmodel, "Ticket wurde versendet.");
        }

        [HttpPut]
        public IHttpActionResult AcceptTransfer(Int32 ID)
        {
            BaseViewModel viewmodel = new BaseViewModel();
            SeatDataController seatDataCtrl = new SeatDataController();

            try
            {
                Seat seat = seatDataCtrl.GetItem(ID);
                
                if(seat.TransferUserID == null)
                {
                    return Error(viewmodel, "Der Platz wurde nicht transferiert.");
                }

                if (seat.TransferUserID != UserHelper.CurrentUserID)
                {
                    return Warning(viewmodel, "Der Platz wurde dir nicht zugesendet.");
                }

                seat.UserID = seat.TransferUserID ?? default(int);
                seat.TransferUserID = null;
                seatDataCtrl.Update(seat);
            }
            catch (Exception ex)
            {
                return Error(viewmodel, ex);
            }

            return Ok(viewmodel, "Transfer wurde durchgeführt.");
        }

        [HttpPut]
        public IHttpActionResult RefuseTransfer(Int32 ID)
        {
            BaseViewModel viewmodel = new BaseViewModel();
            SeatDataController seatDataCtrl = new SeatDataController();

            try
            {
                Seat seat = seatDataCtrl.GetItem(ID);

                if (seat.TransferUserID == null)
                {
                    return Error(viewmodel, "Der Platz wurde nicht transferiert.");
                }

                if (seat.TransferUserID != UserHelper.CurrentUserID)
                {
                    return Warning(viewmodel, "Der Platz wurde dir nicht zugesendet.");
                }
                
                seat.TransferUserID = null;
                seatDataCtrl.Update(seat);
            }
            catch (Exception ex)
            {
                return Error(viewmodel, ex);
            }

            return Ok(viewmodel, "Transfer wurde abgelehnt.");
        }

        [HttpDelete]
        public IHttpActionResult CancelReservation(Int32 ID)
        {
            BaseViewModel viewmodel = new BaseViewModel();
            SeatDataController seatDataCtrl = new SeatDataController();

            try
            {
                seatDataCtrl.Delete(ID);
            }
            catch (Exception ex)
            {
                return Error(viewmodel, ex);
            }

            return Ok(viewmodel, "Stornierung war erfolgreich.");
        }

        [HttpGet]
        public IHttpActionResult Tournaments()
        {
            AccountTournamentViewModel viewmodel = new AccountTournamentViewModel();
            UserDataController dataCtrl = new UserDataController();

            try
            {
                viewmodel.Data.FromModel(dataCtrl.GetItem(UserHelper.CurrentUserID));
            }
            catch (Exception ex)
            {
                return Error(viewmodel, ex);
            }

            return Ok(viewmodel);
        }

        [HttpGet]
        public IHttpActionResult Edit()
        {
            AccountEditViewModel viewmodel = new AccountEditViewModel();
            UserDataController dataCtrl = new UserDataController();

            try
            {
                viewmodel.Data.FromModel(dataCtrl.GetItem(UserHelper.CurrentUserID));
            }
            catch (Exception ex)
            {
                return Error(viewmodel, ex);
            }

            return Ok(viewmodel);
        }

        [HttpPut]
        public IHttpActionResult Edit(AccountEditViewModelItem request)
        {
            AccountEditViewModel viewmodel = new AccountEditViewModel();
            UserDataController dataCtrl = new UserDataController();

            try
            {
                var updateModel = dataCtrl.GetItem(request.ID);
                updateModel.ToModel(request);

                if (request.OldPassword != null && request.NewPassword1 != null && request.NewPassword2 != null)
                {
                    updateModel.Password = PasswordHelper.ChangePassword(dataCtrl.GetItem(UserHelper.CurrentUserID), request.OldPassword, request.NewPassword1, request.NewPassword2);
                    viewmodel.AddSuccessMessage("Passwort wurde geändert.");
                }

                updateModel = dataCtrl.Update(updateModel);
                viewmodel.Data.FromModel(updateModel);
            }
            catch (WrongPasswordException ex)
            {
                return Warning(viewmodel, "Das eingegebene Passwort stimmt nicht.");
            }
            catch (PasswordsNotEqualException ex)
            {
                return Warning(viewmodel, "Die eingegebenen Passwörter stimmt nicht überein.");
            }
            catch (Exception ex)
            {
                return Error(viewmodel, ex);
            }

            return Ok(viewmodel, "Profil wurde aktualisiert.");
        }
        #endregion
    }
}
