using api.NetConnect.data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using api.NetConnect.data.ViewModel.Seating;

namespace api.NetConnect.Converters
{
    public static partial class ConverterExtensions
    {
        public static void FromModel(this SeatingViewModelItem viewModel, Seat model)
        {
            viewModel.ID = model.ID;
            viewModel.ReservationState = model.State;
            viewModel.ReservationDate = model.ReservationDate;
            viewModel.Description = model.Description;
            viewModel.IsPayed = model.Payed;
            viewModel.IsTeam = model.IsTeam;
            viewModel.User = null;

            if(model.IsActive && model.State != 0 && model.User != null)
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
        }
    }
}