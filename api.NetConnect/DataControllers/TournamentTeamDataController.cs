using api.NetConnect.data.Entity;
using System;
using System.Data.Entity;
using System.Linq;

namespace api.NetConnect.DataControllers
{
    public class TournamentTeamDataController : BaseDataController, IDataController<TournamentTeam>
    {
        public TournamentTeamDataController() : base()
        {

        }

        #region Basic Functions
        public TournamentTeam GetItem(int ID)
        {
            var qry = db.TournamentTeam.AsQueryable();
            qry.Include(x => x.Tournament);
            qry.Include(x => x.TournamentTeamParticipant);
            qry.Include(x => x.TournamentWinnerTeam);

            return qry.Single(x => x.ID == ID);
        }

        public IQueryable<TournamentTeam> GetItems()
        {
            var qry = db.TournamentTeam.AsQueryable();
            qry.Include(x => x.Tournament);
            qry.Include(x => x.TournamentTeamParticipant);
            qry.Include(x => x.TournamentWinnerTeam);

            return qry;
        }

        public TournamentTeam Insert(TournamentTeam item)
        {
            var result = db.TournamentTeam.Add(item);
            db.SaveChanges();

            return result;
        }

        public TournamentTeam Update(TournamentTeam item)
        {
            throw new NotImplementedException();
        }

        public void Delete(int ID)
        {
            db.TournamentTeam.Remove(GetItem(ID));
            db.SaveChanges();
        }
        #endregion
    }
}