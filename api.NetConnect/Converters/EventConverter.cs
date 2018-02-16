using api.NetConnect.data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using api.NetConnect.data.ViewModel.Event;
using api.NetConnect.data.ViewModel.Event.Backend;
using api.NetConnect.data.ViewModel;
using api.NetConnect.DataControllers;

namespace api.NetConnect.Converters
{
    public static partial class ConverterExtensions
    {
        #region Frontend
        public static EventViewModelItem FromModel(this EventViewModelItem viewModel, Event model)
        {
            viewModel.ID = model.ID;
            viewModel.Title = model.EventType.Name + " Vol." + model.Volume;
            viewModel.Start = model.Start;
            viewModel.End = model.End;
            viewModel.Image = "http://lan-netconnect.de/_api/images/gallery/8/__preview.jpg";
            viewModel.Description = model.EventType.Description;
            viewModel.District = model.District;
            viewModel.Street = model.Street;
            viewModel.Housenumber = model.Housenumber;
            viewModel.Postcode = model.Postcode;
            viewModel.City = model.City;
            viewModel.RouteLink = "https://www.google.com/maps?ll=51.00048,6.282984&z=16&t=m&hl=de&gl=US&mapclient=embed&q=Hauptstra%C3%9Fe+93+52441+Linnich+Deutschland";
            viewModel.Price = model.ReservationCost;
            viewModel.HasTournaments = model.Tournament.Count > 0;

            Int32 seatsCount = 70 - model.Seat.Count(x => x.State == -1);
            Int32 flagged = model.Seat.Count(x => x.State == 1);
            Int32 reserved = model.Seat.Count(x => x.State == 2);
            Int32 free = seatsCount - flagged - reserved;

            viewModel.Seating = new EventViewModelItem.SeatingReservation()
            {
                SeatsCount = seatsCount,
                Flagged = flagged,
                Reserved = reserved,
                Free = free
            };

            return viewModel;
        }
        #endregion

        #region Backend
        public static List<BackendEventViewModelItem> FromModel(this List<BackendEventViewModelItem> viewModel, List<Event> model)
        {
            foreach (var m in model)
                viewModel.Add(new BackendEventViewModelItem().FromModel(m));

            return viewModel;
        }

        public static BackendEventViewModelItem FromModel(this BackendEventViewModelItem viewModel, Event model)
        {
            viewModel.ID = model.ID;
            viewModel.Name = model.EventType.Name + " Vol." + model.Volume;
            viewModel.Volume = model.Volume;
            viewModel.Image = "";
            viewModel.Start = model.Start;
            viewModel.End = model.End;
            viewModel.ReservationCost = model.ReservationCost;
            viewModel.IsActiveReservation = model.IsActiveReservation;
            viewModel.IsActiveCatering = model.IsActiveCatering;
            viewModel.IsActiveFeedback = model.IsActiveFeedback;
            viewModel.IsPrivate = model.IsPrivate;
            viewModel.FeedbackLink = model.FeedbackLink;
            viewModel.District = model.District;
            viewModel.Street = model.Street;
            viewModel.Housenumber = model.Housenumber;
            viewModel.Postcode = model.Postcode;
            viewModel.City = model.City;

            viewModel.EventType.FromModel(model.EventType);

            return viewModel;
        }

        public static Event ToModel(this BackendEventViewModelItem viewmodel)
        {
            Event model = new Event();

            model.ID = viewmodel.ID;
            model.EventTypeID = viewmodel.EventType.ID;
            model.Volume = viewmodel.Volume;
            //model.ImageContainerID = viewmodel;
            model.Start = viewmodel.Start;
            model.End = viewmodel.End;
            model.ReservationCost = viewmodel.ReservationCost;
            model.IsActiveReservation = viewmodel.IsActiveReservation;
            model.IsActiveCatering = viewmodel.IsActiveCatering;
            model.IsActiveFeedback = viewmodel.IsActiveFeedback;
            model.IsPrivate = viewmodel.IsPrivate;
            model.FeedbackLink = viewmodel.FeedbackLink;
            model.District = viewmodel.District;
            model.Street = viewmodel.Street;
            model.Housenumber = viewmodel.Housenumber;
            model.Postcode = viewmodel.Postcode;
            model.City = viewmodel.City;

            return model;
        }
        #endregion
    }
}