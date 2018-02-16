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
        public static List<BackendEventTypeViewModelItem> FromModel(this List<BackendEventTypeViewModelItem> viewModel, List<EventType> model)
        {
            viewModel = model.ConvertAll(x =>
            {
                return new BackendEventTypeViewModelItem().FromModel(x);
            });

            return viewModel;
        }

        public static BackendEventTypeViewModelItem FromModel(this BackendEventTypeViewModelItem viewModel, EventType model)
        {
            viewModel.ID = model.ID;
            viewModel.Name = model.Name;

            return viewModel;
        }
    }
}