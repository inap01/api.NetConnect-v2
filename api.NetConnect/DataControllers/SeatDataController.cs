using api.NetConnect.data.Entity;
using api.NetConnect.data.ViewModel;
using api.NetConnect.data.ViewModel.Seating;
using api.NetConnect.data.ViewModel.Seating.Backend;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace api.NetConnect.DataControllers
{
    using SeatingArgsRequest = ListArgsRequest<BackendSeatingFilter>;

    public class SeatDataController : GenericDataController<Seat>
    {
        public static List<Seat> GetByEvent(Int32 eventID)
        {
            db = InitDB();

            return db.Seat.Where(x => x.EventID == eventID).ToList();
        }

        public static Seat GetItem(Int32 seatNumber, Int32 eventID)
        {
            db = InitDB();

            var seat = db.Seat.FirstOrDefault(x => x.SeatNumber == seatNumber && x.EventID == eventID);
            if (seat != null)
                return seat;

            return new Seat()
            {
                SeatNumber = seatNumber,
                State = 0,
                Payed = false
            };
        }

        public static Seat Update (Seat item)
        {
            Seat dbItem = GetItem(item.ID);

            dbItem.UserID = item.UserID;
            dbItem.State = item.State;
            dbItem.Description = item.Description;
            dbItem.ReservationDate = item.ReservationDate;
            dbItem.Payed = item.Payed;

            db.SaveChanges();

            return dbItem;
        }
    }
}