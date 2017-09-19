using api.NetConnect.data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace api.NetConnect.DataControllers
{
    public class ChatDataController : GenericDataController<Chat>
    {
        public static Chat Update (Chat item)
        {
            Chat dbItem = GetItem(item.ID);

            dbItem.UserID = item.UserID;
            dbItem.Message = item.Message;
            dbItem.GameFlag = item.GameFlag;
            dbItem.GameTitle = item.GameTitle;

            db.SaveChanges();

            return dbItem;
        }
    }
}