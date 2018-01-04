using api.NetConnect.data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace api.NetConnect.DataControllers
{
    public class CateringDataController : GenericDataController<CateringProduct>
    {
        public static CateringProduct Update(CateringProduct item)
        {
            CateringProduct dbItem = GetItem(item.ID);

            dbItem.Name = item.Name;

            db.SaveChanges();

            return dbItem;
        }
    }

    public class CateringOrderDataController : GenericDataController<CateringOrder>
    {
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