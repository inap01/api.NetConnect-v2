using api.NetConnect.data.Entity;
using api.NetConnect.data.ViewModel;
using api.NetConnect.data.ViewModel.Seating;
using api.NetConnect.data.ViewModel.Seating.Backend;
using api.NetConnect.Helper;
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
        public static List<Seat> GetCurrentUserSeats(Int32 eventID)
        {
            Int32 userID = UserHelper.CurrentUserID;

            return GetByEvent(eventID).Where(x => x.UserID == userID).OrderBy(x => x.SeatNumber).ToList();
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

        public static Seat Insert(Seat item)
        {
            db = InitDB();

            var result = db.Seat.Add(item);
            db.SaveChanges();

            return result;
        }

        public static void Delete(Int32 ID)
        {
            db = InitDB();

            db.Seat.Remove(db.Seat.Single(x => x.ID == ID));
            db.SaveChanges();
        }
    }
}