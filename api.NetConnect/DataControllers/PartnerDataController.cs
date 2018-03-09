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

            db.Entry(result).Reference(c => c.PartnerPack).Load();

            return result;
        }

        public Partner Update(Partner item)
        {
            var dbItem = GetItem(item.ID);
            
            dbItem.Name = item.Name;
            dbItem.Content = item.Content;
            dbItem.Link = item.Link;
            dbItem.RefLink = item.RefLink;
            dbItem.PartnerPackID = item.PartnerPackID;
            dbItem.ImageOriginal = item.ImageOriginal;
            dbItem.ImagePassive = item.ImagePassive;
            dbItem.IsActive = item.IsActive;

            db.SaveChanges();

            db.Entry(dbItem).Reference(c => c.PartnerPack).Load();

            return dbItem;
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
            if(!String.IsNullOrEmpty(args.Filter.Name))
                qry = qry.Where(x => x.Name.ToLower().Contains(args.Filter.Name.ToLower()));

            TotalCount = qry.Count();

            qry = qry.OrderBy(x => x.Name);
            var items = qry.Skip(args.Pagination.ItemsPerPageSelected * (args.Pagination.Page - 1))
                 .Take(args.Pagination.ItemsPerPageSelected)
                 .ToList();

            return items;
        }

        public Partner UpdatePosition(Partner item)
        {
            var dbItem = GetItem(item.ID);

            dbItem.Position = item.Position;

            db.SaveChanges();

            return dbItem;
        }
    }
}