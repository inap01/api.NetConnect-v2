using api.NetConnect.data.Entity;
using api.NetConnect.data.ViewModel.Profile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace api.NetConnect.data.ViewModel.Auth
{
    public class LoginViewModel : BaseViewModel
    {
        public ProfileViewModelItem Data { get; set; }

        public LoginViewModel()
        {
            Data = new ProfileViewModelItem();
        }
    }
}
