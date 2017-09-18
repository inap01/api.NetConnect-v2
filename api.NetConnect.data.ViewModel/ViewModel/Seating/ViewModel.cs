using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace api.NetConnect.ViewModels.Seating
{
    public class SeatingViewModel : BaseViewModel
    {
        public List<SeatingViewModelItem> Data { get; set; }
        public Int32 TotalCount { get; set; }

        public SeatingViewModel()
        {
            Data = new List<SeatingViewModelItem>();
            TotalCount = 0;
        }
    }

    public class SeatingViewModelItem
    {
        public Int32 ID { get; set; }
        public SeatingUser User { get; set; }
        public Int32 Status { get; set; }
        public String Description { get; set; }
        public DateTime ReservationDate { get; set; }
        public DateTime Payed { get; set; }

        public SeatingViewModelItem()
        {
            User = new SeatingUser();
        }
    }

    public class SeatingUser
    {
        public Int32 ID { get; set; }
        public String Email { get; set; }
        public String FirstName { get; set; }
        public String LastName { get; set; }
        public String Nickname { get; set; }

        public SeatingUser()
        {

        }
    }
}