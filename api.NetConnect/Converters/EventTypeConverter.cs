using api.NetConnect.data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using api.NetConnect.data.ViewModel.Event;
using api.NetConnect.data.ViewModel.Event.Backend;
using api.NetConnect.data.ViewModel;
using api.NetConnect.DataControllers;
using api.NetConnect.data.ViewModel.EventType.Backend;

namespace api.NetConnect.Converters
{
    public static partial class ConverterExtensions
    {

        public static BackendEventTypeViewModelItem FromModel(this BackendEventTypeViewModelItem viewModel, EventType model)
        {
            viewModel.ID = model.ID;
            viewModel.Name = model.Name;

            return viewModel;
        }
    }

    public class EventTypeConverter
    {
        public static List<BackendEventTypeViewModelItem> FilterList(ListArgsRequest<BackendEventTypeFilter> args, out Int32 TotalCount)
        {
            List<BackendEventTypeViewModelItem> result = new List<BackendEventTypeViewModelItem>();

            var events = EventTypeDataController.GetItems();

            var items = events.Where(x => x.Name.ToLower().Contains(args.Filter.Name.ToLower())).ToList();

            TotalCount = items.Count;

            items = items.Skip(args.Pagination.ItemsPerPageSelected * (args.Pagination.Page - 1))
                 .Take(args.Pagination.ItemsPerPageSelected)
                 .ToList();

            foreach (var model in items)
            {
                BackendEventTypeViewModelItem item = new BackendEventTypeViewModelItem();
                item.FromModel(model);
                result.Add(item);
            }

            return result;
        }
    }
}