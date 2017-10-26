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

            #region NavigationTop
            viewmodel.Data.NavigationTop.Add(new NavItem()
            {
                Text = "News",
                State = "news",
                StateCompare = "news"
            });
            viewmodel.Data.NavigationTop.Add(new NavItem()
            {
                Text = "Events",
                State = "event.all",
                StateCompare = "event"
            });
            viewmodel.Data.NavigationTop.Add(new NavItem()
            {
                Text = "Galerie",
                State = "gallery.all",
                StateCompare = "gallery"
            });
            viewmodel.Data.NavigationTop.Add(new NavItem()
            {
                Text = "Turniere",
                State = "tournaments",
                StateCompare = "tournaments"
            });
            viewmodel.Data.NavigationTop.Add(new NavItem()
            {
                Text = "Sitzplan",
                State = "seating",
                StateCompare = "seating"
            });
            viewmodel.Data.NavigationTop.Add(new NavItem()
            {
                Text = "Sponsoren",
                State = "partner",
                StateCompare = "partner"
            });
            #endregion
            #region NavigationTop
            viewmodel.Data.NavigationUser.Add(new NavItem()
            {
                Text = "Registrieren",
                State = "register",
                StateCompare = "register"
            });
            viewmodel.Data.NavigationUser.Add(new NavItem()
            {
                Text = "Einloggen",
                State = "login",
                StateCompare = "login"
            });
            #endregion
            #region NavigationAside
            viewmodel.Data.NavigationAside.Add(new NavItem()
            {
                Text = "Informationen",
                State = "event.details({id: 1})"
            });
            viewmodel.Data.NavigationAside.Add(new NavItem()
            {
                Text = "Teilnahme",
                State = "event.seating({id: 1})"
            });
            viewmodel.Data.NavigationAside.Add(new NavItem()
            {
                Text = "Jungendschutz",
                State = "jungendschutz"
            });
            viewmodel.Data.NavigationAside.Add(new NavItem()
            {
                Text = "Teilnahmebedingungen",
                State = "teilnahmebedingungen"
            });
            viewmodel.Data.NavigationAside.Add(new NavItem()
            {
                Text = "Kontakt",
                State = "contact"
            });
            #endregion
            #region NavigationAside
            viewmodel.Data.NavigationBottom.Add(new LinkItem()
            {
                Text = "Sponsor 1",
                Link = "http://google.de"
            });
            viewmodel.Data.NavigationBottom.Add(new LinkItem()
            {
                Text = "Sponsor 2",
                Link = "http://google.de"
            });
            viewmodel.Data.NavigationBottom.Add(new LinkItem()
            {
                Text = "Sponsor 3",
                Link = "http://google.de"
            });
            viewmodel.Data.NavigationBottom.Add(new LinkItem()
            {
                Text = "Sponsor 4",
                Link = "http://google.de"
            });
            viewmodel.Data.NavigationBottom.Add(new LinkItem()
            {
                Text = "Sponsor 5",
                Link = "http://google.de"
            });
            viewmodel.Data.NavigationBottom.Add(new LinkItem()
            {
                Text = "Sponsor 6",
                Link = "http://google.de"
            });
            #endregion

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
