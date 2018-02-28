using api.NetConnect.data.Entity;
using api.NetConnect.data.ViewModel;
using api.NetConnect.data.ViewModel.Seating;
using api.NetConnect.data.ViewModel.Seating.Backend;
using api.NetConnect.Helper;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace api.NetConnect.DataControllers
{
    using SeatingArgsRequest = ListArgsRequest<BackendSeatingFilter>;

    public class SeatDataController : BaseDataController, IDataController<Seat>
    {
        public SeatDataController() : base()
        {

        }

        #region Basic Functions
        public Seat GetItem(int ID)
        {
            var qry = db.Seat.AsQueryable();
            qry.Include(x => x.Event);
            qry.Include(x => x.User);
            qry.Include(x => x.TransferUser);
            qry.Include(x => x.SeatTransferLog);
            qry.Include(x => x.CateringOrder);

            return qry.Single(x => x.ID == ID);
        }

        public IQueryable<Seat> GetItems()
        {
            var qry = db.Seat.AsQueryable();
            qry.Include(x => x.Event);
            qry.Include(x => x.User);
            qry.Include(x => x.TransferUser);
            qry.Include(x => x.SeatTransferLog);
            qry.Include(x => x.CateringOrder);

            return qry;
        }

        public Seat Insert(Seat item)
        {
            var result = db.Seat.Add(item);
            db.SaveChanges();

            db.Entry(result).Reference(c => c.Event).Load();
            db.Entry(result).Reference(c => c.User).Load();
            db.Entry(result).Reference(c => c.TransferUser).Load();

            return result;
        }

        public Seat Update(Seat item)
        {
            var dbItem = GetItem(item.ID);

            dbItem.SeatNumber = item.SeatNumber;
            dbItem.EventID = item.EventID;
            dbItem.UserID = item.UserID;
            dbItem.State = item.State;
            dbItem.Description = item.Description;
            dbItem.ReservationDate = item.ReservationDate;
            dbItem.Payed = item.Payed;
            dbItem.TransferUserID = item.TransferUserID;
            dbItem.IsActive = item.IsActive;

            db.SaveChanges();

            return dbItem;
        }

        public void Delete(int ID)
        {
            db.Seat.Remove(GetItem(ID));
            db.SaveChanges();
        }
        #endregion

        public List<Seat> FilterList(ListArgsRequest<BackendSeatingFilter> args)
        {
            var qry = GetItems();
            
            qry = qry.Where(x => x.EventID == args.Filter.EventSelected.ID);

            qry = qry.OrderByDescending(x => x.ID);

            return qry.ToList();
        }

        public List<Seat> GetCurrentUserSeats(Int32 eventID)
        {
            Int32 userID = UserHelper.CurrentUserID;

            return GetItems().Where(x => x.EventID == eventID && x.UserID == userID).OrderBy(x => x.SeatNumber).ToList();
        }

        public Seat GetItem(Int32 seatNumber, Int32 eventID)
        {
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
    }
}