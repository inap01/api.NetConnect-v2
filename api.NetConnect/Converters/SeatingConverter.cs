using api.NetConnect.data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using api.NetConnect.data.ViewModel.Seating;
using api.NetConnect.data.ViewModel;
using api.NetConnect.DataControllers;
using api.NetConnect.data.ViewModel.Seating.Backend;
using api.NetConnect.data.ViewModel.User.Backend;

namespace api.NetConnect.Converters
{
    using SeatingBackendListViewModel = ListArgsViewModel<SeatingViewModelItem, BackendSeatingFilter>;
    using SeatingArgsRequest = ListArgsRequest<BackendSeatingFilter>;

    public static partial class ConverterExtensions
    {
        #region Fronend
        public static SeatingViewModelItem FromModel(this SeatingViewModelItem viewmodel, Seat model)
        {
            viewmodel.ID = model.ID;
            viewmodel.SeatNumber = model.SeatNumber;
            viewmodel.ReservationState = model.State;
            viewmodel.ReservationDate = model.ReservationDate;
            viewmodel.Description = model.Description;
            viewmodel.IsPayed = model.Payed;
            viewmodel.User = null;
            viewmodel.TransferUser = null;

            if (model.IsActive && model.State != 0 && model.User != null)
            {
                viewmodel.User = new SeatingViewModelItem.SeatingUser()
                {
                    ID = model.UserID,
                    FirstName = model.User.FirstName,
                    LastName = model.User.LastName,
                    Nickname = model.User.Nickname,
                    Email = model.User.Email
                };
            }

            if (model.IsActive && model.State != 0 && model.TransferUser != null)
            {
                viewmodel.User = new SeatingViewModelItem.SeatingUser()
                {
                    ID = model.TransferUserID ?? default(int),
                    FirstName = model.TransferUser.FirstName,
                    LastName = model.TransferUser.LastName,
                    Nickname = model.TransferUser.Nickname,
                    Email = model.TransferUser.Email
                };
            }

            return viewmodel;
        }
        #endregion

        #region Backend
        public static BackendSeatingViewModelItem FromModel(this BackendSeatingViewModelItem viewmodel, Seat model)
        {
            viewmodel.ID = model.ID;
            viewmodel.SeatNumber = model.SeatNumber;
            viewmodel.ReservationState = new BackendSeatingStatusOption(model.State);
            viewmodel.ReservationDate = model.ReservationDate;
            viewmodel.Description = model.Description;
            viewmodel.IsPayed = model.Payed;
            viewmodel.User = null;
            viewmodel.TransferUser = null;
            viewmodel.Event.FromModel(model.Event);

            if (model.IsActive && model.State != 0 && model.User != null)
            {
                viewmodel.User = new BackendUserViewModelItem();
                viewmodel.User.FromModel(model.User);
            }

            if (model.IsActive && model.State != 0 && model.TransferUser != null)
            {
                viewmodel.TransferUser = new BackendUserViewModelItem();
                viewmodel.TransferUser.FromModel(model.TransferUser);
            }

            return viewmodel;
        }

        public static Seat ToModel(this BackendSeatingViewModelItem viewmodel)
        {
            Seat model = new Seat();

            model.SeatNumber = viewmodel.SeatNumber;
            model.EventID = viewmodel.Event.ID;
            model.UserID = viewmodel.User.ID;
            if(viewmodel.TransferUser != null)
                model.TransferUserID = viewmodel.TransferUser.ID;
            model.State = viewmodel.ReservationState.Key;
            model.Payed = viewmodel.IsPayed;
            model.ReservationDate = viewmodel.ReservationDate;
            model.Description = viewmodel.Description;
            model.IsActive = true;

            return model;
        }
        #endregion
    }
}