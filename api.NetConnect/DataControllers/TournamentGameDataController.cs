using api.NetConnect.data.Entity;
using api.NetConnect.data.ViewModel;
using api.NetConnect.data.ViewModel.Game.Backend;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace api.NetConnect.DataControllers
{
    public class TournamentGameDataController : BaseDataController, IDataController<TournamentGame>
    {
        public TournamentGameDataController() : base()
        {

        }

        #region Basic Funtions
        public TournamentGame GetItem(int ID)
        {
            var qry = db.TournamentGame.AsQueryable();
            qry.Include(x => x.Tournament);

            return qry.Single(x => x.ID == ID);
        }

        public IQueryable<TournamentGame> GetItems()
        {
            var qry = db.TournamentGame.AsQueryable();
            qry.Include(x => x.Tournament);

            return qry;
        }

        public TournamentGame Insert(TournamentGame item)
        {
            var result = db.TournamentGame.Add(item);
            db.SaveChanges();

            return result;
        }

        public TournamentGame Update(TournamentGame item)
        {
            throw new NotImplementedException();
        }

        public void Delete(int ID)
        {
            db.TournamentGame.Remove(GetItem(ID));
            db.SaveChanges();
        }
        #endregion

        public List<TournamentGame> FilterList(ListArgsRequest<BackendGameFilter> args, out Int32 TotalCount)
        {
            var qry = GetItems();

            qry = qry.Where(x => x.Name.ToLower().IndexOf(args.Filter.Name.ToLower()) != -1);

            TotalCount = qry.Count();

            qry = qry.OrderBy(x => x.Name);
            var items = qry.Skip(args.Pagination.ItemsPerPageSelected * (args.Pagination.Page - 1))
                 .Take(args.Pagination.ItemsPerPageSelected)
                 .ToList();

            return items;
        }
    }
}