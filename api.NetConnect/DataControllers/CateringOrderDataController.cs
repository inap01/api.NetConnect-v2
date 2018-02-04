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

    public class CateringOrderDataController : GenericDataController<CateringOrder>
    {
        public static IEnumerable<CateringOrder> FilterList(ListArgsRequest<BackendCateringFilter> args, out Int32 TotalCount)
        {
            db = InitDB();

            var qry = db.CateringOrder.AsQueryable();
            qry.Include("CateringOrderDetails");
            qry.Include("CateringProduct");
            qry.Include("CateringProductAttribute");
            qry.Include("CateringProductAttributeRelation");

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

            qry = qry.OrderByDescending(x => x.ID);
            qry = qry.Skip(args.Pagination.ItemsPerPageSelected * (args.Pagination.Page - 1))
                 .Take(args.Pagination.ItemsPerPageSelected);

            return qry;
        }

        public static CateringOrder Insert(CateringOrder item)
        {
            InitDB();

            var result = db.CateringOrder.Add(item);
            db.SaveChanges();

            return result;
        }

        public static CateringOrder Update(CateringOrder item)
        {
            CateringOrder dbItem = GetItem(item.ID);



            db.SaveChanges();

            return dbItem;
        }
    }
}