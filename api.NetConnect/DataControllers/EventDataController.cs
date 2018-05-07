using api.NetConnect.data.Entity;
using api.NetConnect.data.ViewModel;
using api.NetConnect.data.ViewModel.Event.Backend;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace api.NetConnect.DataControllers
{
    public class EventDataController : BaseDataController, IDataController<Event>
    {
        public EventDataController() : base()
        {

        }

        #region Basic Functions
        public Event GetItem(int ID)
        {
            var qry = db.Event.AsQueryable();
            qry = qry.Include(x => x.CateringOrder);
            qry = qry.Include(x => x.Seat);
            qry = qry.Include(x => x.Tournament);
            qry = qry.Include(x => x.EventType);

            qry = qry.Include(x => x.Tournament.Select(y => y.TournamentGame));
            //qry = qry.Include(x => x.Tournament.Select(y => y.Partner));
            //qry = qry.Include(x => x.Tournament.Select(y => y.TournamentParticipant.Select(z => z.User)));
            //qry = qry.Include(x => x.Tournament.Select(y => y.TournamentTeam.Select(z => z.TournamentTeamParticipant.Select(a => a.User))));
            //qry = qry.Include(x => x.Tournament.Select(y => y.TournamentWinner.Select(z => z.TournamentWinnerTeam)));
            //qry = qry.Include(x => x.Tournament.Select(y => y.TournamentWinner.Select(z => z.TournamentWinnerPlayer.User)));


            return qry.Single(x => x.ID == ID);
        }

        public IQueryable<Event> GetItems()
        {
            var qry = db.Event.AsQueryable();
            qry = qry.Include(x => x.CateringOrder);
            qry = qry.Include(x => x.Seat);
            qry = qry.Include(x => x.Tournament);
            qry = qry.Include(x => x.EventType);

            qry = qry.Include(x => x.Tournament.Select(y => y.TournamentGame));
            //qry = qry.Include(x => x.Tournament.Select(y => y.Partner));
            //qry = qry.Include(x => x.Tournament.Select(y => y.TournamentParticipant.Select(z => z.User)));
            //qry = qry.Include(x => x.Tournament.Select(y => y.TournamentTeam.Select(z => z.TournamentTeamParticipant.Select(a => a.User))));
            //qry = qry.Include(x => x.Tournament.Select(y => y.TournamentWinner.Select(z => z.TournamentWinnerTeam)));
            //qry = qry.Include(x => x.Tournament.Select(y => y.TournamentWinner.Select(z => z.TournamentWinnerPlayer.User)));

            return qry;
        }

        public Event Insert(Event item)
        {
            var result = db.Event.Add(item);
            db.SaveChanges();

            return result;
        }

        public Event Update(Event item)
        {
            var dbitem = db.Event.Single(x => x.ID == item.ID);

            dbitem.EventTypeID = item.EventTypeID;
            dbitem.Volume = item.Volume;
            dbitem.Image = item.Image;
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
        public void Delete(int ID)
        {
            db.Event.Remove(GetItem(ID));
            db.SaveChanges();
        }
        #endregion

        public List<Event> FilterList(ListArgsRequest<BackendEventFilter> args, out Int32 TotalCount)
        {
            var qry = GetItems();

            if(!String.IsNullOrEmpty(args.Filter.Name))
                qry = qry.Where(x => (x.EventType.Name + " Vol." + x.Volume).ToLower().Contains(args.Filter.Name.ToLower()));

            TotalCount = qry.Count();

            qry = qry.OrderByDescending(x => x.ID);
            var items = qry.Skip(args.Pagination.ItemsPerPageSelected * (args.Pagination.Page - 1))
                 .Take(args.Pagination.ItemsPerPageSelected)
                 .ToList();

            return items;
        }
    }
}