using api.NetConnect.data.Entity;
using api.NetConnect.data.ViewModel.EventType.Backend;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace api.NetConnect.DataControllers
{
    public class EventTypeDataController : BaseDataController, IDataController<EventType>
    {
        public EventTypeDataController() : base()
        {

        }

        #region Basic Functions
        public EventType GetItem(int ID)
        {
            var qry = db.EventType.AsQueryable();
            qry.Include(x => x.Event);

            return qry.Single(x => x.ID == ID);
        }

        public IQueryable<EventType> GetItems()
        {
            var qry = db.EventType.AsQueryable();
            qry.Include(x => x.Event);

            return qry;
        }

        public EventType Insert(EventType item)
        {
            var result = db.EventType.Add(item);
            db.SaveChanges();

            return result;
        }

        public EventType Update(EventType item)
        {
            var dbItem = GetItem(item.ID);
            
            dbItem.Name = item.Name;
            dbItem.Description = item.Description;

            db.SaveChanges();

            return dbItem;
        }

        public void Delete(int ID)
        {
            db.EventType.Remove(GetItem(ID));
            db.SaveChanges();
        }
        #endregion

        public List<EventType> FilterList(BackendEventTypeListArgs args, out Int32 TotalCount)
        {
            var qry = GetItems();

            if(!String.IsNullOrEmpty(args.Filter.Name))
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