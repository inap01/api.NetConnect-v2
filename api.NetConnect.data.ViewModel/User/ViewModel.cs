using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace api.NetConnect.data.ViewModel.User
{
    public class UserViewModel : BaseViewModel
    {
        public UserViewModelItem Data { get; set; }

        public UserViewModel()
        {
            Data = new UserViewModelItem();
        }
    }

    public class UserViewModelItem : BaseViewModelItem
    {
        public String FirstName { get; set; }
        public String LastName { get; set; }
        public String Nickname { get; set; }
        public String Email { get; set; }
        public String Image { get; set; }
        public String SteamID { get; set; }
        public String BattleTag { get; set; }

        public UserViewModelItem()
        {
        }
    }
}