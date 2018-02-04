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
}