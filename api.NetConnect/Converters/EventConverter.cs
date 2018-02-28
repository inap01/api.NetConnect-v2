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
        public static EventViewModelItem FromModel(this EventViewModelItem viewmodel, Event model)
        {
            viewmodel.ID = model.ID;
            viewmodel.Title = model.EventType.Name + " Vol." + model.Volume;
            viewmodel.Start = model.Start;
            viewmodel.End = model.End;
            viewmodel.Image = Properties.Settings.Default.imageAbsolutePath + model.Image;
            viewmodel.Description = model.EventType.Description;
            viewmodel.District = model.District;
            viewmodel.Street = model.Street;
            viewmodel.Housenumber = model.Housenumber;
            viewmodel.Postcode = model.Postcode;
            viewmodel.City = model.City;
            viewmodel.Price = model.ReservationCost;
            viewmodel.HasTournaments = model.Tournament.Count > 0;

            string routeLink = "https://www.google.com/maps?q=";
            routeLink += $"{model.Street}+";
            routeLink += $"{model.Housenumber}+";
            routeLink += $"{model.Postcode}+";
            routeLink += $"{model.City}+";
            routeLink += $"{model.District}";
            viewmodel.RouteLink = routeLink;

            Int32 seatsCount = 70 - model.Seat.Count(x => x.State == -1);
            Int32 flagged = model.Seat.Count(x => x.State == 1);
            Int32 reserved = model.Seat.Count(x => x.State == 2);
            Int32 free = seatsCount - flagged - reserved;

            viewmodel.Seating = new EventViewModelItem.SeatingReservation()
            {
                SeatsCount = seatsCount,
                Flagged = flagged,
                Reserved = reserved,
                Free = free
            };

            return viewmodel;
        }
        #endregion

        #region Backend
        public static List<BackendEventViewModelItem> FromModel(this List<BackendEventViewModelItem> viewmodel, List<Event> model)
        {
            foreach (var m in model)
                viewmodel.Add(new BackendEventViewModelItem().FromModel(m));

            return viewmodel;
        }

        public static BackendEventViewModelItem FromModel(this BackendEventViewModelItem viewmodel, Event model)
        {
            viewmodel.ID = model.ID;
            viewmodel.Name = model.EventType.Name + " Vol." + model.Volume;
            viewmodel.Volume = model.Volume;
            viewmodel.Image = model.Image;
            viewmodel.Start = model.Start;
            viewmodel.End = model.End;
            viewmodel.ReservationCost = model.ReservationCost;
            viewmodel.IsActiveReservation = model.IsActiveReservation;
            viewmodel.IsActiveCatering = model.IsActiveCatering;
            viewmodel.IsActiveFeedback = model.IsActiveFeedback;
            viewmodel.IsPrivate = model.IsPrivate;
            viewmodel.FeedbackLink = model.FeedbackLink;
            viewmodel.District = model.District;
            viewmodel.Street = model.Street;
            viewmodel.Housenumber = model.Housenumber;
            viewmodel.Postcode = model.Postcode;
            viewmodel.City = model.City;

            viewmodel.EventType.FromModel(model.EventType);

            return viewmodel;
        }

        public static Event ToModel(this BackendEventViewModelItem viewmodel)
        {
            Event model = new Event();

            model.ID = viewmodel.ID;
            model.EventTypeID = viewmodel.EventType.ID;
            model.Volume = viewmodel.Volume;
            model.Image = viewmodel.Image;
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