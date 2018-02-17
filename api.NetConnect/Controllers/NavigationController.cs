using api.NetConnect.data.ViewModel.Navigation;
using api.NetConnect.data.ViewModel.Navigation.Backend;
using api.NetConnect.DataControllers;
using api.NetConnect.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace api.NetConnect.Controllers
{
    public class NavigationController : BaseController
    {
        [HttpGet]
        public IHttpActionResult Frontend()
        {
            NavigationViewModel viewmodel = new NavigationViewModel();
            EventDataController dataCtrl = new EventDataController();
            SeatDataController seatDataCtrl = new SeatDataController();
            PartnerDisplayRelationDataController displayDataCtrl = new PartnerDisplayRelationDataController();
            var partner = displayDataCtrl.GetItems()
                .Where(x => x.Partner.IsActive)
                .OrderBy(x => x.Partner.PartnerPackID)
                .ThenBy(x => x.Partner.Position);

            #region NavigationTop
            viewmodel.Data.NavigationTop.Add(new NavItem()
            {
                Text = "News",
                State = "news.all",
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
                State = "event.tournaments.all({id: 10})",
                StateCompare = "event.tournaments"
            });
            viewmodel.Data.NavigationTop.Add(new NavItem()
            {
                Text = "Sitzplan",
                State = "event.seating({id: 10})",
                StateCompare = "event.seating"
            });
            viewmodel.Data.NavigationTop.Add(new NavItem()
            {
                Text = "Sponsoren",
                State = "partner",
                StateCompare = "partner"
            });
            #endregion
            #region NavigationUser
            if(UserHelper.Authenticated)
            {
                if(UserHelper.CurrentUserRole == UserRole.Admin || UserHelper.CurrentUserRole == UserRole.Team)
                {
                    viewmodel.Data.NavigationUser.Add(new NavItem()
                    {
                        Text = "<i class='fas fa-user-secret'></i>",
                        State = "admin.dashboard",
                        StateCompare = "admin.dashboard",
                        Tooltip = "Adminbereich"
                    });
                }
                viewmodel.Data.NavigationUser.Add(new NavItem()
                {
                    Text = "<i class='fas fa-utensils'></i>",
                    State = "catering",
                    StateCompare = "catering",
                    Tooltip = "Catering"
                });
                //viewmodel.Data.NavigationUser.Add(new NavItem()
                //{
                //    Text = "<i class='fas fa-comments'></i>",
                //    State = "profile.overview",
                //    StateCompare = "profile",
                //    Tooltip = "Chat"
                //});
                viewmodel.Data.NavigationUser.Add(new NavItem()
                {
                    Text = "<i class='fas fa-user-circle'></i>",
                    State = "profile.overview",
                    StateCompare = "profile",
                    Tooltip = UserHelper.CurrentUserName
                });
                viewmodel.Data.NavigationUser.Add(new NavItem()
                {
                    Text = "<i class='fas fa-sign-out'></>",
                    State = "logout",
                    StateCompare = "logout",
                    Tooltip = "Ausloggen"
                });
            }
            else
            {
                viewmodel.Data.NavigationUser.Add(new NavItem()
                {
                    Text = "<i class='fas fa-user-plus'></i>",
                    State = "register",
                    StateCompare = "register",
                    Tooltip = "Registrieren"
                });
                viewmodel.Data.NavigationUser.Add(new NavItem()
                {
                    Text = "<i class='fas fa-sign-in'></i>",
                    State = "login",
                    StateCompare = "login",
                    Tooltip = "Einloggen"
                });
            }
            #endregion
            #region NavigationAside
            viewmodel.Data.NavigationAside.Add(new NavItem()
            {
                Text = "Informationen",
                State = "event.details({id: 10})"
            });
            viewmodel.Data.NavigationAside.Add(new NavItem()
            {
                Text = "Teilnehmer",
                State = "event.seating({id: 10})"
            });
            viewmodel.Data.NavigationAside.Add(new NavItem()
            {
                Text = "Jugendschutz",
                State = "jugendschutz"
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
            #region EventsAside
            foreach (var e in dataCtrl.GetItems().Where(x => x.End > DateTime.Now).OrderByDescending(x => x.Start))
            {
                var seats = seatDataCtrl.GetItems().Where(x => x.EventID == e.ID);
                Int32 seatsCount = 70 - seats.Count(x => x.State == -1);
                Int32 flagged = seats.Count(x => x.State == 1);
                Int32 reserved = seats.Count(x => x.State == 2);
                Int32 free = seatsCount - flagged - reserved;
                viewmodel.Data.EventsAside.Add(new EventItem()
                {
                    ID = e.ID,
                    Title = $"{e.EventType.Name} Vol.{e.Volume}",
                    Start = e.Start,
                    End = e.End,
                    PublicAccess = !e.IsPrivate,
                    Seating = new data.ViewModel.Event.EventViewModelItem.SeatingReservation()
                    {
                        SeatsCount = 70 - seats.Count(x => x.State == -1),
                        Flagged = flagged,
                        Reserved = reserved,
                        Free = free
                    }
                });
            }
            #endregion
            #region PartnerTop
            foreach (var p in partner.Where(x => x.PartnerDisplay.Name == "Header"))
                viewmodel.Data.PartnerTop.Add(new PartnerItem()
                {
                    Name = p.Partner.Name,
                    Link = p.Partner.Link,
                    ImagePassive = Properties.Settings.Default.imageAbsolutePath + p.Partner.ImagePassive + "/passive.png",
                    ImageOriginal = Properties.Settings.Default.imageAbsolutePath + p.Partner.ImageOriginal + "/image.png"
                });
            #endregion
            #region NavigationBottom
            foreach (var p in partner.Where(x => x.PartnerDisplay.Name == "Footer"))
                viewmodel.Data.NavigationBottom.Add(new LinkItem()
                {
                    Text = p.Partner.Name,
                    Link = p.Partner.Link
                });
            #endregion

            return Ok(viewmodel);
        }

        [HttpGet]
        public IHttpActionResult Backend()
        {
            BackendNavigationViewModel viewmodel = new BackendNavigationViewModel();

            #region NavigationTop
            viewmodel.Data.NavigationTop.Add(new NavItem()
            {
                Text = "Mein Profil",
                State = "profile.edit",
                StateCompare = "account.edit"
            });
            viewmodel.Data.NavigationTop.Add(new NavItem()
            {
                Text = "Backend verlassen",
                State = "home",
                StateCompare = "home"
            });
            #endregion
            #region NavigationAside
            // Dashboard
            viewmodel.Data.NavigationAside.Add(new NavItem()
            {
                Text = "Dashboard",
                State = "admin.dashboard",
                StateCompare = "admin.dashboard"
            });
            // News
            //viewmodel.Data.NavigationAside.Add(new NavItem()
            //{
            //    Text = "News",
            //    State = "admin.news.all",
            //    StateCompare = "admin.news"
            //});
            // Events
            viewmodel.Data.NavigationAside.Add(new NavItem()
            {
                Text = "Events",
                StateCompare = "admin.event",
                SubMenu = new List<NavItem>() {
                    new NavItem() {
                        Text = "Alle Events",
                        State = "admin.event.all",
                        StateCompare = "admin.event.all"
                    },
                    new NavItem() {
                        Text = "Neues Event",
                        State = "admin.event.new",
                        StateCompare = "admin.event.new"
                    },
                    new NavItem() {
                        Text = "Alle Event Typen",
                        State = "admin.eventtype.all",
                        StateCompare = "admin.eventtype.all"
                    },
                    new NavItem() {
                        Text = "Neuer Event Typ",
                        State = "admin.eventtype.new",
                        StateCompare = "admin.eventtype.new"
                    }
                }
            });
            // Turniere
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
                        State = "admin.game.all",
                        StateCompare = "admin.game.all"
                    }
                }
            });
            // Seating
            viewmodel.Data.NavigationAside.Add(new NavItem()
            {
                Text = "Sitzplan",
                StateCompare = "admin.seating",
                SubMenu = new List<NavItem>() {
                    new NavItem() {
                        Text = "Alle Plätze",
                        State = "admin.seating.all",
                        StateCompare = "admin.seating.all"
                    },
                    new NavItem() {
                        Text = "Plätze Tauschen",
                        State = "admin.seating.swap",
                        StateCompare = "admin.seating.swap"
                    }
                }
            });
            // Catering
            viewmodel.Data.NavigationAside.Add(new NavItem()
            {
                Text = "Catering",
                StateCompare = "admin.catering",
                SubMenu = new List<NavItem>() {
                    new NavItem() {
                        Text = "Alle Bestellungen",
                        State = "admin.catering.all",
                        StateCompare = "admin.catering.all"
                    },
                    new NavItem() {
                        Text = "Bestellung aufnehmen",
                        State = "admin.catering.new",
                        StateCompare = "admin.catering.new"
                    }
                }
            });
            // Partner
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
                        Text = "Sortieren",
                        State = "admin.partner.position",
                        StateCompare = "admin.partner.position"
                    },
                    new NavItem() {
                        Text = "Typen",
                        State = "admin.partner.all",
                        StateCompare = "admin.partner.all"
                    }
                }
            });
            // Benutzer
            viewmodel.Data.NavigationAside.Add(new NavItem()
            {
                Text = "Benutzer",
                State = "admin.user.all",
                StateCompare = "admin.user"
            });
            #endregion

            return Ok(viewmodel);
        }
    }
}
