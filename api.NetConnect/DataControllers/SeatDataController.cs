using api.NetConnect.data.Entity;
using api.NetConnect.data.ViewModel;
using api.NetConnect.data.ViewModel.Seating;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace api.NetConnect.DataControllers
{
    using SeatingArgsRequest = ListArgsRequest<SeatingFilter, SeatingSortSettings>;

    public class SeatDataController : GenericDataController<Seat>
    {
        public static List<Seat> FilterList(SeatingArgsRequest args)
        {
            List<Seat> result = new List<Seat>();

            var seats = db.Seat.AsQueryable();

            if(args.Filter.Status != SeatingStatusFilter.Ungefiltert)
            {
                if (args.Filter.Status == SeatingStatusFilter.Frei)
                    seats = seats.Where(x => x.State == 0);
                else if (args.Filter.Status == SeatingStatusFilter.Vorgemerkt)
                    seats = seats.Where(x => x.State == 1);
                else if (args.Filter.Status == SeatingStatusFilter.Reserviert)
                    seats = seats.Where(x => x.State == 2);
            }

            foreach(var seat in seats)
            {
                String name = seat.User.FirstName + " " + seat.User.LastName;
                String usageString = "Netconnect-" + seat.UserID + "-" + seat.ID;

                if (seat.ID.ToString().IndexOf(args.Filter.SeatNumber) != -1 &&
                    name.ToLower().IndexOf(args.Filter.Name.ToLower()) != -1 && 
                    usageString.ToLower().IndexOf(args.Filter.UsageString.ToLower()) != -1)
                {
                    result.Add(seat);
                }
            }


            return result;
        }

        public static Seat Update (Seat item)
        {
            Seat dbItem = GetItem(item.ID);

            dbItem.UserID = item.UserID;
            dbItem.State = item.State;
            dbItem.Description = item.Description;
            dbItem.ReservationDate = item.ReservationDate;
            dbItem.Payed = item.Payed;
            dbItem.IsTeam = item.IsTeam;

            db.SaveChanges();

            return dbItem;
        }
    }
}