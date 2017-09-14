using api.NetConnect.data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
namespace api.NetConnect.ViewModels.Seating
{
    public static class SeatingConverterExtensions
    {
        public static Seating.SeatingViewModelItem ToViewModel(this Seat model)
        {
            return new SeatingViewModelItem()
            {
                ID = model.ID
                
            };
        }
    }
}