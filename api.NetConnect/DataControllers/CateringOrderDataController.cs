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

    public class CateringOrderDataController : BaseDataController, IDataController<CateringOrder>
    {
        public CateringOrderDataController() : base()
        {

        }

        #region Basic Functions
        public CateringOrder GetItem(int ID)
        {
            var qry = db.CateringOrder.AsQueryable();
            qry.Include(x => x.CateringOrderDetail);
            qry.Include(x => x.User);
            qry.Include(x => x.Seat);
            qry.Include(x => x.Event);

            return qry.Single(x => x.ID == ID);
        }

        public IQueryable<CateringOrder> GetItems()
        {
            var qry = db.CateringOrder.AsQueryable();
            qry.Include(x => x.CateringOrderDetail);
            qry.Include(x => x.User);
            qry.Include(x => x.Seat);
            qry.Include(x => x.Event);

            return qry;
        }

        public CateringOrder Insert(CateringOrder item)
        {
            var result = db.CateringOrder.Add(item);
            db.SaveChanges();

            return result;
        }

        public CateringOrder Update(CateringOrder item)
        {
            var dbItem = GetItem(item.ID);

            dbItem.OrderState = item.OrderState;
            db.SaveChanges();

            return dbItem;
        }

        public void Delete(int ID)
        {
            db.CateringOrder.Remove(GetItem(ID));
            db.SaveChanges();
        }
        #endregion

        public IEnumerable<CateringOrder> FilterList(ListArgsRequest<BackendCateringFilter> args, out Int32 TotalCount)
        {
            var qry = db.CateringOrder.AsQueryable();
            qry.Include(x => x.CateringOrderDetail);

            qry = qry.Where(x => x.EventID == args.Filter.EventSelected.ID);

            if (!String.IsNullOrEmpty(args.Filter.Name))
                qry = qry.Where(x => (x.User.FirstName + " " + x.User.LastName).Contains(args.Filter.Name));
            if (!String.IsNullOrEmpty(args.Filter.SeatNumber))
                qry = qry.Where(x => x.Seat.SeatNumber.ToString().Contains(args.Filter.SeatNumber));
            if (args.Filter.StatusSelected == CateringStatusFilterEnum.Offen)
                qry = qry.Where(x => x.OrderState >= 0 && x.OrderState < 2);
            if (args.Filter.StatusSelected == CateringStatusFilterEnum.Abgeschlossen)
                qry = qry.Where(x => x.OrderState == -1 || x.OrderState == 2);

            TotalCount = qry.Count();

            qry = qry.OrderByDescending(x => x.Registered);
            qry = qry.Skip(args.Pagination.ItemsPerPageSelected * (args.Pagination.Page - 1))
                 .Take(args.Pagination.ItemsPerPageSelected);

            return qry;
        }
    }
}