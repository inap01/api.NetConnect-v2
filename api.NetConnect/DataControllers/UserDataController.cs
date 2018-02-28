using api.NetConnect.data.Entity;
using api.NetConnect.data.ViewModel;
using api.NetConnect.data.ViewModel.User.Backend;
using api.NetConnect.Helper;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace api.NetConnect.DataControllers
{
    public class UserDataController : BaseDataController, IDataController<User>
    {
        public UserDataController() : base()
        {

        }

        #region Basic Functions
        public User GetItem(int ID)
        {
            var qry = db.User.AsQueryable();

            return qry.Single(x => x.ID == ID);
        }

        public IQueryable<User> GetItems()
        {
            var qry = db.User.AsQueryable();

            return qry;
        }

        public User Insert(User item)
        {
            var result = db.User.Add(item);
            db.SaveChanges();

            return result;
        }

        public User Update(User item)
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

        public void Delete(int ID)
        {
            db.User.Remove(GetItem(ID));
            db.SaveChanges();
        }
        #endregion

        public List<User> FilterList(ListArgsRequest<BackendProfileFilter> args, out Int32 TotalCount)
        {
            List<BackendUserViewModelItem> result = new List<BackendUserViewModelItem>();

            var qry = GetItems();

            if(!String.IsNullOrEmpty(args.Filter.FirstName))
                qry = qry.Where(x => x.FirstName.ToLower().Contains(args.Filter.FirstName.ToLower()));
            if (!String.IsNullOrEmpty(args.Filter.LastName))
                qry = qry.Where(x => x.LastName.ToLower().Contains(args.Filter.LastName.ToLower()));
            if (!String.IsNullOrEmpty(args.Filter.Nickname))
                qry = qry.Where(x => x.Nickname.ToLower().Contains(args.Filter.Nickname.ToLower()));

            TotalCount = qry.Count();

            qry = qry.OrderBy(x => x.FirstName);
            var items = qry.Skip(args.Pagination.ItemsPerPageSelected * (args.Pagination.Page - 1))
                 .Take(args.Pagination.ItemsPerPageSelected)
                 .ToList();

            return items;
        }

        public Boolean ValidateUser(String email, String password)
        {
            User u;
            return ValidateUser(email, password, out u);
        }

        public Boolean ValidateUser(String email, String password, out User User)
        {
            var user = GetItems().Single(x => x.Email == email);
            if(PasswordHelper.HashPassword(password, user.PasswordSalt) == user.Password)
            {
                User = user;
                return true;
            }

            User = null;
            return false;
        }

        public Boolean CheckExistingEmail(String email)
        {
            return GetItems().Count(x => x.Email == email) > 0;
        }

        public Boolean CheckExistingNickname(String nickname)
        {
            return GetItems().Count(x => x.Nickname == nickname) > 0;
        }

        public User ChangePassword(Int32 userID, String NewPassword)
        {
            User dbItem = GetItem(userID);

            dbItem.Password = NewPassword;
            dbItem.PasswordReset = null;

            db.SaveChanges();

            return dbItem;
        }

        public User SetPasswordReset(Int32 userID)
        {
            User dbItem = GetItem(userID);
            
            dbItem.PasswordReset = PasswordHelper.HashPassword(DateTime.Now.ToString(), "#random");

            db.SaveChanges();

            return dbItem;
        }
    }
}