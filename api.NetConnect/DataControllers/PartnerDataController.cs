using api.NetConnect.data.Entity;
using api.NetConnect.data.ViewModel;
using api.NetConnect.data.ViewModel.Partner.Backend;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace api.NetConnect.DataControllers
{
    public class PartnerDataController : BaseDataController, IDataController<Partner>
    {
        public PartnerDataController() : base()
        {

        }

        #region Basic Functions
        public Partner GetItem(int ID)
        {
            var qry = db.Partner.AsQueryable();
            qry.Include(x => x.PartnerPack);
            qry.Include(x => x.PartnerDisplayRelation);
            qry.Include(x => x.Tournament);

            return qry.Single(x => x.ID == ID);
        }

        public IQueryable<Partner> GetItems()
        {
            var qry = db.Partner.AsQueryable();
            qry.Include(x => x.PartnerPack);
            qry.Include(x => x.PartnerDisplayRelation);
            qry.Include(x => x.Tournament);

            return qry;
        }

        public Partner Insert(Partner item)
        {
            var result = db.Partner.Add(item);
            db.SaveChanges();

            return result;
        }

        public Partner Update(Partner item)
        {
            throw new NotImplementedException();
        }

        public void Delete(int ID)
        {
            db.Partner.Remove(GetItem(ID));
            db.SaveChanges();
        }
        #endregion

        public List<Partner> FilterList(ListArgsRequest<BackendPartnerFilter> args, out Int32 TotalCount)
        {
            var qry = GetItems();

            if (args.Filter.StatusSelected != StatusFilterEnum.Alle)
            {
                if (args.Filter.StatusSelected == StatusFilterEnum.Aktiv)
                    qry = qry.Where(x => x.IsActive);
                else
                    qry = qry.Where(x => !x.IsActive);
            }

            if (args.Filter.PartnerTypeSelected != "Alle")
                qry = qry.Where(x => x.PartnerPack.Name == args.Filter.PartnerTypeSelected);

            qry = qry.Where(x => x.Name.ToLower().Contains(args.Filter.Name.ToLower()));

            TotalCount = qry.Count();

            var items = qry.Skip(args.Pagination.ItemsPerPageSelected * (args.Pagination.Page - 1))
                 .Take(args.Pagination.ItemsPerPageSelected)
                 .ToList();

            return items;
        }
    }
}