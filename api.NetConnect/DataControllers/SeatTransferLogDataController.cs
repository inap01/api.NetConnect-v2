using api.NetConnect.data.Entity;
using api.NetConnect.data.ViewModel;
using api.NetConnect.data.ViewModel.Seating;
using api.NetConnect.data.ViewModel.Seating.Backend;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace api.NetConnect.DataControllers
{
    public class SeatTransferLogDataController : BaseDataController, IDataController<SeatTransferLog>
    {

        public SeatTransferLog GetItem(int ID)
        {
            var qry = db.SeatTransferLog.AsQueryable();
            qry.Include(x => x.Seat);
            qry.Include(x => x.SourceUser);
            qry.Include(x => x.DestinationUser);

            return qry.Single(x => x.ID == ID);
        }

        public IQueryable<SeatTransferLog> GetItems()
        {
            var qry = db.SeatTransferLog.AsQueryable();
            qry.Include(x => x.Seat);
            qry.Include(x => x.SourceUser);
            qry.Include(x => x.DestinationUser);

            return qry;
        }

        public SeatTransferLog Insert(SeatTransferLog item)
        {
            var result = db.SeatTransferLog.Add(item);
            db.SaveChanges();

            return result;
        }

        public SeatTransferLog Update(SeatTransferLog item)
        {
            throw new NotImplementedException();
        }

        public void Delete(int ID)
        {
            db.SeatTransferLog.Remove(GetItem(ID));
            db.SaveChanges();
        }
    }
}