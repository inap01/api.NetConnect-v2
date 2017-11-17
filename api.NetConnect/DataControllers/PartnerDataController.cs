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
            Partner dbItem = GetItem(item.ID);

            dbItem.Name = item.Name;
            dbItem.Link = item.Link;
            dbItem.RefLink = item.RefLink;
            dbItem.Content = item.Content;
            dbItem.PartnerPackID = item.PartnerPackID;
            dbItem.IsActive = item.IsActive;
            dbItem.Position = item.Position;
            dbItem.ClickCount = item.ClickCount;

            var relationListBefore = dbItem.PartnerDisplayRelation.ToList();
            var relationListNow = item.PartnerDisplayRelation.ToList();

            var newList = item.PartnerDisplayRelation.Intersect(dbItem.PartnerDisplayRelation);
            //Was gleich ist
            foreach(var relation in dbItem.PartnerDisplayRelation.ToList())
            {
                var tmp = relationListNow.FirstOrDefault(x => x.PartnerDisplayID == relation.PartnerDisplayID);
                if (tmp != null)
                {
                    relationListBefore.Remove(relation);
                    relationListNow.Remove(tmp);
                    dbItem.PartnerDisplayRelation.Add(relation);
                }
            }
            //Füge neue Relationen hinzu
            foreach (var relation in relationListNow)
            {
                dbItem.PartnerDisplayRelation.Add(relation);
            }
            //Lösche nicht mehr vorhandenes
            foreach (var relation in relationListBefore)
            {
                var tmp = dbItem.PartnerDisplayRelation.FirstOrDefault(x => x.PartnerDisplayID == relation.PartnerDisplayID);
                if(tmp != null)
                    dbItem.PartnerDisplayRelation.Remove(tmp);
            }

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