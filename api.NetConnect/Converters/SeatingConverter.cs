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
        public static SeatingViewModelItem FromModel(this SeatingViewModelItem viewModel, Seat model)
        {
            viewModel.ID = model.ID;
            viewModel.SeatNumber = model.SeatNumber;
            viewModel.ReservationState = model.State;
            viewModel.ReservationDate = model.ReservationDate;
            viewModel.Description = model.Description;
            viewModel.IsPayed = model.Payed;
            viewModel.User = null;
            viewModel.TransferUser = null;

            if (model.IsActive && model.State != 0 && model.User != null)
            {
                viewModel.User = new SeatingViewModelItem.SeatingUser()
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
                viewModel.User = new SeatingViewModelItem.SeatingUser()
                {
                    ID = model.TransferUserID ?? default(int),
                    FirstName = model.TransferUser.FirstName,
                    LastName = model.TransferUser.LastName,
                    Nickname = model.TransferUser.Nickname,
                    Email = model.TransferUser.Email
                };
            }

            return viewModel;
        }
        #endregion

        #region Backend
        public static BackendSeatingViewModelItem FromModel(this BackendSeatingViewModelItem viewModel, Seat model)
        {
            viewModel.ID = model.ID;
            viewModel.SeatNumber = model.SeatNumber;
            viewModel.ReservationState = new BackendSeatingStatusOption(model.State);
            viewModel.ReservationDate = model.ReservationDate;
            viewModel.Description = model.Description;
            viewModel.IsPayed = model.Payed;
            viewModel.User = null;
            viewModel.TransferUser = null;
            viewModel.Event.FromModel(model.Event);

            if (model.IsActive && model.State != 0 && model.User != null)
            {
                viewModel.User = new BackendUserViewModelItem();
                viewModel.User.FromModel(model.User);
            }

            if (model.IsActive && model.State != 0 && model.TransferUser != null)
            {
                viewModel.TransferUser = new BackendUserViewModelItem();
                viewModel.TransferUser.FromModel(model.TransferUser);
            }

            return viewModel;
        }
        #endregion
    }
}