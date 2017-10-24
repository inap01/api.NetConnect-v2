using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace api.NetConnect.data.ViewModel.Profile.Backend
{
    public class BackendProfileViewModel : BaseViewModel
    {
        public BackendProfileViewModelItem Data { get; set; }

        public BackendProfileViewModel()
        {
            Data = new BackendProfileViewModelItem();
        }
    }

    public class BackendProfileViewModelItem : BaseViewModelItem
    {
        public String FirstName { get; set; }
        public String LastName { get; set; }
        public String Nickname { get; set; }
        public String Email { get; set; }
        public String SteamID { get; set; }
        public String BattleTag { get; set; }
        public Boolean Newsletter { get; set; }
        public String OldPassword { get; set; }
        public String NewPassword1 { get; set; }
        public String NewPassword2 { get; set; }

        public BackendProfileViewModelItem()
        {
        }
    }
}