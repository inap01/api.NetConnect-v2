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
        public static List<BackendEventTypeViewModelItem> FromModel(this List<BackendEventTypeViewModelItem> viewmodel, List<EventType> model)
        {
            foreach (var m in model)
                viewmodel.Add(new BackendEventTypeViewModelItem().FromModel(m));

            return viewmodel;
        }

        public static BackendEventTypeViewModelItem FromModel(this BackendEventTypeViewModelItem viewmodel, EventType model)
        {
            viewmodel.ID = model.ID;
            viewmodel.Name = model.Name;
            viewmodel.Description = model.Description;

            return viewmodel;
        }

        public static EventType ToModel(this BackendEventTypeViewModelItem viewmodel)
        {
            EventType model = new EventType();

            model.ID = viewmodel.ID;
            model.Name = viewmodel.Name;
            model.Description = viewmodel.Description;

            return model;
        }
    }
}