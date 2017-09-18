using api.NetConnect.data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace api.NetConnect.DataControllers
{
    public class PartnerDataController
    {
        private static DataContext InitDB()
        {
            return new DataContext();
        }

        public static Partner GetItem(Int32 id)
        {
            DataContext db = InitDB();

            return db.Partner.FirstOrDefault(x => x.ID == id);
        }
    }
}