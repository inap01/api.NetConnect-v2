﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static api.NetConnect.data.ViewModel.Event.EventViewModelItem;

namespace api.NetConnect.data.ViewModel.Navigation
{
    public class NavigationViewModel : BaseViewModel
    {
        public NavigationViewModelItem Data { get; set; }

        public NavigationViewModel()
        {
            Data = new NavigationViewModelItem();
        }
    }

    public class NavigationViewModelItem : BaseViewModelItem
    {
        public List<NavItem> NavigationUser { get; set; }
        public List<NavItem> NavigationTop { get; set; }
        public List<NavItem> NavigationAside { get; set; }
        public List<EventItem> EventsAside { get; set; }
        public List<PartnerItem> PartnerTop { get; set; }
        public List<LinkItem> NavigationBottom { get; set; }

        public NavigationViewModelItem()
        {
            NavigationUser = new List<NavItem>();
            NavigationTop = new List<NavItem>();
            NavigationAside = new List<NavItem>();
            EventsAside = new List<EventItem>();
            PartnerTop = new List<PartnerItem>();
            NavigationBottom = new List<LinkItem>();
        }
    }

    public class NavItem
    {
        public String Text { get; set; }
        public String State { get; set; }
        public String StateCompare { get; set; }
        public String Tooltip { get; set; }
        public List<NavItem> SubMenu { get; set; }
    }

    public class LinkItem
    {
        public String Text { get; set; }
        public String Link { get; set; }
    }

    public class PartnerItem
    {
        public String Name { get; set; }
        public String ImagePassive { get; set; }
        public String ImageOriginal { get; set; }
        public String Link { get; set; }
    }

    public class EventItem
    {
        public Int32 ID { get; set; }
        public String Title { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public SeatingReservation Seating { get; set; }
        public Boolean PublicAccess { get; set; }

        public EventItem()
        {
            Seating = new SeatingReservation();
        }
    }
}