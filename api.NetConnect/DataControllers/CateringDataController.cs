using api.NetConnect.data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace api.NetConnect.DataControllers
{
    public class CateringProductDataController : GenericDataController<CateringProduct>
    {
        public static CateringProduct Update(CateringProduct item)
        {
            CateringProduct dbItem = GetItem(item.ID);

            dbItem.Name = item.Name;
            dbItem.Description = item.Description;
            dbItem.Image = item.Image;
            dbItem.Price = item.Price;
            dbItem.Attributes = item.Attributes;
            dbItem.SingleChoice = item.SingleChoice;

            db.SaveChanges();

            return dbItem;
        }
    }

    public class CateringOrderDataController : GenericDataController<CateringOrder>
    {
        public static CateringOrder Update(CateringOrder item)
        {
            CateringOrder dbItem = GetItem(item.ID);

            dbItem.Volume = item.Volume;
            dbItem.UserID = item.UserID;
            dbItem.SeatID = item.SeatID;
            dbItem.CompletionState = item.CompletionState;

            db.SaveChanges();

            return dbItem;
        }
    }

    public class CateringOrderDetailDataController : GenericDataController<CateringOrderDetail>
    {
        public static CateringOrderDetail Update(CateringOrderDetail item)
        {
            CateringOrderDetail dbItem = GetItem(item.ID);

            dbItem.CateringOrderID = item.CateringOrderID;
            dbItem.CateringProductID = item.CateringProductID;
            dbItem.Attributes = item.Attributes;

            db.SaveChanges();

            return dbItem;
        }
    }
}