using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace api.NetConnect.data.ViewModel.Seating.Backend
{
    public class BackendSeatingViewModel : BackendBaseViewModel
    {
        public BackendSeatingViewModelItem Data { get; set; }

        public BackendSeatingViewModel()
        {
            Data = new BackendSeatingViewModelItem();
        }
    }

    public class BackendSeatingViewModelItem : BaseViewModelItem
    {
        public Int32 ReservationState { get; set; }
        public DateTime ReservationDate { get; set; }
        public String Description { get; set; }
        public Boolean IsPayed { get; set; }
        public Boolean IsTeam { get; set; }
        public SeatingUser User { get; set; }

        public BackendSeatingViewModelItem()
        {
        }

        public class SeatingUser : BaseViewModelItem
        {
            public String FirstName { get; set; }
            public String LastName { get; set; }
            public String Nickname { get; set; }
            public String Email { get; set; }
        }
    }
}