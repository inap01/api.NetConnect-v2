using api.NetConnect.data.Entity;
using api.NetConnect.data.ViewModel;
using api.NetConnect.data.ViewModel.Catering.Backend;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace api.NetConnect.DataControllers
{
    public class CateringProductAttributeDataController : BaseDataController, IDataController<CateringProductAttribute>
    {
        public CateringProductAttributeDataController() : base()
        {

        }

        #region Basic Functions
        public CateringProductAttribute GetItem(int ID)
        {
            var qry = db.CateringProductAttribute.AsQueryable();
            qry.Include(x => x.CateringProductAttributeRelation);

            return qry.Single(x => x.ID == ID);
        }

        public IQueryable<CateringProductAttribute> GetItems()
        {
            var qry = db.CateringProductAttribute.AsQueryable();
            qry.Include(x => x.CateringProductAttributeRelation);

            return qry;
        }

        public CateringProductAttribute Insert(CateringProductAttribute item)
        {
            var result = db.CateringProductAttribute.Add(item);
            db.SaveChanges();

            return result;
        }

        public CateringProductAttribute Update(CateringProductAttribute item)
        {
            var dbItem = GetItem(item.ID);
            dbItem.Name = item.Name;

            db.SaveChanges();

            return dbItem;
        }

        public void Delete(int ID)
        {
            db.CateringProductAttribute.Remove(GetItem(ID));
            db.SaveChanges();
        }
        #endregion

        public List<CateringProductAttribute> FilterList(ListArgsRequest<BackendProductAttributeFilter> args, out Int32 TotalCount)
        {
            var qry = GetItems();

            if (!String.IsNullOrEmpty(args.Filter.Name))
                qry = qry.Where(x => x.Name.ToLower().Contains(args.Filter.Name.ToLower()));

            TotalCount = qry.Count();

            qry = qry.OrderByDescending(x => x.ID);
            var items = qry.Skip(args.Pagination.ItemsPerPageSelected * (args.Pagination.Page - 1))
                 .Take(args.Pagination.ItemsPerPageSelected)
                 .ToList();

            return items;
        }
    }
}