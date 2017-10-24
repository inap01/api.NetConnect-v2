using api.NetConnect.data.ViewModel.Navigation;
using api.NetConnect.data.ViewModel.Navigation.Backend;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace api.NetConnect.Controllers
{
    public class NavigationController : ApiController
    {
        [HttpGet]
        public IHttpActionResult Frontend()
        {
            NavigationViewModel viewmodel = new NavigationViewModel();

            return Ok(viewmodel);
        }

        [HttpGet]
        public IHttpActionResult Backend()
        {
            BackendNavigationViewModel viewmodel = new BackendNavigationViewModel();

            viewmodel.Data.NavigationAside.Add(new NavItem()
            {
                Text = "Dashboard",
                State = "admin.dashboard",
                StateCompare = "admin.dashboard"
            });
            viewmodel.Data.NavigationAside.Add(new NavItem()
            {
                Text = "News",
                State = "admin.news.all",
                StateCompare = "admin.news"
            });
            viewmodel.Data.NavigationAside.Add(new NavItem()
            {
                Text = "Turniere",
                StateCompare = "admin.tournament",
                SubMenu = new List<NavItem>() {
                    new NavItem() {
                        Text = "Alle Turniere",
                        State = "admin.tournament.all",
                        StateCompare = "admin.tournament.all"
                    },
                    new NavItem() {
                        Text = "Neues Turnier",
                        State = "admin.tournament.new",
                        StateCompare = "admin.tournament.new"
                    },
                    new NavItem() {
                        Text = "Spiele",
                        State = "admin.tournament.all",
                        StateCompare = "admin.tournament.all"
                    }
                }
            });
            viewmodel.Data.NavigationAside.Add(new NavItem()
            {
                Text = "Partner",
                StateCompare = "admin.partner",
                SubMenu = new List<NavItem>() {
                    new NavItem() {
                        Text = "Alle Partner",
                        State = "admin.partner.all",
                        StateCompare = "admin.partner.all"
                    },
                    new NavItem() {
                        Text = "Neuer Partner",
                        State = "admin.partner.new",
                        StateCompare = "admin.partner.new"
                    },
                    new NavItem() {
                        Text = "Typen",
                        State = "admin.partner.all",
                        StateCompare = "admin.partner.all"
                    }
                }
            });

            return Ok(viewmodel);
        }
    }
}
