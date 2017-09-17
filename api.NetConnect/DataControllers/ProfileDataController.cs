using api.NetConnect.data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace api.NetConnect.DataControllers
{
    public class ProfileDataController
    {
        private static DataContext InitDB()
        {
            return new DataContext();
        }

        public static data.User GetItem(Int32 id)
        {
            DataContext db = InitDB();

            return db.User.FirstOrDefault(x => x.ID == id);
        }
    }
}