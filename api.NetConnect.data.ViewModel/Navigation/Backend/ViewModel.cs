using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace api.NetConnect.data.ViewModel.Navigation.Backend
{
    public class BackendNavigationViewModel : BackendBaseViewModel
    {
        public BackendNavigationViewModelItem Data { get; set; }

        public BackendNavigationViewModel()
        {
            Data = new BackendNavigationViewModelItem();

            Form = GetForm();
        }

        public override Dictionary<string, InputInformation> GetForm()
        {
            Dictionary<string, InputInformation> result = new Dictionary<string, InputInformation>();

            return result;
        }
    }

    public class BackendNavigationViewModelItem : BaseViewModelItem
    {
        public List<NavItem> NavigationTop { get; set; }
        public List<NavItem> NavigationAside { get; set; }

        public BackendNavigationViewModelItem()
        {
            NavigationTop = new List<NavItem>();
            NavigationAside = new List<NavItem>();
        }
    }
}