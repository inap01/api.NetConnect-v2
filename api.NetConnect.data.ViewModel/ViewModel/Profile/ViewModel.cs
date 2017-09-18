using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace api.NetConnect.ViewModels.Profile
{
    public class ProfileViewModel : BaseViewModel
    {
        public ProfileViewModelItem Data { get; set; }

        public ProfileViewModel()
        {
            Data = new ProfileViewModelItem();
        }
    }

    public class ProfileViewModelItem : BaseViewModelItem
    {
        public Int32 ID { get; set; }
        public String FirstName { get; set; }
        public String LastName { get; set; }
        public String Nickname { get; set; }
        public String Email { get; set; }
        public String SteamID { get; set; }
        public String BattleTag { get; set; }
        public Boolean Newsletter { get; set; }
        public String Password1 { get; set; }
        public String Password2 { get; set; }

        public ProfileViewModelItem()
        {
        }
    }
}