using api.NetConnect.data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace api.NetConnect.DataControllers
{
    public class SeatDataController : GenericDataController<Seat>
    {
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