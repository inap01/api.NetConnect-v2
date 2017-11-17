using api.NetConnect.data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace api.NetConnect.DataControllers
{
    public class PartnerDataController : GenericDataController<Partner>
    {
        public static Partner Create(Partner item)
        {
            Partner dbItem = db.Partner.Add(item);

            db.SaveChanges();

            return dbItem;
        }

        public static Partner Update(Partner item)
        {
            db.SaveChanges();

            return item;
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

    public class PartnerDisplayDataController : GenericDataController<PartnerDisplay>
    {
        public static PartnerDisplay Update(PartnerDisplay item)
        {
            PartnerDisplay dbItem = GetItem(item.ID);


            return dbItem;
        }
    }

    public class PartnerDisplayRelationDataController : GenericDataController<PartnerDisplayRelation>
    {
        public static PartnerDisplayRelation Update(PartnerDisplayRelation item)
        {
            PartnerDisplayRelation dbItem = GetItem(item.ID);


            return dbItem;
        }
    }
}