using api.NetConnect.data.Entity;
using api.NetConnect.data.ViewModel;
using api.NetConnect.data.ViewModel.Tournament.Backend;
using api.NetConnect.Helper;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace api.NetConnect.DataControllers
{
    public class TournamentDataController : GenericDataController<Tournament>
    {
        public static List<Tournament> GetByEvent(Int32 eventID)
        {
            db = InitDB();

            return db.Tournament.Where(x => x.EventID == eventID).OrderBy(x => x.Start).ToList();
        }

        public static List<Tournament> FilterList(ListArgsRequest<BackendTournamentFilter> args, out Int32 TotalCount)
        {
            db = InitDB();

            var qry = db.Tournament.AsQueryable();
            qry.Include("TournamentGame");
            qry.Include("Event");
            qry.Include("Partner");

            qry = qry.Where(x => x.EventID == args.Filter.EventSelected.ID);
            if (args.Filter.GameSelected.ID != -1)
                qry = qry.Where(x => x.TournamentGameID == args.Filter.GameSelected.ID);

            TotalCount = qry.Count();

            qry = qry.OrderByDescending(x => x.EventID);
            qry = qry.Skip(args.Pagination.ItemsPerPageSelected * (args.Pagination.Page - 1))
                 .Take(args.Pagination.ItemsPerPageSelected);

            return qry.ToList();
        }

        public static Tournament Insert(Tournament item)
        {
            InitDB();

            var result = db.Tournament.Add(item);
            db.SaveChanges();

            db.Entry(result).Reference(c => c.Event).Load();
            db.Entry(result).Reference(c => c.TournamentGame).Load();

            return result;
        }

        public static Tournament Update(Tournament item)
        {
            Tournament dbItem = GetItem(item.ID);
            
            dbItem.TournamentGameID = item.TournamentGameID;
            dbItem.TeamSize = item.TeamSize;
            dbItem.ChallongeLink = item.ChallongeLink;
            dbItem.Mode = item.Mode;
            dbItem.Start = item.Start;
            dbItem.End = item.End;
            dbItem.PartnerID = item.PartnerID;

            db.SaveChanges();

            return dbItem;
        }
    }

    public class TournamentGameDataController : GenericDataController<TournamentGame>
    {
        public static TournamentGame Update(TournamentGame item)
        {
            TournamentGame dbItem = GetItem(item.ID);

            dbItem.Name = item.Name;
            dbItem.Rules = item.Rules;

            db.SaveChanges();

            return dbItem;
        }
    }

    public class TournamentParticipantDataController : GenericDataController<TournamentParticipant>
    {
        public static TournamentParticipant GetByTournament(Int32 TournamentID)
        {
            Int32 UserID = UserHelper.CurrentUserID;

            InitDB();

            var result = db.TournamentParticipant.FirstOrDefault(x => x.TournamentID == TournamentID && x.UserID == UserID);

            return result;
        }
        public static TournamentParticipant Insert(TournamentParticipant item)
        {
            InitDB();

            var result = db.TournamentParticipant.Add(item);
            db.SaveChanges();

            return result;
        }

        public static void Delete(Int32 TournamentID)
        {
            InitDB();

            var item = db.TournamentParticipant.Single(x => x.TournamentID == TournamentID && x.UserID == UserHelper.CurrentUserID);
            db.TournamentParticipant.Remove(item);
            db.SaveChanges();
        }
    }

    public class TournamentTeamDataController : GenericDataController<TournamentTeam>
    {
        public static TournamentTeam Insert(TournamentTeam item)
        {
            InitDB();

            var result = db.TournamentTeam.Add(item);
            db.SaveChanges();

            return result;
        }
    }

    public class TournamentTeamParticipantDataController : GenericDataController<TournamentTeamParticipant>
    {
        public static TournamentTeamParticipant GetByTournament(Int32 TournamentID)
        {
            Int32 UserID = UserHelper.CurrentUserID;

            InitDB();

            var result = db.TournamentTeamParticipant.FirstOrDefault(x => x.TournamentTeam.TournamentID == TournamentID && x.UserID == UserID);

            return result;
        }
        public static TournamentTeamParticipant Insert(TournamentTeamParticipant item)
        {
            InitDB();

            var result = db.TournamentTeamParticipant.Add(item);
            db.SaveChanges();

            return result;
        }
        public static void Delete(Int32 TournamentID)
        {
            InitDB();

            var item = db.TournamentTeamParticipant.Single(x => x.TournamentTeam.TournamentID == TournamentID && x.UserID == UserHelper.CurrentUserID);
            db.TournamentTeamParticipant.Remove(item);
            db.SaveChanges();
        }
    }
}