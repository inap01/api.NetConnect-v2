using api.NetConnect.data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using api.NetConnect.data.ViewModel.Info;

namespace api.NetConnect.Converters
{
    public static partial class ConverterExtensions
    {
        public static void FromModel(this InfoViewModelItem viewModel, Settings model)
        {
            viewModel.ReservationCost = model.ReservationCost;
            viewModel.Location = "Körrenzig";
            viewModel.Street = "Hauptstraße 91";
            viewModel.Postcode = "52441 Linnich";
            viewModel.RouteLink = "https://www.google.com/maps?ll=51.00048,6.282984&z=16&t=m&hl=de&gl=US&mapclient=embed&q=Hauptstra%C3%9Fe+93+52441+Linnich+Deutschland";
            viewModel.Start = model.Start;
            viewModel.End = model.End;
            viewModel.MinAge = 18;
        }
    }
}