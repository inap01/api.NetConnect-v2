using api.NetConnect.data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace api.NetConnect.DataControllers
{
    public class PartnerDataController : GenericDataController<Partner>
    {
        public static Partner Update(Partner item)
        {
            Partner dbItem = GetItem(item.ID);

            dbItem.Name = item.Name;
            dbItem.Link = item.Link;
            dbItem.Content = item.Content;
            dbItem.PartnerPackID = item.PartnerPackID;
            dbItem.IsActive = item.IsActive;
            dbItem.Position = item.Position;
            dbItem.ClickCount = item.ClickCount;

            db.SaveChanges();

            return dbItem;
        }
    }
    public class PartnerPackDataController : GenericDataController<PartnerPack>
    {
        public static PartnerPack Update(PartnerPack item)
        {
            PartnerPack dbItem = GetItem(item.ID);

            dbItem.Name = item.Name;

            db.SaveChanges();

            return dbItem;
        }
    }
}