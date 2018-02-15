using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace api.NetConnect.data.ViewModel.Account
{
    public class TransferReservationRequest
    {
        public Int32 SeatID { get; set; }
        public String Email { get; set; }
        public String Password { get; set; }
    }
}
