using api.NetConnect.data.Entity;
using api.NetConnect.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace api.NetConnect.DataControllers
{
    public class UserDataController : GenericDataController<User>
    {
        public static User Update (User item)
        {
            User dbItem = GetItem(item.ID);

            dbItem.Newsletter = item.Newsletter;
            dbItem.BattleTag = item.BattleTag;
            dbItem.SteamID = item.SteamID;
            //dbItem.Image = item.Image;
            dbItem.IsAdmin = item.IsAdmin;
            dbItem.IsTeam = item.IsTeam;
            dbItem.Email = item.Email;
            dbItem.Nickname = item.Nickname;
            dbItem.LastName = item.LastName;
            dbItem.FirstName = item.FirstName;

            db.SaveChanges();

            return dbItem;
        }

        public static Boolean ValidateUser(String email, String password, out User User)
        {
            var user = UserDataController.GetItem(email, "Email");
            if(PasswordHelper.HashPassword(password) == user.Password)
            {
                User = user;
                return true;
            }

            User = null;
            return false;
        }
    }
}