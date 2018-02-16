using api.NetConnect.data.Entity;
using api.NetConnect.Helper;
using System;
using System.Data.Entity;
using System.Linq;

namespace api.NetConnect.DataControllers
{
    public class TournamentParticipantDataController : BaseDataController, IDataController<TournamentParticipant>
    {
        public TournamentParticipantDataController() : base()
        {

        }

        #region Basic Functions
        public TournamentParticipant GetItem(int ID)
        {
            var qry = db.TournamentParticipant.AsQueryable();
            qry.Include(x => x.Tournament);
            qry.Include(x => x.User);

            return qry.Single(x => x.ID == ID);
        }

        public IQueryable<TournamentParticipant> GetItems()
        {
            var qry = db.TournamentParticipant.AsQueryable();
            qry.Include(x => x.Tournament);
            qry.Include(x => x.User);

            return qry;
        }

        public TournamentParticipant Insert(TournamentParticipant item)
        {
            var result = db.TournamentParticipant.Add(item);
            db.SaveChanges();

            return result;
        }

        public TournamentParticipant Update(TournamentParticipant item)
        {
            throw new NotImplementedException();
        }

        public void Delete(int ID)
        {
            db.TournamentParticipant.Remove(GetItem(ID));
            db.SaveChanges();
        }
        #endregion

        public TournamentParticipant GetItemByTournament(Int32 TournamentID)
        {
            Int32 UserID = UserHelper.CurrentUserID;
            
            var result = db.TournamentParticipant.FirstOrDefault(x => x.TournamentID == TournamentID && x.UserID == UserID);

            return result;
        }

        public void DeleteByTournament(Int32 TournamentID)
        {
            var item = db.TournamentParticipant.Single(x => x.TournamentID == TournamentID && x.UserID == UserHelper.CurrentUserID);
            db.TournamentParticipant.Remove(item);
            db.SaveChanges();
        }
    }
}