﻿using api.NetConnect.data.Entity;
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
        public static BackendEventViewModelItem FromModel(this BackendEventViewModelItem viewModel, Event model)
        {
            viewModel.ID = model.ID;
            viewModel.Volume = model.Volume;
            viewModel.Image = "";
            viewModel.Start = model.Start;
            viewModel.End = model.End;

            viewModel.EventType.FromModel(model.EventType);

            return viewModel;
        }
    }

    public class EventConverter
    {
        public static List<BackendEventViewModelItem> FilterList(ListArgsRequest<BackendEventFilter> args, out Int32 TotalCount)
        {
            List<BackendEventViewModelItem> result = new List<BackendEventViewModelItem>();

            var items = EventDataController.GetItems();

            items = items.Where(x => (x.EventType.Name + " Vol." + x.Volume).ToLower().Contains(args.Filter.Name.ToLower())).ToList();

            TotalCount = items.Count;

            items = items.Skip(args.Pagination.ItemsPerPageSelected * (args.Pagination.Page - 1))
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