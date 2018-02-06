using api.NetConnect.data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace api.NetConnect.DataControllers
{
    public class EventDataController : GenericDataController<Event>
    {
        public static Event Insert(Event item)
        {
            InitDB();

            var result = db.Event.Add(item);
            db.SaveChanges();

            return result;
        }
        public static Event Update(Event item)
        {
            InitDB();

            var dbitem = db.Event.Single(x => x.ID == item.ID);

            dbitem.EventTypeID = item.EventTypeID;
            dbitem.Volume = item.Volume;
            //dbitem.ImageContainerID = item.ImageContainerID;
            dbitem.Start = item.Start;
            dbitem.End = item.End;
            dbitem.ReservationCost = item.ReservationCost;
            dbitem.IsActiveReservation = item.IsActiveReservation;
            dbitem.IsActiveCatering = item.IsActiveCatering;
            dbitem.IsActiveFeedback = item.IsActiveFeedback;
            dbitem.IsPrivate = item.IsPrivate;
            dbitem.FeedbackLink = item.FeedbackLink;
            dbitem.District = item.District;
            dbitem.Street = item.Street;
            dbitem.Housenumber = item.Housenumber;
            dbitem.Postcode = item.Postcode;
            dbitem.City = item.City;

            db.SaveChanges();

            return dbitem;
        }
    }
}