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
            viewModel.Seating = new EventViewModelItem.SeatingReservation()
            {
                SeatsCount = 40,
                Free = 15,
                Flagged = 5,
                Reserved = 20
            };

            return viewModel;
        }
        #endregion

        #region Backend
        public static BackendEventViewModelItem FromModel(this BackendEventViewModelItem viewModel, Event model)
        {
            viewModel.ID = model.ID;
            viewModel.Name = model.EventType.Name + " Vol." + model.Volume;
            viewModel.Volume = model.Volume;
            viewModel.Image = "";
            viewModel.Start = model.Start;
            viewModel.End = model.End;

            viewModel.EventType.FromModel(model.EventType);

            return viewModel;
        }
        #endregion
    }

    public class EventConverter
    {
        public static List<BackendEventViewModelItem> FilterList(ListArgsRequest<BackendEventFilter> args, out Int32 TotalCount)
        {
            List<BackendEventViewModelItem> result = new List<BackendEventViewModelItem>();

            var events = EventDataController.GetItems();

            events = events.Where(x => (x.EventType.Name + " Vol." + x.Volume).ToLower().Contains(args.Filter.Name.ToLower())).ToList();

            TotalCount = events.Count();

            var items = events.Skip(args.Pagination.ItemsPerPageSelected * (args.Pagination.Page - 1))
                 .Take(args.Pagination.ItemsPerPageSelected)
                 .ToList();

            foreach (var model in items)
            {
                BackendEventViewModelItem item = new BackendEventViewModelItem();
                item.FromModel(model);
                result.Add(item);
            }

            return result;
        }
    }
}