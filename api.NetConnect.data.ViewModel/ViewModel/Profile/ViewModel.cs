using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace api.NetConnect.ViewModels.Profile
{
    public class ProfileViewModel : BaseViewModel
    {
        public ProfileViewModelItem Data { get; set; }
        public Int32 TotalCount { get; set; }

        public ProfileViewModel()
        {
            Data = new ProfileViewModelItem();
            TotalCount = 0;
        }
    }

    public class ProfileViewModelItem
    {
        public Int32 ID { get; set; }
        public String FirstName { get; set; }
        public String LastName { get; set; }
        public String Nickname { get; set; }
        public String Email { get; set; }
        public String SteamID { get; set; }
        public String BattleTag { get; set; }
        public Boolean Newsletter { get; set; }

        public ProfileViewModelItem()
        {
        }
    }
}