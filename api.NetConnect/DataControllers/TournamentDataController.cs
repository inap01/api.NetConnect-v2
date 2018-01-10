using api.NetConnect.data.Entity;
using api.NetConnect.Helper;
using System;
using System.Collections.Generic;
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
    }

    public class TournamentTeamDataController : GenericDataController<TournamentTeam>
    {
        public static TournamentTeam Update(TournamentTeam item)
        {
            TournamentTeam dbItem = GetItem(item.ID);

            dbItem.Name = item.Name;
            dbItem.TournamentID = item.TournamentID;
            dbItem.Password = item.Password;

            db.SaveChanges();

            return dbItem;
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
    }
}