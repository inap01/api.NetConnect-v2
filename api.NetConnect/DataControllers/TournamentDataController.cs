using api.NetConnect.data.Entity;
using api.NetConnect.data.ViewModel;
using api.NetConnect.data.ViewModel.Tournament.Backend;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace api.NetConnect.DataControllers
{
    public class TournamentDataController : BaseDataController, IDataController<Tournament>
    {
        public TournamentDataController() : base()
        {

        }

        #region Basic Functions
        public Tournament GetItem(int ID)
        {
            var qry = db.Tournament.AsQueryable();
            qry = qry.Include(x => x.TournamentGame);
            qry = qry.Include(x => x.Event);
            qry = qry.Include(x => x.Partner);
            qry = qry.Include(x => x.TournamentParticipant.Select(y => y.User));
            qry = qry.Include(x => x.TournamentTeam.Select(y => y.TournamentTeamParticipant.Select(z => z.User)));
            qry = qry.Include(x => x.TournamentWinner.Select(y => y.TournamentWinnerTeam));
            qry = qry.Include(x => x.TournamentWinner.Select(y => y.TournamentWinnerPlayer.User));

            return qry.Single(x => x.ID == ID);
        }

        public IQueryable<Tournament> GetItems()
        {
            var qry = db.Tournament.AsQueryable();
            qry = qry.Include(x => x.TournamentGame);
            qry = qry.Include(x => x.Event);
            qry = qry.Include(x => x.Partner);
            qry = qry.Include(x => x.TournamentParticipant.Select(y => y.User));
            qry = qry.Include(x => x.TournamentTeam.Select(y => y.TournamentTeamParticipant.Select(z => z.User)));
            qry = qry.Include(x => x.TournamentWinner.Select(y => y.TournamentWinnerTeam));
            qry = qry.Include(x => x.TournamentWinner.Select(y => y.TournamentWinnerPlayer.User));

            return qry;
        }

        public Tournament Insert(Tournament item)
        {
            var result = db.Tournament.Add(item);
            db.SaveChanges();

            db.Entry(result).Reference(c => c.Event).Load();
            db.Entry(result).Reference(c => c.TournamentGame).Load();

            return result;
        }

        public Tournament Update(Tournament item)
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

        public void Delete(int ID)
        {
            db.Tournament.Remove(GetItem(ID));
            db.SaveChanges();
        }
        #endregion
        
        public List<Tournament> FilterList(ListArgsRequest<BackendTournamentFilter> args, out Int32 TotalCount)
        {
            var qry = GetItems();

            qry = qry.Where(x => x.EventID == args.Filter.EventSelected.ID);
            if (args.Filter.GameSelected.ID != -1)
                qry = qry.Where(x => x.TournamentGameID == args.Filter.GameSelected.ID);

            TotalCount = qry.Count();

            qry = qry.OrderByDescending(x => x.EventID);
            qry = qry.Skip(args.Pagination.ItemsPerPageSelected * (args.Pagination.Page - 1))
                 .Take(args.Pagination.ItemsPerPageSelected);

            return qry.ToList();
        }
    }
}