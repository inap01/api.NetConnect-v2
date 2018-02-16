using api.NetConnect.data.Entity;
using api.NetConnect.Helper;
using System;
using System.Data.Entity;
using System.Linq;

namespace api.NetConnect.DataControllers
{
    public class TournamentTeamParticipantDataController : BaseDataController, IDataController<TournamentTeamParticipant>
    {
        public TournamentTeamParticipantDataController() : base()
        {

        }

        #region Basic Functions
        public TournamentTeamParticipant GetItem(int ID)
        {
            var qry = db.TournamentTeamParticipant.AsQueryable();
            qry.Include(x => x.TournamentTeam);
            qry.Include(x => x.User);

            return qry.Single(x => x.ID == ID);
        }

        public IQueryable<TournamentTeamParticipant> GetItems()
        {
            var qry = db.TournamentTeamParticipant.AsQueryable();
            qry.Include(x => x.TournamentTeam);
            qry.Include(x => x.User);

            return qry;
        }

        public TournamentTeamParticipant Insert(TournamentTeamParticipant item)
        {
            var result = db.TournamentTeamParticipant.Add(item);
            db.SaveChanges();

            return result;
        }

        public TournamentTeamParticipant Update(TournamentTeamParticipant item)
        {
            throw new NotImplementedException();
        }

        public void Delete(int ID)
        {
            db.TournamentTeamParticipant.Remove(GetItem(ID));
            db.SaveChanges();
        }
        #endregion

        public TournamentTeamParticipant GetItemByTournament(Int32 TournamentID)
        {
            Int32 UserID = UserHelper.CurrentUserID;

            var result = db.TournamentTeamParticipant.FirstOrDefault(x => x.TournamentTeam.TournamentID == TournamentID && x.UserID == UserID);

            return result;
        }

        public void DeleteByTournament(Int32 TournamentID)
        {
            Int32 UserID = UserHelper.CurrentUserID;

            var item = db.TournamentTeamParticipant.Single(x => x.TournamentTeam.TournamentID == TournamentID && x.UserID == UserID);
            db.TournamentTeamParticipant.Remove(item);
            db.SaveChanges();
        }
    }
}