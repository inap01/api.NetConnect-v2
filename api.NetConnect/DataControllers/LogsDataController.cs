using api.NetConnect.data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace api.NetConnect.DataControllers
{
    public class LogsDataController : GenericDataController<Logs>
    {
        public static Logs Update (Logs item)
        {
            Logs dbItem = GetItem(item.ID);

            dbItem.UserID = item.UserID;
            dbItem.SQLTable = item.SQLTable;
            dbItem.SQLActionType = item.SQLActionType;
            dbItem.SQLQuery = item.SQLQuery;
            dbItem.ModelBefore = item.ModelBefore;
            dbItem.ModelAfter = item.ModelAfter;

            db.SaveChanges();

            return dbItem;
        }
    }
}