using api.NetConnect.data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using api.NetConnect.data.ViewModel.Event;
using api.NetConnect.data.ViewModel.Event.Backend;

namespace api.NetConnect.Converters
{
    public static partial class ConverterExtensions
    {
        public static BackendEventViewModelItem FromModel(this BackendEventViewModelItem viewModel, Event model)
        {
            viewModel.ID = model.ID;
            viewModel.Title = model.EventType.Name + " Vol. " + model.Volume;
            viewModel.Image = "";
            viewModel.Start = model.Start;
            viewModel.End = model.End;

            return viewModel;
        }
    }
}