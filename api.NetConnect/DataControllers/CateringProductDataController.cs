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
    public class CateringProductDataController : BaseDataController, IDataController<CateringProduct>
    {
        public CateringProductDataController() : base()
        {

        }

        #region Basic Functions
        public CateringProduct GetItem(int ID)
        {
            var qry = db.CateringProduct.AsQueryable();
            qry.Include(x => x.CateringProductAttributeRelation);

            return qry.Single(x => x.ID == ID);
        }

        public IQueryable<CateringProduct> GetItems()
        {
            var qry = db.CateringProduct.AsQueryable();
            qry.Include(x => x.CateringProductAttributeRelation);

            return qry;
        }

        public CateringProduct Insert(CateringProduct item)
        {
            var result = db.CateringProduct.Add(item);
            db.SaveChanges();

            return result;
        }

        public CateringProduct Update(CateringProduct item)
        {
            var dbItem = GetItem(item.ID);
            dbItem.Name = item.Name;
            dbItem.Image = item.Image;
            dbItem.Price = item.Price;
            dbItem.SingleChoice = item.SingleChoice;
            dbItem.IsActive = item.IsActive;

            db.SaveChanges();

            return dbItem;
        }

        public void Delete(int ID)
        {
            db.CateringProduct.Remove(GetItem(ID));
            db.SaveChanges();
        }
        #endregion

        public List<CateringProduct> FilterList(ListArgsRequest<BackendProductFilter> args, out Int32 TotalCount)
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