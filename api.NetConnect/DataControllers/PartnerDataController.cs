using api.NetConnect.data;
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

        public static data.Partner GetItem(Int32 id)
        {
            DataContext db = InitDB();

            return db.Partner.FirstOrDefault(x => x.ID == id);
        }
    }
}